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

	from math import log
	from shutil import copy, SameFileError
	from os import makedirs, listdir, remove, rename
	from os.path import exists, isfile, islink
	from time import time
	from warnings import warn
	from sys import exit
	from collections import deque
	from scipy.optimize import leastsq
	from skimage.metrics import structural_similarity as ssim
	from matplotlib.widgets import Slider

	import glob
	import matplotlib.pyplot as plt

except Exception as ex:
	print('\n[EXCEPTION] Import failed: \n\n'
		  '  {}'.format(ex))
	input('\nPress any key to exit...')
	exit()


separator = '---'


class Logger:
	"""
	Simple class for .txt logging.
	Initialize with path to .txt log file.
	Use method log(str) to write to file.
	Use method close() to close the file.
	"""
	def __init__(self, path):
		self.file = open(path, 'w')

	def log(self, string):
		try:
			print(string)

			h, m, s = time_hms(time())
			h = str(h + 2).rjust(2, '0')
			m = str(m).rjust(2, '0')
			s = str(s).rjust(2, '0')
			self.file.write('{}:{}:{} -> {}\n'.format(h, m, s, string))

			return True

		except IOError:
			return False

	def close(self):
		self.file.close()


def to_odd(a, side=0):
	a = int(a)
	if a % 2 == 1:
		return a
	if side == 0:
		return a + 1
	else:
		return a - 1


def to_int(a):
	return int(round(a))


