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
	from matplotlib.widgets import Slider
	from class_console_printer import Console_printer, tag_string
	from class_progress_bar import Progress_bar
	from glob import glob
	from inspect import getfullargspec

	import matplotlib.pyplot as plt
	import scipy.stats as stats

except Exception as ex:
	print()
	print(tag_string('exception', 'Import failed: \n'))
	print('  {}'.format(ex))
	input('\nPress ENTER/RETURN key to exit...')
	exit()

colormap = 'viridis'

colorspaces_list = ['rgb', 'hsv', 'lab', 'grayscale']
color_conv_codes = [
	[[], 	[41], 		[45], 		[7]],
	[[55], 	[], 		[55, 45], 	[55, 7]],
	[[57], 	[57, 41], 	[], 		[57, 7]],
	[[8], 	[8, 41], 	[8, 45], 	[]]
]


def convert_img(img: str, from_cs: str, to_cs: str) -> np.ndarray:
	"""
	Convert image from one colorspace to another. Extends cv2.cvtColor() to
	encompass all transformation possibilities between RGB, HSV, LAB, and grayscale.
	"""
	
	from_cs_index = colorspaces_list.index(from_cs)
	to_cs_index = colorspaces_list.index(to_cs)
	conv_codes = color_conv_codes[from_cs_index][to_cs_index]

	if to_cs == from_cs:
		return img	

	if from_cs == 'grayscale':
		img = img[:, :, 0]
	
	if len(conv_codes) == 0:
		return img
	
	for code in conv_codes:
		img = cv2.cvtColor(img, code)
	
	return img


def is_grayscale(img: np.ndarray) -> bool:
	"""
	Checks whether all three image channels are identical,
	i.e., if image is grayscale.
	"""
	
	if (img[:, :, 0] == img[:, :, 1]).all() and (img[:, :, 0] == img[:, :, 2]).all():
		return True
	else:
		return False


def func(name, image, params):
	"""
	Template function for all filtering functions.
	"""
	return name(image, *params)
	
	
def negative(img):
	print(tag_string('info', 'Convert to image negative'))
	return ~img


def to_grayscale(img):
	img_gray = convert_img(img, colorspace, 'grayscale')
	print(tag_string('info', 'Convert to grayscale'))
	return convert_img(img_gray, 'grayscale', colorspace)


def to_rgb(img):
	print(tag_string('info', 'Convert to RGB colorspace'))
	return convert_img(img, colorspace, 'rgb')
	
	
def to_hsv(img):
	print(tag_string('info', 'Convert to HSV colorspace'))
	return convert_img(img, colorspace, 'hsv')
	

def to_lab(img):
	print(tag_string('info', 'Convert to L*a*b* colorspace'))
	return convert_img(img, colorspace, 'lab')
	

def select_channel(img, channel=1):
	global colorspace

	try:
		img_single = img[:, :, int(channel)-1]
	except IndexError:
		print(tag_string('error', 'Image is already single channel, cannot select channel {:.0f}'.format(channel)))
		return img

	colorspace = 'grayscale'
	
	print(tag_string('info', 'Selecting channel {:.0f}'.format(channel)))
	return cv2.merge([img_single, img_single, img_single])
	
	
def highpass(img, sigma=51):
	if sigma % 2 == 0:
		sigma += 1

	img_gray = convert_img(img, colorspace, 'grayscale')[:, :, 0]
	blur = cv2.GaussianBlur(img_gray, (0, 0), int(sigma))
	img_highpass = ~cv2.subtract(cv2.add(blur, 127), img_gray)

	print(tag_string('info', 'Highpass filter: sigma={:.0f}'.format(sigma)))
	return cv2.merge([img_highpass, img_highpass, img_highpass])
	
	
def normalize_image(img, lower=None, upper=None):
	if lower is None:
		lower = np.min(img)
	if upper is None:
		upper = np.max(img)

	img_c = img.astype(int)

	img_c = ((img_c - np.min(img_c)) / (np.max(img_c) - np.min(img_c)) * 255).astype('uint8')

	return img_c


def intensity_capping(img, n_std=0.0):
	img_gray = convert_img(img, colorspace, 'grayscale')
	
	median = np.median(img_gray)
	stdev = np.std(img_gray)
	cap = median - n_std * stdev

	img_gray[img_gray > cap] = cap
	img_gray = normalize_image(img_gray, cap, np.max(img_gray))

	print(tag_string('info', 'Pixel intensity capping: n_std={:.1f}'.format(n_std)))
	return convert_img(img_gray, 'grayscale', colorspace)
	
	
