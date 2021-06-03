"""
This is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This package is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this package. If not, you can get eh GNU GPL from
https://www.gnu.org/licenses/gpl-3.0.en.html.

Created by Robert Ljubicic.
"""

try:
	from __init__ import *
	from datetime import timedelta
	from shutil import copy, SameFileError
	from time import time
	from os import path, listdir, makedirs, remove
	from feature_tracking import get_gcps_from_image, fresh_folder
	from itertools import product
	from warnings import warn
	from math import log

	import glob

except Exception as ex:
	print('\n[EXCEPTION] Import failed: \n\n'
		  '  {}'.format(ex))
	input('\nPress any key to exit...')
	exit()


MAX_FRAMES_DEFAULT = 60**3  # 60 minutes at 60fps
separator = '---'


def framesToVideo(output, folder='.', ext='jpg', codec='MJPG', fps=30.00, scale=1.0,
				  max_frames=MAX_FRAMES_DEFAULT, verbose=True, interp=cv2.INTER_LINEAR, size_adj=False) -> bool:
	"""
	Convert individual images to a video. Images are specified using prefix and extension and the code iterates through
	corresponding images in a specified folder. Several video codecs are available, scaling is provided, FPS
	and a maximum number of num_frames can be set.

	:param output:		Video output filename as a string. Enter without the extension as only .avi is available.
	:param folder:		Folder containing the images to be written to a video. The video will be created in the same
						folder. Default is the parent folder, i.e. '.'.
	:param prefix:		String prefix of files to be written to a video. Default is 'frame'.
	:param ext:			Extension of the image files. Should be a string, without the leading dot. Default is 'jpg'.
	:param codec:		Codec for the output video. Default is MJPG which has the best quality but a large file size.
						Other available are DIVX, XVID, WMV1 and WMV2, which are all smaller in size but provide worse quality video.
	:param fps:			FPS count for the output video. Default is 30.
	:param scale:		Scale factor for the video. Linear interpolation is used if scale != 1.0. Default is 1.0.
	:param max_frames:	Maximum number of num_frames to write to video. Default is defined by MAX_FRAMES_DEFAULT global.
						Be careful not to exceed the PC RAM limit since the video is stored in RAM until the final frame is encoded.
	:param verbose:		Whether to use a verbose output. Default is False.
	:param interp:		Interpolation algorithm for image resizing from cv2 package. Default is cv2.INTER_CUBIC.
	:param size_adj:	Whether to adjust the size of all frames to the size of the first frame. Default is False.
	:return:			True is success, raises an error if something is wrong.
	"""

	# Check for allowed codec
	# codec = 'H264' if '264' in codec else ('HEVC' if '265' in codec else codec)

	assert codec in ('MJPG', 'DIVX', 'XVID', 'WMV1', 'WMV2')

	# Get the first image shape
	for filename in listdir(folder):
		if filename.endswith('.' + ext):
			image = cv2.imread(folder + '/' + filename)
			height, width, *_ = image.shape
			break

	# Verify shape available
	try:
		height, width
	except NameError:
		raise ValueError('[ERROR] Could not obtain image shape. Check file frame_path or type!')

	# TODO: Should I include other extensions?
	saveStr = folder + '/' + output + '.avi'

	out = cv2.VideoWriter(saveStr, cv2.VideoWriter_fourcc(*codec), fps,
						  (int(scale * width), int(scale * height)))

	if verbose:
		print(separator)
		print('[BEGIN] :STARTING FRAMESTOVIDEO: '.ljust(len(separator), '-'))
		print('[INFO] Encoding frames to video from folder', folder + '/')
		print('[INFO] Writing results to', saveStr)
		print(separator)

	i = 0

	if max_frames is None:
		max_frames = 60**3  # 60 minutes at 60fps

	# Go through all frames
	for filename in listdir(folder):
		if filename.endswith('.' + ext) and i < max_frames:
			image = cv2.imread(folder + '/' + filename)
			h, w, *_ = image.shape

			# Happens sometimes with oddly packed videos
			if h != height or w != width:
				if not size_adj:
					print('[ERROR] Frame {} does not have the same size as the first frame!\n'
						  '[ERROR] OpenCV Video writer requires all frames to be the same size!'.format(i))
					input('\nPress any key to exit...')
					exit()
				else:
					print('[WARNING] Adjusting the size of frame {} to {}x{} px'.format(i, width, height))
					cv2.resize(image, [height, width], interpolation=interp)

			if scale != 1.0:
				image = cv2.resize(image, None, fx=scale, fy=scale, interpolation=interp)

			out.write(image)

			if verbose:
				print('[INFO] Writing frame: {}'.format(i))

			i += 1

	# Clear output video from memory
	out.release()

	if verbose:
		print(separator)
		print('[END] Video written to {} using {} codec'.format(saveStr, codec))
		print('[END] Total number of num_frames written is', i)
		print('[END] Total duration of the video is', timedelta(seconds=(i / fps)))
		size = path.getsize(saveStr) / (1024 * 1024)
		print('[END] Total size of the file is', round(size, 2), 'MB')
		print(separator)

	return True