def get_gcps_from_image(image_orig: np.ndarray, verbose=False, ia=11, sa=21, hide_sliders=False) -> list:
	"""
	Extracts [x, y] pixel coordinates from an image using right mouse click.
	Use middle mouse button or BACKSPACE key to remove the last selected point from the list.
	Pres ENTER to accept the list of points, or ESC to cancel the operation.

	:param image_orig:	Image as numpy array to be rectified.
	:param verbose:		Whether to use verbose output. Default is False.
	:param ia:			Interrogation area size. Default is 11.
	:param sa:			Search area size. Default is 21.
	:return:			A list of initial pixel positions to use with coordTransform().
	"""

	global legend_toggle

	image = image_orig.copy()

	iw = ia // 2
	sw = sa // 2
	points = []
	org = []
	axcolor = 'lightgoldenrodyellow'
	valfmt = "%d"

	if not hide_sliders:
		ia_c, sa_c = 0.8, 0.9
	else:
		ia_c, sa_c = 1.0, 1.0

	fig, ax = plt.subplots()
	plt.subplots_adjust(bottom=0.2)

	def xy2str(l):
		s = ''
		i = 1
		for x, y in l:
			s += '{}: {}, {}\n'.format(i, x, y)
			i += 1

		return s[:-1]

	def getPixelValue(event):
		global p_list

		if event.button == 2 and len(points) > 0:
			p = points.pop()
			o = org.pop()

			image[p[1] - sw: p[1] + sw + 1, p[0] - sw: p[0] + sw + 1] = o

			points_list.set_text(xy2str(points))
			img_ref.set_data(image)
			update_ia(sl_ax_ia_size.val)
			update_sa(sl_ax_sa_size.val)
			plt.draw()

		if event.button == 3:
			p = [to_int(event.xdata), to_int(event.ydata)]
			points.append(p)

			sec_s = image[p[1] - sw: p[1] + sw + 1, p[0] - sw: p[0] + sw + 1]
			sec_i = image[p[1] - iw: p[1] + iw + 1, p[0] - iw: p[0] + iw + 1]

			org.append(image[p[1] - sw: p[1] + sw + 1, p[0] - sw: p[0] + sw + 1].copy())

			sec_i = sec_i ** ia_c
			image[p[1] - iw: p[1] + iw + 1, p[0] - iw: p[0] + iw + 1] = sec_i

			sec_s = sec_s ** sa_c
			image[p[1] - sw: p[1] + sw + 1, p[0] - sw: p[0] + sw + 1] = sec_s

			image[p[1], p[0]] = 0

			points_list.set_text(xy2str(points))
			img_ref.set_data(image)
			update_ia(sl_ax_ia_size.val)
			update_sa(sl_ax_sa_size.val)
			plt.draw()

	def keypress(event):
		global show_legend
		global p_list

		if event.key == 'd' and len(points) > 0:
			p = points.pop()
			o = org.pop()

			image[p[1] - sw: p[1] + sw + 1, p[0] - sw: p[0] + sw + 1] = o

			points_list.set_text(xy2str(points))
			img_ref.set_data(image)
			update_ia(sl_ax_ia_size.val)
			update_sa(sl_ax_sa_size.val)
			plt.draw()

		elif event.key == 'enter':
			plt.close()

		elif event.key in ['escape', 'q']:
			plt.close()
			exit('EXECUTION STOPPED: Operation aborted by user!')

		elif event.key == 'f1':
			show_legend = not show_legend
			legend_toggle.set_visible(show_legend)
			points_list.set_visible(show_legend)
			event.canvas.draw()

	if verbose:
		print('---')
		print('Click MOUSE RIGHT to add a point to the list')
		print('Press ENTER key to accept the list of points')
		print('Press D or click MOUSE MIDDLE to remove the last point in the list')
		print("Press ESC to cancel the operation")
		print('---')

	fig.canvas.mpl_connect('button_press_event', getPixelValue)
	fig.canvas.mpl_connect('key_press_event', keypress)
	img_ref = plt.imshow(image, cmap='gray')

	def update_sa(val):
		global search_size
		global k_size
		global k_span

		if val <= sl_ax_ia_size.val - 2:
			sl_ax_ia_size.set_val(val - 2)

		sa = int(val)
		sw = sa // 2

		ia = int(sl_ax_ia_size.val)
		iw = ia // 2

		search_size = sa
		k_size = ia
		k_span = iw

		image = image_orig.copy()
		org = []

		for p in points:
			sec_s = image[p[1] - sw: p[1] + sw + 1, p[0] - sw: p[0] + sw + 1]
			sec_i = image[p[1] - iw: p[1] + iw + 1, p[0] - iw: p[0] + iw + 1]

			org.append(image[p[1] - sw: p[1] + sw + 1, p[0] - sw: p[0] + sw + 1].copy())

			sec_i = sec_i ** ia_c
			image[p[1] - iw: p[1] + iw + 1, p[0] - iw: p[0] + iw + 1] = sec_i

			sec_s = sec_s ** sa_c
			image[p[1] - sw: p[1] + sw + 1, p[0] - sw: p[0] + sw + 1] = sec_s

			image[p[1], p[0]] = 0

		img_ref.set_data(image)
		plt.draw()

	def update_ia(val):
		global search_size
		global k_size
		global k_span

		if val >= sl_ax_sa_size.val + 2:
			sl_ax_sa_size.set_val(val + 2)

		sa = int(sl_ax_sa_size.val)
		sw = sa // 2

		ia = int(val)
		iw = ia // 2

		search_size = sa
		k_size = ia
		k_span = iw

		image = image_orig.copy()
		org = []

		for p in points:
			sec_s = image[p[1] - sw: p[1] + sw + 1, p[0] - sw: p[0] + sw + 1]
			sec_i = image[p[1] - iw: p[1] + iw + 1, p[0] - iw: p[0] + iw + 1]

			org.append(image[p[1] - sw: p[1] + sw + 1, p[0] - sw: p[0] + sw + 1].copy())

			sec_i = sec_i ** ia_c
			image[p[1] - iw: p[1] + iw + 1, p[0] - iw: p[0] + iw + 1] = sec_i

			sec_s = sec_s ** sa_c
			image[p[1] - sw: p[1] + sw + 1, p[0] - sw: p[0] + sw + 1] = sec_s

			image[p[1], p[0]] = 0

		img_ref.set_data(image)
		plt.draw()

	ax_ia_size = plt.axes([0.3, 0.1, 0.40, 0.03], facecolor=axcolor)
	sl_ax_ia_size = Slider(ax_ia_size, 'IA size', 3, 101, valinit=ia, valstep=2, valfmt=valfmt)
	sl_ax_ia_size.on_changed(update_ia)

	ax_sa_size = plt.axes([0.3, 0.05, 0.40, 0.03], facecolor=axcolor)
	sl_ax_sa_size = Slider(ax_sa_size, 'SA size', 5, 101, valinit=sa, valstep=2, valfmt=valfmt)
	sl_ax_sa_size.on_changed(update_sa)

	legend = 'O = zoom to window\n' \
			 'P = pan image\n' \
			 'Mouse RIGHT = select feature\n' \
			 'Mouse MIDDLE or D = remove last feature\n' \
			 'ENTER/RETURN = accept selected features\n' \
			 'Use sliders to change IA/SA window size'

	if hide_sliders:
		ax_ia_size.set_visible(False)
		ax_sa_size.set_visible(False)

		legend = 'O = zoom to window\n' \
				 'P = pan image\n' \
				 'Mouse RIGHT = select feature\n' \
				 'Mouse MIDDLE or D = remove last feature\n' \
				 'ENTER/RETURN = accept selected features'

	legend_toggle = plt.text(0.02, 0.97, legend,
							 horizontalalignment='left',
							 verticalalignment='top',
							 transform=ax.transAxes,
							 bbox=dict(facecolor='white', alpha=0.5),
							 fontsize=9,
							 )

	hint = plt.text(0.02, 1.02, 'F1: toggle legend',
					 horizontalalignment='left',
					 verticalalignment='bottom',
					 transform=ax.transAxes,
					 fontsize=9,
					 )

	points_list = plt.text(0.99, 0.02, '',
				  horizontalalignment='right',
				  verticalalignment='bottom',
				  transform=ax.transAxes,
				  bbox=dict(facecolor='white', alpha=0.5),
				  fontsize=9,
				  )

	plt.show()

	if verbose:
		print(points)

	return points


