#!/usr/bin/env python3
# -*- coding: utf-8 -*-

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
	from os import path, makedirs
	from scipy.signal import gaussian, convolve2d
	from matplotlib.widgets import Slider

	import inspect
	import glob
	import matplotlib.pyplot as plt
	import mplcursors

except Exception as ex:
	print('\n[EXCEPTION] Import failed: \n\n'
	      '  {}'.format(ex))
	input('\nPress ENTER/RETURN to exit...')
	exit()

separator = '---'


def addBackgroundImage(fore: np.ndarray, back: np.ndarray) -> np.ndarray:
	background = back.copy().astype(float)
	alpha = fore

	try:  # Try to read the third dimension of an array. If fails, the array is 2D.
		background.shape[2]
		dimension = 3
	except IndexError:
		dimension = 2

	foreground = fore.copy().astype(float)
	alpha = alpha.copy().astype(float) / 255

	if dimension == 3:
		if len(foreground.shape) == 2:
			# Color me purple
			foreground = np.stack((foreground,) * 3, -1)
			foreground = np.where(foreground == [0., 0., 0.], [0., 0., 0.], [0., 0., 0.])
			alpha = np.stack((alpha,) * 3, -1)
		else:
			foreground = np.where(foreground == [0., 0., 0.], [0., 0., 0.], [0., 0., 0.])

	foreground = cv2.multiply(alpha, foreground)
	alpha = 1.0 - alpha
	background = cv2.multiply(alpha, background)
	combined = cv2.add(foreground, background)

	return combined.astype('uint8')


def func(name, image, params):
	return name(image, *params)


def histeq(img):
	print('[FILTER] Histogram equalization')
	eq = cv2.equalizeHist(cv2.cvtColor(img, cv2.COLOR_BGR2GRAY))
	return cv2.cvtColor(eq, cv2.COLOR_GRAY2BGR)


def clahe(img, clip=2.0, tile=8):
	print('[FILTER] CLAHE: clip={:.1f}, tile={:.0f}'.format(clip, tile))
	clahe = cv2.createCLAHE(clipLimit=clip, tileGridSize=(int(tile), int(tile)))
	return cv2.cvtColor(clahe.apply(cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)), cv2.COLOR_GRAY2BGR)


def denoise(img, h=3, hcolor=3, template_size=7, search_size=21):
	print('[FILTER] Denoise: h={:.0f}, hcolor={:.0f}, template_size={:.0f}, search_size={:.0f}'.format(h, hcolor, template_size, search_size))
	if template_size % 2 == 1:
		template_size += 1
	if search_size % 2 == 1:
		search_size += 1
	return cv2.fastNlMeansDenoisingColored(img, None, int(h), int(hcolor), int(template_size), int(search_size))


def hsv_filter(img, hu=255, hl=0, su=255, sl=0, vu=255, vl=0):
	print('[FILTER] HSV: Hu={:.0f}, Hl={:.0f}, Su={:.0f}, Sl={:.0f}, Vu={:.0f}, Vl={:.0f}'.format(hu, hl, su, sl, vu, vl))
	img = cv2.cvtColor(img, cv2.COLOR_BGR2HSV)
	mask = cv2.inRange(img, (hl, sl, vl), (hu, su, vu))
	return mask


def brightness_contrast(img, alpha=1.0, beta=0.0):
	print('[FILTER] Brightness and contrast: alpha={:.1f}, beta={:.1f}'.format(alpha, beta))
	new = cv2.convertScaleAbs(img, alpha=alpha, beta=beta)
	return new


def gamma(img, gamma=1.0):
	print('[FILTER] Gamma correction: gamma={:.1f}'.format(gamma))
	invGamma = 1.0 / gamma
	table = np.array([((i / 255.0) ** invGamma) * 255
		for i in np.arange(0, 256)]).astype("uint8")
	return cv2.LUT(img, table)