def coordTransform(image: np.ndarray,
				   points_old: np.ndarray, points_new: np.ndarray,
				   width: int, height: int,
				   method=cv2.findHomography,
				   M_ortho=None,
				   use_ransac=False, ransac_thr=None,
				   confidence=0.995, LM_iters=10) -> tuple:
	"""
	Performs coordinate transform an image or set of images using point handles. Original handle positions and new handle
	positions must be specifies, along with the height and width of the new image(s). See parameters for more details.

	:param image:			Image as numpy.ndarray to be transformed.
	:param points_old:		Handle points for transformation. A list of handles (min. 2) must be specified as [x, y].
	:param points_new:		New position for handles. A list of four handles must be specified as [x, y].
	:param width:			Width of the transformed image. Must be specified.
	:param height:			Height of the transformed image. Must be specified.
	:param method:			Method to use for the transformation: 	cv2.estimateAffinePartial2D,
																	cv2.getAffineTransform,
																	cv2.estimateAffine2D,
																	cv2.getPerspectiveTransform, or
																	cv2.findHomography (default).
	:param M_ortho:			Orthorectification matrix.
	:param use_ransac:		Whether to use RANSAC filtering for outlier detection. Default is False.
	:param ransac_thr:		Threshold for RANSAC outlier detection. Default is 1.
	:param confidence:		Required confidence for the transformation matrix. Default is 0.995.
	:param LM_iters:		Number of Levenberg-Marquardt iterations for refining. Default is 10.
	:param show:			Whether to show the original and the stabilized image. Default is False.
	:return:				New transformed image as numpy.ndarray.
	"""

	assert len(points_old) >= 2 and len(points_new) >= 2, \
		'[ERROR] Minimal number of origin and destination points is 2!'
	assert len(points_old) == len(points_new), \
		'[ERROR] Number of origin points must be equal to the number of destination points!'

	if method in [cv2.estimateAffine2D, cv2.estimateAffinePartial2D]:
		if use_ransac:
			M_stable, status = method(points_old, points_new, method=cv2.RANSAC, ransacReprojThreshold=ransac_thr, confidence=confidence, refineIters=LM_iters)
		else:
			M_stable, status = method(points_old, points_new, confidence=confidence, refineIters=LM_iters)

	elif method == cv2.getAffineTransform:
		if len(points_old) != 3:
			warn('[WARNING] Origin array not of size 3 for strict affine transformation!\n'
				 'Using only the first 3 features in from the file to estimate the transformation matrix.')
		M_stable = method(points_old[:3], points_new[:3])
		status = []

	elif method == cv2.getPerspectiveTransform:
		if len(points_old) != 4:
			warn('[WARNING] Origin array not of size 4 for simple projective transformation!\n'
				 'Using only the first 4 features in from the file to estimate the transformation matrix.')
		M_stable = method(points_old[:4], points_new[:4])
		status = []

	elif method == cv2.findHomography:
		if use_ransac:
			M_stable, status = method(points_old, points_new, method=cv2.RANSAC, ransacReprojThreshold=ransac_thr, confidence=confidence)
		else:
			M_stable, *_ = method(points_old, points_new, 0)
			status = []

	else:
		print('[ERROR] Unknown transformation method for stabilization point set!')
		input('\nPress any key to exit...')

	if M_ortho is not None:
		M_final = np.matmul(M_ortho, M_stable)
	else:
		M_final = M_stable

	if method in [cv2.getPerspectiveTransform, cv2.findHomography]:
		ortho = cv2.warpPerspective(image, M_final, (width, height))[::-1]
	else:
		ortho = cv2.warpAffine(image, M_final, (width, height))[::-1]

	return ortho, M_stable, status