def brightness_contrast(img, alpha=1.0, beta=0.0):
	print(tag_string('info', 'Brightness and contrast: alpha={:.1f}, beta={:.1f}'.format(alpha, beta)))
	return cv2.convertScaleAbs(img, alpha=alpha, beta=beta)


def gamma(img, gamma=1.0):
	invGamma = 1.0 / gamma
	
	table = np.array([((i / 255.0) ** invGamma) * 255
		for i in np.arange(0, 256)]).astype("uint8")
		
	print(tag_string('info', 'Gamma correction: gamma={:.1f}'.format(gamma)))
	return cv2.LUT(img, table)
	
	
def gaussian_lookup(img, sigma=51):
	x = np.arange(0, 256)
	pdf = stats.norm.pdf(x, 127, sigma)

	cdf = np.cumsum(pdf)
	cdf_norm = np.array([(x - np.min(cdf))/(np.max(cdf) - np.min(cdf)) * 255 for x in cdf]).astype('uint8')

	print(tag_string('info', 'Gaussian lookup filter: sigma={}'.format(sigma)))
	return cv2.LUT(img, cdf_norm)
	
	
def thresholding(img, c1u=255, c1l=0, c2u=255, c2l=0, c3u=255, c3l=0):
	mask = cv2.inRange(img, (c1l, c2l, c3l), (c1u, c2u, c3u))
	
	print(tag_string('info', 'Thresholding: Channel 1: [{}, {}], Channel 2: [{}, {}], Channel 3: [{}, {}]'.format(c1u, c1l, c2u, c2l, c3u, c3l)))
	return mask


def denoise(img, ksize=3):
	print(tag_string('info', 'Denoise: ksize={}'.format(ksize)))
	return cv2.medianBlur(img, ksize)


def remove_background(img, num_frames_background=10):
	num_frames_background = int(num_frames_background)
	h, w = img.shape[:2]

	if len(img_list) < num_frames_background:
		num_frames_background = len(img_list)

	step = len(img_list) // num_frames_background
	img_back_path = '{}/../median_{}.{}'.format(path.dirname(img_list[0]), num_frames_background, args.ext)

	if path.exists(img_back_path):
		back = cv2.imread(img_back_path)
	else:
		stack = np.ndarray([h, w, 3, num_frames_background], dtype='uint8')

		for i in range(num_frames_background):
			stack[:, :, :, i] = cv2.imread(img_list[i*step])

		back = np.median(stack, axis=3)
		cv2.imwrite(img_back_path, back)

	print(tag_string('info', 'Remove image background: num_frames_background={:.0f}'.format(num_frames_background)))
	return cv2.subtract(back, img)


def histeq(img):
	img_gray = convert_img(img, colorspace, 'grayscale')
	eq = cv2.equalizeHist(img_gray)
	
	print(tag_string('info', 'Histogram equalization'))
	return convert_img(eq, 'grayscale', colorspace)


def clahe(img, clip=2.0, tile=8):
	clahe = cv2.createCLAHE(clipLimit=clip, tileGridSize=(int(tile), int(tile)))
	img_gray = convert_img(img, colorspace, 'grayscale')
	img_clahe = clahe.apply(img_gray)
	
	print(tag_string('info', 'CLAHE: clip={:.1f}, tile={:.0f}'.format(clip, tile)))
	return convert_img(img_clahe, 'grayscale', colorspace)


def params_to_list(params: str) -> list:
	"""
	Splits filter parameters into a list.
	"""
	
	if params == '':
		return []
	else:
		return [float(x) for x in params.split(',')]


def keypress(event):
	global is_original

	if event.key == ' ':
		if is_original:
			img_shown.set_data(img[:, :, 0] if is_grayscale(img) else img)
		else:
			img_shown.set_data(original)

		is_original = not is_original
		plt.draw()

	elif event.key == 'escape':
		exit()


def update_frame(val):
	global original
	global img

	original = cv2.imread(img_list[sl_ax_frame_num.val])
	original = cv2.cvtColor(original, cv2.COLOR_BGR2RGB)
	
	img = apply_filters(original, filters_data)

	if is_original:
		img_shown.set_data(original)
	else:
		img_shown.set_data(img[:, :, 0] if is_grayscale(img) else img)

	plt.draw()
	return


def apply_filters(img: np.ndarray, filters_data: np.ndarray) -> np.ndarray:
	"""
	Applies multiple filters consecutively using a template function.
	"""

	global colorspace
	
	for i in range(filters_data.shape[0]):
		img = func(globals()[filters_data[i][0]], img, params_to_list(filters_data[i][1]))
		
		if filters_data[i][0].startswith('to_'):
			colorspace = filters_data[i][0].split('_')[1]

	return img