def modify_channels(img, r=1.0, g=1.0, b=1.0):
	print('[FILTER] Modify channels: c1={:.1f}, c2={:.1f}, c3={:.1f}'.format(r, g, b))
	img_r = cv2.convertScaleAbs(img[:, :, 2], alpha=r, beta=0)
	img_g = cv2.convertScaleAbs(img[:, :, 1], alpha=g, beta=0)
	img_b = cv2.convertScaleAbs(img[:, :, 0], alpha=b, beta=0)

	if g == 0 and b == 0:
		img_g, img_b = img_r, img_r
	elif r == 0 and b == 0:
		img_r, img_b = img_g, img_g
	elif r == 0 and g == 0:
		img_r, img_g = img_b, img_b

	return np.dstack([img_b, img_g, img_r])


def grayscale(img):
	print('[FILTER] Convert to grayscale')
	return cv2.cvtColor(cv2.cvtColor(img, cv2.COLOR_BGR2GRAY), cv2.COLOR_GRAY2BGR)


def negative(img):
	print('[FILTER] Convert to image negative')
	return ~img


def highpass(img, sigma=51):
	print('[FILTER] Highpass filter: sigma={:.0f}'.format(sigma))
	if sigma % 2 == 1:
		sigma += 1
	new = img - cv2.GaussianBlur(img, (0, 0), int(sigma)) + 127

	return new


def laplacian(img):
	print('[FILTER] Laplacian of an image')
	new = cv2.Laplacian(cv2.cvtColor(img, cv2.COLOR_BGR2GRAY), cv2.CV_8U, ksize=3)
	return cv2.cvtColor(new, cv2.COLOR_GRAY2BGR)


def intensity_capping(img, n_std=2):
	print('[FILTER] Pixel intensity capping: n_std='.format(n_std))
	img_g = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
	median = np.median(img_g)
	stdev = np.std(img_g)
	cap = median + n_std * stdev

	img_g[img_g > cap] = cap

	return cv2.cvtColor(img_g, cv2.COLOR_GRAY2BGR)


def remove_background(img, num_imgs=10):
	print('[FILTER] Remove image background: num_imgs={:.0f}'.format(num_imgs))
	num_imgs = int(num_imgs)
	new = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY).astype('int')
	h, w = new.shape

	if len(img_list) < num_imgs:
		num_imgs = len(img_list)

	back_path = r'{}/../median_{}.{}'.format(path.dirname(img_list[0]), num_imgs, args.ext)

	if path.exists(back_path):
		back = cv2.imread(back_path, 0).astype('int')
	else:
		stack = np.ndarray([h, w, num_imgs], dtype='int')

		for i in range(num_imgs):
			stack[:, :, i] = cv2.imread(img_list[i], 0)

		back = np.median(stack, axis=2).astype('int')
		cv2.imwrite(back_path, back.astype('uint8'))

	new -= back
	new[new < 0] = 0
	new[new > 255] = 255

	return cv2.cvtColor(new.astype('uint8'), cv2.COLOR_GRAY2BGR)


def params_to_list(params):
	if params == '':
		return []
	else:
		return [float(x) for x in params.split(',')]


def keypress(event):
	global is_original

	if event.key == ' ':
		if is_original:
			img_shown.set_data(img_rgb)
		else:
			img_shown.set_data(original)

		is_original = not is_original
		plt.draw()

	elif event.key == 'escape':
		exit()


def update_frame(val):
	global original
	global img
	global img_rgb

	original = cv2.imread(img_list[sl_ax_frame_num.val])
	img, img_rgb = apply_filters(original, filters_data)

	if is_original:
		img_shown.set_data(img_rgb)
	else:
		img_shown.set_data(img)

	plt.draw()
	return


