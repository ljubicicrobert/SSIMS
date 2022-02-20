#!/usr/bin/env python3
# -*- coding: utf-8 -*-

try:
	import os
	import glob
	import matplotlib.pyplot as plt

	from __init__ import *

except Exception as ex:
	print('\n[EXCEPTION] Import failed: \n\n'
	      '  {}'.format(ex))
	input('\nPress ENTER/RETURN to exit...')
	exit()


if __name__ == '__main__':
	try:
		parser = ArgumentParser(add_help=False)
		parser.add_argument('--model', type=str, help='Camera model name', default='')
		parser.add_argument('--folder', type=str, help='Path to images folder')
		parser.add_argument('--ext', type=str, help='Frames'' extension', default='jpg')
		parser.add_argument('-w', type=int, help='Number of squares in horizontal direction')
		parser.add_argument('-h', type=int, help='Number of squares in vertical direction')
		parser.add_argument('--use_k3', type=int, help='Whether to use three radial distortion coefficients', default=0)
		parser.add_argument('--output', type=int, help='Whether to output undistorted images', default=0)
		args = parser.parse_args()

		input_folder = args.folder
		output_folder = '{}/undistorted'.format(input_folder)
		extension = args.ext
		camera_model = 'camera_parameters' if args.model == '' else args.model

		if not os.path.exists(output_folder):
			os.mkdir(output_folder)

		cheq_w = int(args.w) - 1
		cheq_h = int(args.h) - 1

		board_size = (cheq_w, cheq_h)
		fixed_k3 = 1 if args.use_k3 == 0 else 0
		calibration_flags = cv2.CALIB_FIX_K3 if fixed_k3 else 0

		objpoints = []
		imgpoints = []

		objp = np.zeros((1, board_size[0] * board_size[1], 3), np.float32)
		objp[0, :, :2] = np.mgrid[0:board_size[0], 0:board_size[1]].T.reshape(-1, 2)

		fig, ax = plt.subplots()

		images = glob.glob('{}/*.{}'.format(input_folder, extension))
		image_names = [os.path.basename(x) for x in images]
		num_images = len(images)
		ret_list = [None] * num_images

		image_0 = cv2.imread(images[0], 0)
		h, w = image_0.shape
		h, w = min(h, w), max(h, w)
		rotations = [0] * num_images

		try:
			mng = plt.get_current_fig_manager()
			mng.set_window_title('Chequerboard corners detection')
		except:
			pass

		print('[START] Starting camera calibration using images in folder [{}]\n'.format(input_folder))

		for i, fname in enumerate(images):
			img_bgr = cv2.imread(fname)
			img_gray = cv2.cvtColor(img_bgr, cv2.COLOR_BGR2GRAY)

			if img_gray.shape != (h, w):
				if img_gray.shape == (w, h):
					print('   [INFO] Rotating image {}/{}'.format(i+1, num_images))
					img_bgr = cv2.rotate(img_bgr, cv2.ROTATE_90_CLOCKWISE)
					img_gray = cv2.rotate(img_gray, cv2.ROTATE_90_CLOCKWISE)
					rotations[i] = 1
				else:
					print('[ERROR] All images in the folder [{}] must be of the same size!'.format(input_folder))
					input(input('\nPress ENTER/RETURN to exit...'))
					exit()

			ret, corners = cv2.findChessboardCorners(img_gray, board_size)
			plt.cla()

			if ret:
				print('[SUCCESS] Detected corners in image {}/{}'.format(i+1, num_images))
				ret_list[i] = ret
				objpoints.append(objp)
				corners_subpixel = cv2.cornerSubPix(img_gray, corners, (11, 11), (-1, -1), (cv2.TERM_CRITERIA_EPS + cv2.TERM_CRITERIA_MAX_ITER, 30, 0.001))
				imgpoints.append(corners_subpixel)
			else:
				print(' [FAILED] Corner detection failed in image {}/{}'.format(i+1, num_images))

			if ret:
				xs = corners_subpixel[:, 0, 0].tolist()
				ys = corners_subpixel[:, 0, 1].tolist()
				plt.scatter(xs, ys, facecolors='none', edgecolors='r')

			plt.imshow(img_gray, cmap='gray')
			plt.title('Image {}/{}'.format(i+1, num_images))
			plt.axis('off')
			plt.draw()
			plt.pause(0.01)

		plt.pause(1.0)
		plt.close()

		print('\n   [INFO] Calculating camera intrinsics and distortion coefficients...', end='')
		ret, mtx, dist, rvecs, tvecs = cv2.calibrateCamera(objpoints, imgpoints, (w, h), None, None, None, None, flags=calibration_flags)
		print(' DONE!\n')

		mean_img_errors = []

		for i in range(len(objpoints)):
			if ret_list[i] is not None:
				reprojected_points, _ = cv2.projectPoints(objpoints[i], rvecs[i], tvecs[i], mtx, dist)
				s = cv2.norm(imgpoints[i], reprojected_points, cv2.NORM_L2) / np.sqrt(len(reprojected_points))
				mean_img_errors.append(s)
			else:
				mean_img_errors.append(np.NaN)

		mean_error = np.nanmean(mean_img_errors)
		stdev_error = np.nanstd(mean_img_errors)
		good_images = np.count_nonzero(~np.isnan(mean_img_errors))

		np.savetxt('{}/ret_list.txt'.format(input_folder), mean_img_errors, fmt='%.4f')

		print('   [INFO] Mean reprojection error = {:.3f} px'.format(mean_error))

		mtx_scaled = mtx / w

		print('\nCamera matrix (f=F/W, c=C/W):')
		print('fx = {:.8f}'.format(mtx_scaled[0, 0]))
		print('fy = {:.8f}'.format(mtx_scaled[1, 1]))
		print('cx = {:.8f}'.format(mtx_scaled[0, 2]))
		print('cy = {:.8f}'.format(mtx_scaled[1, 2]))

		print('\nk1 = {:.8f}'.format(dist[0, 0]))
		print('k2 = {:.8f}'.format(dist[0, 1]))
		print('k3 = {:.8f}\n'.format(dist[0, 4]))
		print('p1 = {:.8f}'.format(dist[0, 2]))
		print('p2 = {:.8f}'.format(dist[0, 3]))

		plt.bar(list(range(num_images)), mean_img_errors)
		plt.xticks(list(range(num_images)), [s.split('.')[0] for s in image_names], rotation=90)
		plt.ylabel('Reprojection error [px]'.format(mean_error))
		plt.title('Mean reprojection error = {:.3f} px'.format(mean_error))
		plt.show()

		if args.output == 1:
			print('\n[INFO] Writing undistorted images to [{}]\n'.format(output_folder))

			for i, iname in enumerate(image_names):
				img_bgr = cv2.imread(images[i])
				if rotations[i] == 1:
					img_bgr = cv2.rotate(img_bgr, cv2.ROTATE_90_CLOCKWISE)

				dst = cv2.undistort(img_bgr, mtx, dist)
				cv2.imwrite('{}/{}'.format(output_folder, iname), dst)
				print('[INFO] Undistorting image {}/{}'.format(i+1, num_images))

		cfg = configparser.ConfigParser()
		cfg.optionxform = str

		cfg['Camera'] = {'Model': args.model}
		cfg['Calibration'] = {
			'Total images': '{:d}'.format(num_images),
			'Detected patterns': '{:d}'.format(good_images),
			'Failed detections': '{:d}'.format(num_images - good_images),
			'Cheq. W': args.w,
			'Cheq. H': args.h,
			'Mean repr. error [px]': '{:.4f}'.format(mean_error),
			'Stdev repr. error [px]': '{:.4f}'.format(stdev_error),
		}
		cfg['Intrinsics'] = {
			'fx': '{:.8f}'.format(mtx_scaled[0, 0]),
			'fy': '{:.8f}'.format(mtx_scaled[1, 1]),
			'cx': '{:.8f}'.format(mtx_scaled[0, 2]),
			'cy': '{:.8f}'.format(mtx_scaled[1, 2]),
		}
		cfg['Radial'] = {
			'k1': '{:.8f}'.format(dist[0, 0]),
			'k2': '{:.8f}'.format(dist[0, 1]),
			'k3': '{:.8f}'.format(dist[0, 4]),
		}
		cfg['Tangential'] = {
			'p1': '{:.8f}'.format(dist[0, 2]),
			'p2': '{:.8f}'.format(dist[0, 3]),
		}

		with open('{}/{}.cpf'.format(input_folder, camera_model), 'w') as configfile:
			cfg.write(configfile)

		print('\n [END] Camera calibration complete!')
		input('\nPress ENTER/RETURN to exit...')

	except Exception as ex:
		print('\n[EXCEPTION] The following exception has occurred: \n\n'
		      '  {}'.format(ex))
		input('\nPress ENTER/RETURN to exit...')