def find_gcp(single_ch: np.ndarray, kernel: np.ndarray, show=False, subpixel=True, subpixel_ksize=3) -> tuple:
	"""
	Detects a GCP center in the using SSIM.

	:param single_ch: 		Input image to search for GCP in.
	:param kernel:			Kernel to use for SSIM comparison.
	:param show: 			Whether to show the SSIM map. Default is False.
	:param subpixel: 		Whether to use subpixel estimator. Default is True.
	:param subpixel_ksize: 	The size of the subpixel search area. Default is 3.
	:return: 				Position (x,y) of the GCP center in the input image.
	"""

	def get_ssim_map(ker: np.array, search_area: np.array):
		pixel_score = np.zeros([search_area.shape[0], search_area.shape[1]])

		for i in range(k_span, search_area.shape[1] - k_span):
			for j in range(k_span, search_area.shape[0] - k_span):
				subarea = cv2.getRectSubPix(search_area, (k_size, k_size), (i, j))
				pixel_score[i, j] = ssim(subarea, ker)

		if show:
			plt.imshow(pixel_score)
			plt.show()

		return pixel_score

	score_map = get_ssim_map(kernel, single_ch)

	try:
		score_max = np.max(score_map)
	except ValueError:
		score_max = 0
		xpix, ypix = 0, 0
		return (xpix, ypix), score_max

	x_pix, y_pix = np.unravel_index(np.argmax(score_map), score_map.shape)

	if subpixel:
		subpixel_area = subpixel_ksize // 2
		params = fit_gaussian_2d(score_map[x_pix - subpixel_area: x_pix + subpixel_area + 1,
										   y_pix - subpixel_area: y_pix + subpixel_area + 1])
		h, dx, dy, wx, wy = params
		x_sub, y_sub = x_pix + dx - subpixel_area, y_pix + dy - subpixel_area

		return (x_sub, y_sub), score_max

	else:
		return (x_pix, y_pix), score_max


def gaussian_2d_func(height, center_x, center_y, stddev_x, stddev_y):
	"""
	Returns a gaussian_2d_func function with the given parameters.

	:param height:		Peak of the 2D Gaussian.
	:param center_x:	X position of the Gaussian peak.
	:param center_y:	Y position of the Gaussian peak.
	:param stddev_x:	Standard deviation in X direction.
	:param stddev_y:	Standard deviation in Y direction.
	:return:			Generator function for 2D Gaussian.
	"""

	stddev_x = float(stddev_x)
	stddev_y = float(stddev_y)

	return lambda x, y: height*np.exp(-(((center_x-x) / stddev_x) ** 2 + ((center_y - y) / stddev_y) ** 2) / 2)


def moments(data):
	"""Returns the 2D Gaussian parameters by calculating its moments.

	:param data:	Data to fit 2D Gaussian to.
	:return:		Height, x, y, stddev_x, stddev_y
	"""

	total = data.sum()
	X, Y = np.indices(data.shape)
	x = (X * data).sum()/total
	y = (Y * data).sum()/total
	col = data[:, int(y)]
	stddev_x = np.sqrt(np.abs((np.arange(col.size) - y) ** 2 * col).sum() / col.sum())
	row = data[int(x), :]
	stddev_y = np.sqrt(np.abs((np.arange(row.size) - x) ** 2 * row).sum() / row.sum())
	height = data.max()

	return height, x, y, stddev_x, stddev_y


def fit_gaussian_2d(data):
	"""Fits a 2D Gaussian to data using least square minimization.

	:param data:	Data to fit 2D Gaussian to.
	:return:		Parameters of the least-square-fitted 2D Gaussian.
	"""

	params = moments(data)
	errorfunction = lambda p: np.ravel(gaussian_2d_func(*p)(*np.indices(data.shape)) - data)
	p, *_ = leastsq(errorfunction, params)

	return p