def imcrop(img, bbox):
	"""
	Crop image to boundary box.

	:param img: 	Image.
	:param bbox: 	Boundary box as [Xstart, Xend, Ystart, Yend].
	:return: 		Cropped image.
	"""

	x1, x2, y1, y2 = bbox

	if x1 < 0 or y1 < 0 or x2 > img.shape[1] or y2 > img.shape[0]:
		img, x1, x2, y1, y2 = pad_img_to_fit_bbox(img, x1, x2, y1, y2)

	return img[y1:y2, x1:x2, :]


def gcp_distances(gcp_coord):
	N = len(gcp_coord)
	dists = np.ndarray([N, N])

	for i, j in product(range(N), range(N)):
		fr = gcp_coord[i]
		to = gcp_coord[j]
		dists[i, j] = ((fr[0] - to[0])**2 + (fr[1] - to[1])**2) ** 0.5

	return dists


def pad_img_to_fit_bbox(img, x1, x2, y1, y2):
	"""
	Pad image with zeros for cropping.

	:param img: Image.
	:param x1:  X from.
	:param x2: 	X to.
	:param y1: 	Y from.
	:param y2: 	Y to.
	:return: 	Padded image.
	"""

	img = np.pad(img, ((np.abs(np.minimum(0, y1)), np.maximum(y2 - img.shape[0], 0)),
			   (np.abs(np.minimum(0, x1)), np.maximum(x2 - img.shape[1], 0)), (0, 0)), mode="constant")

	y1 += np.abs(np.minimum(0, y1))
	y2 += np.abs(np.minimum(0, y1))
	x1 += np.abs(np.minimum(0, x1))
	x2 += np.abs(np.minimum(0, x1))

	return img, x1, x2, y1, y2


def compress(data, selectors):
	return list(d for d, s in zip(data, selectors) if s)