def apply_filters(img, filters_data):
	for i in range(filters_data.shape[0]):
		img = func(globals()[filters_data[i][0]], img, params_to_list(filters_data[i][1]))
		img_rgb = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)

	legend = 'Filters:'
	for i in range(filters_data.shape[0]):
		func_args_names = globals()[filters_data[i][0]]
		func_args = inspect.getfullargspec(func_args_names)[0][1:] if filters_data[i][1] != '' else []
		legend_values = ['{}={}'.format(p, v) for p, v in zip(func_args, filters_data[i][1].split(','))]
		legend += '\n    ' + filters_data[i][0] + ': ' + ', '.join(legend_values if filters_data[i][1] != '' else '')

	legend_toggle = plt.text(0.02, 0.97, legend,
	                         horizontalalignment='left',
	                         verticalalignment='top',
	                         transform=ax.transAxes,
	                         bbox=dict(facecolor='white', alpha=0.5),
	                         fontsize=9,
	                         )

	return img, img_rgb


if __name__ == '__main__':
	try:
		parser = ArgumentParser()
		parser.add_argument('--folder', type=str, help='Path to frames folder')
		parser.add_argument('--ext', type=str, help='Frames\' extension', default='jpg')
		parser.add_argument('--multi', type=int, help='Path to filter list file', default=0)
		args = parser.parse_args()

		img_list = glob.glob(r'{}/*.{}'.format(args.folder, args.ext))
		num_frames = len(img_list)
		filters_data = np.loadtxt(args.folder + '/filters.txt', dtype='str', delimiter=r'/', ndmin=2)

		fig, ax = plt.subplots()
		fig.canvas.mpl_connect('key_press_event', keypress)
		plt.subplots_adjust(bottom=0.13)
		plt.axis('off')

		axcolor = 'lightgoldenrodyellow'
		valfmt = "%d"

		ax_frame_num = plt.axes([0.2, 0.05, 0.63, 0.03], facecolor=axcolor)
		sl_ax_frame_num = Slider(ax_frame_num, f'Frame #\n({num_frames} total)', 0, num_frames - 1, valinit=0, valstep=1, valfmt=valfmt)
		sl_ax_frame_num.on_changed(update_frame)

		if args.multi == 0:
			img_path = img_list[0]
			img = cv2.imread(img_path)
			original = cv2.cvtColor(cv2.imread(img_path), cv2.COLOR_BGR2RGB)
			is_original = False

			img, img_rgb = apply_filters(img, filters_data)

			try:
				mng = plt.get_current_fig_manager()
				mng.window.state('zoomed')
				mng.set_window_title('Filtering')
			except:
				pass

			ax.set_title('Use SPACE to toggle between original and filtered image, and Q or ESC to exit')
			ax.axis('off')
			img_shown = ax.imshow(img_rgb)
			plt.show()
			exit()

		else:
			filtered_folder = args.folder + '_filtered'

			print('[BEGIN] :STARTING FILTERING: '.ljust(len(separator), '-'))
			print(' [INFO] Filtering frames from folder', args.folder + '/')
			print(' [INFO] Filters to apply:', [row[0] for row in filters_data])

			if not path.exists(filtered_folder):
				makedirs(filtered_folder)

			for j in range(len(img_list)):
				img_path = img_list[j]
				img = cv2.imread(img_path)

				for i in range(filters_data.shape[0]):
					img = func(locals()[filters_data[i][0]], img, params_to_list(filters_data[i][1]))

				cv2.imwrite('{}/{}'.format(filtered_folder, path.basename(img_path)), img)

				print(' [INFO] Filtering frame {}/{} ({:.1f}%)'.format(j, num_frames - 1, j/(num_frames - 1) * 100))

			print('  [END] Filtering complete!')
			print('  [END] Results available in folder [{}]!'.format(filtered_folder))
			input('\nPress ENTER/RETURN to exit...')

	except Exception as ex:
		print('\n[EXCEPTION] The following exception has occurred: \n\n'
		      '  {}'.format(ex))
		input('\nPress ENTER/RETURN to exit...')