def fresh_folder(path):
	if not exists(path):
		makedirs(path)
	else:
		files = glob.glob('{}/*'.format(path))
		for f in files:
			remove(f)


def time_hms(time):
	hours = int(time // 3600)
	minutes = int((time - hours * 3600) // 60)
	seconds = int(time % 60)

	return hours, minutes, seconds


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
			input('[ERROR] There was a problem reading the configuration file!\nCheck if project has valid configuration.')
			exit()

		section = 'Basic'

		# Folder with raw frames
		frames_folder = cfg.get(section, 'InputFolder')

		# Folder for result output
		results_folder = cfg.get(section, 'OutputFolder')

		try:
			copy(args.cfg, results_folder)
		except SameFileError:
			pass

		# Extension for image files
		ext = cfg.get(section, 'ImageExtension', fallback='jpg')

		# Search area size (high cost)
		search_size = int(cfg.get(section, 'SearchAreaSize', fallback='21'))

		# Interrogation area size (low cost)
		k_size = int(cfg.get(section, 'InterrogationAreaSize', fallback='11'))
		k_span = k_size // 2

		# Optional, to improve accuracy (low cost)
		subp = int(cfg.get(section, 'Subpixel', fallback='3'))

		section = 'Advanced'

		# Expand the search area width/height if SSIM score is below :expand_ssim_thr:
		expand_ssim_search = int(cfg.get(section, 'ExpandSA', fallback='0'))

		# Search area expansion factor (high cost)
		expand_coef = float(cfg.get(section, 'ExpandSACoef', fallback='2.0'))

		# SSIM score threshold for expanded search
		expand_ssim_thr = float(cfg.get(section, 'ExpandSAThreshold', fallback='0.5'))

		if subp > 1:
			subpixel = True
			# Use 3-7 px, must be odd
			subpixel_ksize = subp
		else:
			subpixel = False
			subpixel_ksize = 1

		# If significant image rotation is expected
		update_kernels = int(cfg.get(section, 'UpdateKernels', fallback='0'))

		# Whether to obtain new GCPs or use previous from the starting frame (depreciated)
		get_new_gcps = True

		# Do not change from this point on ------------------------------------------------------------
		assert search_size > 0 and search_size % 2 == 1 and type(search_size) == int, \
			'[ERROR] Search area size must be a positive odd integer!'
		assert 0 < k_size < search_size and k_size % 2 == 1 and type(k_size) == int, \
			'[ERROR] Interrogation area size (kernel size) must be a positive odd integer, and smaller than search area size!'
		assert expand_coef > 1, \
			'[ERROR] Search area expansion coefficient must be higher than 1!'
		assert 0 < expand_ssim_thr < 1, \
			'[ERROR] Search area expansion threshold must be in range (0, 1)!'
		assert 0 < subpixel_ksize < k_size and subpixel_ksize % 2 == 1 and type(subpixel_ksize) == int, \
			'[ERROR] Subpixel estimator size must be a positive odd integer and smaller than investigation area size!'

		raw_frames_list = glob.glob('{}/*.{}'.format(frames_folder, ext))
		num_raw_frames = len(raw_frames_list)
		numbering_len = int(log(num_raw_frames, 10)) + 1

		is_expanded_search = False
		init_search_size = search_size
		exp_search_size = to_odd(search_size*expand_coef)

		img_path = raw_frames_list[0]
		img = cv2.imread(img_path)
		img_rgb = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)

		show_legend = True
		legend_toggle = None
		markers = get_gcps_from_image(img_rgb, verbose=True, ia=k_size, sa=search_size)
		cfg['Basic']['SearchAreaSize'] = str(search_size)
		cfg['Basic']['InterrogationAreaSize'] = str(k_size)
		
		with open(args.cfg, 'w') as configfile:
			cfg.write(configfile)

		markers_mask = [1] * len(markers)

		folders_to_check = ['{}/gcps_csv'.format(results_folder),
							'{}/gcps_img'.format(results_folder),
							'{}/kernels'.format(results_folder)]

		for f in folders_to_check:
			fresh_folder(f)

		try:
			log_path = '{}/log_gcps.txt'.format(results_folder)
			logger = Logger(log_path)
			logger.log(separator)
			logger.log('[LOG] Log file {}/'.format(log_path))

			kernels = []
			img = cv2.cvtColor(img, cv2.COLOR_RGB2LAB)
			img_ch, *_ = cv2.split(img)

			for m in markers:
				k = cv2.getRectSubPix(img_ch, (k_size, k_size), (m[0], m[1]))
				cv2.imwrite('{}/kernels/{}.{}'.format(results_folder, len(kernels), ext), k)
				kernels.append(k)

			times = deque(maxlen=10)
			script_start = time()

			ssim_scores = np.zeros([num_raw_frames, len(markers)])

			for n in range(num_raw_frames):
				try:
					logger.log(separator)
					logger.log('[INFO] Image: {}/{} ({:.1f}%)'.format(n, num_raw_frames - 1, n/(num_raw_frames - 1) * 100))
					frame_start = time()

					img_path = raw_frames_list[n]
					img = cv2.imread(img_path)

					img = cv2.cvtColor(img, cv2.COLOR_BGR2LAB)
					img_ch, *_ = cv2.split(img)

					for j in range(len(markers)):
						xx, yy = markers[j]

						if xx != 0 and yy != 0:
							search_space = cv2.getRectSubPix(img_ch, (search_size, search_size), (xx, yy))

							rel_center, ssim_max = find_gcp(search_space,
															kernels[j],
															show=False,
															subpixel=subpixel,
															subpixel_ksize=subpixel_ksize)

							if expand_ssim_search and ssim_max < expand_ssim_thr:
								logger.log('[WARNING] Expanding the search area, SSIM={:.3f} < {:.3f}'.format(ssim_max, expand_ssim_thr))
								is_expanded_search = True
								search_size = exp_search_size
								search_space = cv2.getRectSubPix(img_ch, (search_size, search_size), (xx, yy))

								rel_center, ssim_max = find_gcp(search_space,
														 kernels[j],
														 show=False,
														 subpixel=subpixel,
														 subpixel_ksize=subpixel_ksize)

							# cx, cy = [to_int(x) for x in rel_center]

							# cv2.circle(search_space, (cx, cy), 1, (0, 0, 255), -1)
							# cv2.circle(search_space, (search_size // 2 + 1, search_size // 2 + 1), 1, (255, 0, 0))

							real_x = rel_center[0] + xx - (search_size - 1) / 2
							real_y = rel_center[1] + yy - (search_size - 1) / 2

							markers[j] = [real_x, real_y]
							ssim_scores[n, j] = ssim_max

							if is_expanded_search:
								is_expanded_search = False
								search_size = init_search_size

							if update_kernels:
								kernels[j] = cv2.getRectSubPix(img_ch, (k_size, k_size), (real_x, real_y))

							try:
								plt.imsave(
									'{}/gcps_img/{}_{}.{}'.format(results_folder, str(n).rjust(numbering_len, '0'), j, ext),
									cv2.getRectSubPix(img_ch, (search_size, search_size), (real_x, real_y)))
							except SystemError:
								logger.log('[WARNING] Marker {} lost! Setting coordinates to (0, 0).'.format(j))
								markers[j] = [0, 0]
								markers_mask[j] = 0
								plt.imsave(
									'{}/gcps_img/{}_{}.{}'.format(results_folder, str(n).rjust(numbering_len, '0'), j, ext),
									np.zeros([search_size, search_size]))

						else:
							logger.log('[WARNING] Marker {} lost! Setting coordinates to (0, 0).'.format(j))
							markers[j] = [0, 0]
							markers_mask[j] = 0

				except (AttributeError, IOError, IndexError):
					break

				dt = time() - frame_start
				times.append(dt)

				elapsed = time() - script_start
				remaining = np.mean(times) * (num_raw_frames - n)

				np.savetxt('{}/gcps_csv/{}.txt'.format(results_folder, str(n).rjust(numbering_len, '0')), markers, fmt='%.3f', delimiter=' ')
				np.savetxt('{}/ssim_scores.txt'.format(results_folder), ssim_scores, fmt='%.3f', delimiter=' ')

				logger.log('[INFO] Markers: {}'.format([[round(x, 3), round(y, 3)] for x, y in markers]))
				logger.log('[INFO] Frame processing time = {:.3f} sec'.format(dt))
				hours, minutes, seconds = time_hms(elapsed)
				logger.log('[INFO] Elapsed time = {} h {} min {} sec'.format(hours, minutes, seconds))
				hours, minutes, seconds = time_hms(remaining)
				logger.log('[INFO] Remaining time ~ {} h {} min {} sec'.format(hours, minutes, seconds))

		except IOError:
			try:
				logger.close()
			except IOError:
				exit()

		else:
			logger.close()

		np.savetxt('{}/markers_mask.txt'.format(results_folder), markers_mask, fmt='%d', delimiter=' ')

		print('\a')
		input('\nPress any key to exit...')

	except Exception as ex:
		print('\n[EXCEPTION] The following exception has occurred: \n\n'
			  '  {}'.format(ex))
		input('\nPress any key to exit...')