if __name__ == '__main__':
	try:
		parser = ArgumentParser()
		parser.add_argument('--folder', type=str, help='Path to frames folder')
		parser.add_argument('--ext', type=str, help='Frames\' extension', default='jpg')
		parser.add_argument('--multi', type=int, help='Path to filter list file', default=0)
		args = parser.parse_args()

		img_list = glob('{}/*.{}'.format(args.folder, args.ext))
		num_frames = len(img_list)
		filters_data = np.loadtxt(args.folder + '/filters.txt', dtype='str', delimiter='/', ndmin=2)

		fig, ax = plt.subplots()
		fig.canvas.mpl_connect('key_press_event', keypress)
		plt.subplots_adjust(bottom=0.13)
		plt.axis('off')

		axcolor = 'lightgoldenrodyellow'
		valfmt = "%d"

		ax_frame_num = plt.axes([0.2, 0.05, 0.63, 0.03], facecolor=axcolor)
		sl_ax_frame_num = Slider(ax_frame_num, f'Frame #\n({num_frames} total)', 0, num_frames - 1, valinit=0, valstep=1, valfmt=valfmt)
		sl_ax_frame_num.on_changed(update_frame)

		progress_bar = Progress_bar(total=num_frames, prefix=tag_string('info', 'Frame '))
		printer = Console_printer()

		print(tag_string('start', 'Frame filtering'))

		if args.multi == 0:
			img_path = img_list[0]
			img = cv2.cvtColor(cv2.imread(img_path), cv2.COLOR_BGR2RGB)
			colorspace = 'rgb'

			original = img.copy()
			is_original = False

			img = apply_filters(img, filters_data)

			try:
				mng = plt.get_current_fig_manager()
				mng.window.state('zoomed')
				mng.set_window_title('Filtering')
			except Exception:
				pass

			ax.set_title('Use SPACE to toggle between original and filtered image, and Q or ESC to exit')
			ax.axis('off')

			if is_grayscale(img):
				img_shown = ax.imshow(img[:, :, 0], cmap=colormap)
			else:
				img_shown = ax.imshow(img)

			legend = 'Filters:'

			for i in range(filters_data.shape[0]):
				func_args_names = globals()[filters_data[i][0]]		
				func_args = getfullargspec(func_args_names)[0][1:] if filters_data[i][1] != '' else []
				legend_values = ['{}={}'.format(p, v) for p, v in zip(func_args, filters_data[i][1].split(','))]
				legend += '\n    ' + filters_data[i][0] + ': ' + ', '.join(legend_values if filters_data[i][1] != '' else '')

			plt.text(0.02, 0.97, legend,
					horizontalalignment='left',
					verticalalignment='top',
					transform=ax.transAxes,
					bbox=dict(facecolor='white', alpha=0.5),
					fontsize=9,
					)

			plt.show()
			exit()

		else:
			filtered_folder = args.folder + '_filtered'

			print(tag_string('info', 'Filtering frames from folder [{}]'.format(args.folder)))
			print(tag_string('info', 'Filters to apply:'), [row[0] for row in filters_data])
			print()

			if not path.exists(filtered_folder):
				makedirs(filtered_folder)

			for j in range(len(img_list)):
				img_path = img_list[j]
				img = cv2.imread(img_path)
				img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
				colorspace = 'rgb'

				printer.single_line(progress_bar.get(j))

				for i in range(filters_data.shape[0]):
					img = func(locals()[filters_data[i][0]], img, params_to_list(filters_data[i][1]))

					if filters_data[i][0].startswith('to_'):
						colorspace = filters_data[i][0].split('_')[1]

				img_rgb = convert_img(img, colorspace, 'rgb')
				img_bgr = cv2.cvtColor(img_rgb, cv2.COLOR_RGB2BGR)

				cv2.imwrite('{}/{}'.format(filtered_folder, path.basename(img_path)), img_bgr)

				printer.clear_lines(filters_data.shape[0])
				printer.single_line(progress_bar.get(j))

			print()
			print(tag_string('end', 'Filtering complete!'))
			print(tag_string('end', 'Results available in folder [{}]'.format(filtered_folder)))
			print('\a')
			input('\nPress ENTER/RETURN to exit...')
	
	except Exception as ex:
		print()
		print(tag_string('exception', 'The following exception has occurred: \n'))
		print('  {}'.format(ex))
		input('\nPress ENTER/RETURN key to exit...')
