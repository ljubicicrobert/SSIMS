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

	import glob
	import matplotlib.pyplot as plt
	import mplcursors

except Exception as ex:
	print('\n[EXCEPTION] Import failed: \n\n'
	      '  {}'.format(ex))
	input('\nPress any key to exit...')
	exit()

separator = '---'


def func(name, image, params):
	return name(image, *params)


def histeq(img):
	eq = cv2.equalizeHist(cv2.cvtColor(img, cv2.COLOR_BGR2GRAY))
	return cv2.cvtColor(eq, cv2.COLOR_GRAY2BGR)


def clahe(img, clip=2.0, tile=8):
	clahe = cv2.createCLAHE(clipLimit=clip, tileGridSize=(int(tile), int(tile)))
	return cv2.cvtColor(clahe.apply(cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)), cv2.COLOR_GRAY2BGR)


def denoise(img, h=3, hcolor=3, template_size=7, search_size=21):
	if template_size % 2 == 1:
		template_size += 1
	if search_size % 2 == 1:
		search_size += 1
	return cv2.fastNlMeansDenoisingColored(img, None, int(h), int(hcolor), int(template_size), int(search_size))


def hsv_filter(img, hu=255, hl=0, su=255, sl=0, vu=255, vl=0):
	img = cv2.cvtColor(img, cv2.COLOR_BGR2HSV)
	new = cv2.inRange(img, (hu, su, vu), (hl, sl, vl))
	return cv2.cvtColor(new, cv2.COLOR_GRAY2BGR)


def brightness_contrast(img, alpha=1.0, beta=0.0):
	new = cv2.convertScaleAbs(img, alpha=alpha, beta=beta)
	return new


def gamma(img, gamma=1.0):
	invGamma = 1.0 / gamma
	table = np.array([((i / 255.0) ** invGamma) * 255
		for i in np.arange(0, 256)]).astype("uint8")
	return cv2.LUT(img, table)


def modify_channels(img, r=1.0, g=1.0, b=1.0):
	img_r = cv2.convertScaleAbs(img[:, :, 2], alpha=r, beta=0)
	img_g = cv2.convertScaleAbs(img[:, :, 1], alpha=g, beta=0)
	img_b = cv2.convertScaleAbs(img[:, :, 0], alpha=b, beta=0)
	return np.dstack([img_b, img_g, img_r])


def grayscale(img):
	return cv2.cvtColor(cv2.cvtColor(img, cv2.COLOR_BGR2GRAY), cv2.COLOR_GRAY2BGR)


def highpass(img, sigma=51):
	if sigma % 2 == 1:
		sigma += 1
	return img - cv2.GaussianBlur(img, (0, 0), int(sigma)) + 127


def laplacian(img):
	new = cv2.Laplacian(cv2.cvtColor(img, cv2.COLOR_BGR2GRAY), cv2.CV_8U, ksize=3)
	return cv2.cvtColor(new, cv2.COLOR_GRAY2BGR)


def params_to_list(params):
	if params == '':
		return []
	else:
		return [float(x) for x in params.split(',')]


def keypress(event):
	global is_original

	if event.key == ' ':
		if not is_original:
			img_shown.set_data(original)
		else:
			img_shown.set_data(img)

		plt.draw()
		is_original = not is_original

	elif event.key == 'escape':
		exit()


if __name__ == '__main__':
	try:
		parser = ArgumentParser()
		parser.add_argument('--folder', type=str, help='Path to frames folder')
		parser.add_argument('--ext', type=str, help='Frames\' extension')
		parser.add_argument('--multi', type=int, help='Path to filter list file', default=0)
		args = parser.parse_args()

		img_list = glob.glob(r'{0}\*.{1}'.format(args.folder, args.ext))
		num_frames = len(img_list)
		filters_data = np.loadtxt(args.folder + '/filters.txt', dtype='str', delimiter=r'/', ndmin=2)
		num_filters = filters_data.shape[0]

		fig, ax = plt.subplots()
		fig.canvas.mpl_connect('key_press_event', keypress)

		if args.multi == 0:
			img_path = img_list[0]
			img = cv2.imread(img_path)
			original = cv2.cvtColor(cv2.imread(img_path), cv2.COLOR_BGR2RGB)
			is_original = False

			for i in range(num_filters):
				img = func(locals()[filters_data[i][0]], img, params_to_list(filters_data[i][1]))

			legend = 'Filters:'
			for i in range(num_filters):
				legend += '\n    ' + filters_data[i][0] + '/' + filters_data[i][1]

			legend_toggle = plt.text(0.02, 0.97, legend,
			                         horizontalalignment='left',
			                         verticalalignment='top',
			                         transform=ax.transAxes,
			                         bbox=dict(facecolor='white', alpha=0.5),
			                         fontsize=9,
			                         )

			try:
				mng = plt.get_current_fig_manager()
				mng.window.state('zoomed')
				mng.set_window_title('Filtering')
			except:
				pass

			plt.title('Use SPACE to toggle between original and filtered image, and Q or ESC to exit')
			plt.axis('off')
			img_shown = plt.imshow(img)
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

				for i in range(num_filters):
					img = func(locals()[filters_data[i][0]], img, params_to_list(filters_data[i][1]))

				cv2.imwrite('{0}/{1}'.format(filtered_folder, path.basename(img_path)), img)

				print(' [INFO] Filtering frame {}/{} ({:.1f}%)'.format(j, num_frames - 1, j/(num_frames - 1) * 100))

			print('  [END] Filtering complete!')
			print('  [END] Results available in folder [{}]!'.format(filtered_folder))
			input('\nPress ENTER/RETURN to exit...')

	except Exception as ex:
		print('\n[EXCEPTION] The following exception has occurred: \n\n'
		      '  {}'.format(ex))
		input('\nPress any key to exit...')