if __name__ == '__main__':
	try:
		parser = ArgumentParser()
		parser.add_argument('--cfg', type=str, help='Path to configuration file')
		args = parser.parse_args()

		cfg = configparser.ConfigParser()
		cfg.optionxform = str

		try:
			cfg.read(args.cfg, encoding='utf-8-sig')
		except:
			print('[ERROR] There was a problem reading the configuration file!\nCheck if project has valid configuration.')
			exit()

		section = 'Stabilization'

		# Folder in which the data is located
		frames_folder = cfg.get(section, 'InputFolder')

		# Output folder location
		results_folder = cfg.get(section, 'OutputFolder')

		try:
			copy(args.cfg, results_folder)
		except SameFileError:
			pass

		# Prefix for frame files
		ext = cfg.get(section, 'ImageExtension', fallback='jpg')

		# Output image quality [1-100]
		qual = float(cfg.get(section, 'ImageQuality', fallback='95'))

		# FPS count for stabilized video
		fps = float(cfg.get(section, 'fps'))

		# See available methods below in :methods:
		stabilization_method = int(cfg.get(section, 'Method'))

		# Detection and filtering of outliers, if available for the chosen method
		use_ransac_filtering = int(cfg.get(section, 'UseRANSAC', fallback='0'))

		# Acceptable reprojection error for outlier detection
		ransac_filtering_thr = float(cfg.get(section, 'RANSACThreshold', fallback='2.0'))

		# Perform orthorectification
		orthorectify = int(cfg.get(section, 'Orthorectify', fallback='0'))

		# px/meter
		px_ratio = float(cfg.get(section, 'PXRatio'))

		# Select/discard GCPs
		gcps_mask = cfg.get(section, 'FeatureMask')

		# Image padding outside GCP area
		pdx = cfg.get(section, 'PaddX')
		pdy = cfg.get(section, 'PaddY')

		# Whether to use camera perameters to remove distortion
		remove_distortion = int(cfg.get(section, 'Undistort', fallback=0))

		if remove_distortion and not path.exists(r'{}/camera_parameters.txt'.format(results_folder)):
			print('[WARNING] Camera parameters file not found in {}'.format(results_folder))
			input('[PROCEED?] If you wish to proceed without the camera parameters (without undistortion), press Enter/Return key...')
			remove_distortion = 0

		# Whether to create a video from frames
		create_video = int(cfg.get(section, 'CreateVideo'))

		padd_x = [int(float(x) * px_ratio) for x in pdx.split('-')]
		padd_y = [int(float(y) * px_ratio) for y in pdy.split('-')]

		# Do not change from this point on ---------------------------------------------------------------------------------
		methods = {0: cv2.estimateAffinePartial2D,
				   1: cv2.getAffineTransform,
				   2: cv2.estimateAffine2D,
				   3: cv2.getPerspectiveTransform,
				   4: cv2.findHomography}

		methods_alias = ['similarity',
						 'affine_2D_strict',
						 'affine_2D_optimal',
						 'projective_strict',
						 'projective_optimal']

		gcp_folder = '{}/gcps_csv'.format(results_folder)
		stabilized_folder = '{}/images_{}/{}'.format(results_folder,
												 'orthorectified' if orthorectify else 'stabilized',
													 methods_alias[stabilization_method])
		transform_folder = '{}/transform_{}/{}'.format(results_folder,
													   'orthorectified' if orthorectify else 'stabilized',
													   methods_alias[stabilization_method])
		end_file = '{}/end'.format(results_folder)

		fresh_folder(stabilized_folder)
		fresh_folder(transform_folder)

		if path.exists(end_file):
			remove(end_file)

		raw_frames_list = glob.glob('{}/*.{}'.format(frames_folder, ext))
		features_coord = glob.glob('{}/*.txt'.format(gcp_folder))
		total_frames = len(raw_frames_list)
		num_len = int(log(total_frames, 10)) + 1
		total_features = np.loadtxt(features_coord[0], dtype='float32', delimiter=' ').shape[0]

		print('[BEGIN] Stabilization starting for data in {}'.format(results_folder))

		anchors = np.loadtxt(features_coord[0], dtype='float32', delimiter=' ')

		num_avail_gcps = anchors.shape[0]

		if gcps_mask == '1':
			gcps_mask = [1] * num_avail_gcps
		else:
			gcps_mask = [int(x) for x in gcps_mask]
			num_avail_gcps = gcps_mask.count(True)
			anchors = np.asarray(compress(anchors, gcps_mask))

		folders_to_check = [stabilized_folder,
							transform_folder]

		for f in folders_to_check:
			if not path.exists(f):
				makedirs(f)

		assert num_avail_gcps >= 2,\
			'[ERROR] Number of available GCPs is not >= 2 in all frames!\n' \
			'        Consider repeating the feature tracking with features which are available in all frames.\n' \
			'        If this cannot be achieved, consider splitting the video into several segments and then' \
			'        stabilizing each one individually.'
		if stabilization_method > 3: assert num_avail_gcps > 3,\
			'[ERROR] Number of available GCPs is not >= 4 in all frames for projective transform!\n' \
			'        Consider switching to one of the available affine transformation methods or\n' \
			'        repeat the feature tracking with features which are available in all frames.'

		img = cv2.imread(raw_frames_list[0], cv2.COLOR_BGR2RGB)

		h, w = img.shape[:2]

		if orthorectify:
			gcps_real = np.multiply(np.loadtxt('{}/gcps_real.txt'.format(results_folder), dtype='float32', delimiter=' '), px_ratio)
			gcps_image = np.asarray(get_gcps_from_image(img, verbose=True, hide_sliders=True))

			assert gcps_real.shape == gcps_image.shape,\
				'[ERROR] Number of GCPs [{}] not equal to number of selected features [{}]'.format(gcps_real.shape[0], gcps_image.shape[0])

			min_x = np.min(gcps_real[:, 0])
			min_y = np.min(gcps_real[:, 1])

			for p in gcps_real:
				p[0] = p[0] - min_x + padd_x[0]
				p[1] = p[1] - min_y + padd_y[0]

			max_x = int(np.max(gcps_real[:, 0]) + padd_x[1])
			max_y = int(np.max(gcps_real[:, 1]) + padd_y[1])

			h, w = max_y, max_x

			np.savetxt('{}/gcps_image.txt'.format(results_folder), gcps_image, fmt='%.3f', delimiter=' ')

			if gcps_real.shape[0] >= 4:
				if use_ransac_filtering:
					M_ortho, *_ = cv2.findHomography(gcps_image, gcps_real, cv2.RANSAC, ransac_filtering_thr)
				else:
					M_ortho, *_ = cv2.findHomography(gcps_image, gcps_real, 0)

			elif gcps_real.shape[0] == 3:
				if use_ransac_filtering:
					M_ortho, *_ = cv2.estimateAffine2D(gcps_image, gcps_real, method=cv2.RANSAC, ransacReprojThreshold=ransac_filtering_thr, confidence=0.995, refineIters=10)
				else:
					M_ortho, *_ = cv2.estimateAffine2D(gcps_image, gcps_real, confidence=0.995, refineIters=10)

		else:
			M_ortho = None

		i = 0

		while True:
			try:
				start_time = time()

				features = np.asarray(compress(np.loadtxt(features_coord[i], dtype='float32', delimiter=' '), gcps_mask))
				img_path = raw_frames_list[i]
				img = cv2.imread(img_path)

				stabilized, M, status = coordTransform(img, features, anchors, width=w, height=h,
													   method=methods[stabilization_method],
													   M_ortho=M_ortho,
													   use_ransac=use_ransac_filtering,
													   ransac_thr=ransac_filtering_thr)

				stabilized = stabilized[::-1]

				n = str(i).rjust(num_len, '0')
				np.savetxt('{}/{}.txt'.format(transform_folder, n), M, delimiter=' ')
				save_str_img = '{}/{}.{}'.format(stabilized_folder, n, ext)

				if ext.lower() in ['jpg', 'jpeg']:
					cv2.imwrite(save_str_img,
								stabilized,
								[int(cv2.IMWRITE_JPEG_QUALITY), qual])
				elif ext.lower() == 'png':
					cv2.imwrite(save_str_img,
								stabilized,
								[int(cv2.IMWRITE_PNG_COMPRESSION), 9 - int(0.09 * qual)])
				elif ext.lower() == 'webp':
					cv2.imwrite(save_str_img,
								stabilized,
								[int(cv2.IMWRITE_WEBP_QUALITY), qual + 1])
				else:
					cv2.imwrite(save_str_img, stabilized[::-1])

				note = ''
				if [0] in status:
					note = '==> Outlier detected: {}'.format([i[0] for i in status])

				print('[INFO] Stabilized frame {}/{} in {:.3f} sec ({:.1f}%) {}'.format(i, total_frames-1, time() - start_time, i/(total_frames-1) * 100, note))

				i += 1

			except (IOError, IndexError):
				break

		if create_video:
			framesToVideo('!stabilized' if not orthorectify else '!orthorectified', folder=stabilized_folder, ext=ext, fps=fps, verbose=True)

		open(end_file, 'w').close()

		print('\a')
		input('Press any key to exit...')

	except Exception as ex:
		print('\n[EXCEPTION] The following exception has occurred: \n\n'
			  '  {}'.format(ex))
		input('\nPress any key to exit...')
