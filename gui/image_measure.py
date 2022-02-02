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
	from sys import exit
	from os import path

	import glob
	import matplotlib.pyplot as plt
	import mplcursors

except Exception as ex:
	print('\n[EXCEPTION] Import failed: \n\n'
		  '  {}'.format(ex))
	input('\nPress any key to exit...')
	exit()


def xy2str(l, dist):
	s = ''
	i = 1
	for x, y in l:
		s += '{}: {}, {}\n'.format(i, x, y)
		i += 1

	if dist > 0:
		s += 'Distance = {} px'.format(np.round(dist, 1))

	return s


def getMeasurement(event):
	global line
	global d
	global ax
	global points
	global plt_image

	if event.button == 3:
		d = 0
		p = [event.xdata, event.ydata]
		points.append(np.round(p, 1))

		if len(points) == 1:
			try:
				ax.lines.pop(0)
			except IndexError:
				pass

			ax.plot(points[0][0], points[0][1],
			        points[0][0], points[0][1], 'ro')

			d = 0

		elif len(points) == 2:
			ax.lines.pop(0)
			xs = [row[0] for row in points]
			ys = [row[1] for row in points]
			ax.plot(xs, ys, 'ro-')

			d = ((points[0][0] - points[1][0])**2 + (points[0][1] - points[1][1])**2)**0.5

		elif len(points) > 2:
			points = []
			d = 0

			try:
				ax.lines.pop(0)
				ax.lines.pop(0)
			except IndexError:
				pass

		plt.draw()

	points_list.set_text('')
	points_list.set_text(xy2str(points, d))

	plt.draw()


if __name__ == '__main__':
	try:
		parser = ArgumentParser()
		parser.add_argument('--img_path', type=str, help='Path to image file or folder with images')
		parser.add_argument('--ext', type=str, help='Path to image file')
		args = parser.parse_args()

		points = []
		d = 0

		img_path = args.img_path
		ext = args.ext

		try:
			path.exists(img_path)
		except:
			raise ValueError('The path argument [--img_path] does not seem to correspond to an image or folder with images, or could be missing!')

		if path.isfile(img_path):
			img = cv2.imread(img_path)
		elif path.isdir(img_path):
			images = glob.glob('{}/*.{}'.format(img_path, ext))
			img = cv2.imread(images[0])
		else:
			raise ValueError('The path argument [--img_path] does not seem to correspond to an image or folder with images, or could be missing!')

		img_rgb = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)

		fig, ax = plt.subplots()
		plt_image = ax.imshow(img_rgb)
		fig.canvas.mpl_connect('button_press_event', getMeasurement)

		points_list = plt.text(0.99, 0.02, '',
		                       horizontalalignment='right',
		                       verticalalignment='bottom',
		                       transform=ax.transAxes,
		                       bbox=dict(facecolor='white', alpha=0.5),
		                       fontsize=9,
		                       )

		legend = 'Right click to select starting and end point.\n' \
		         'Point coordinates and measurement will appear in\n' \
		         'the bottom right corner once both points are selected.\n' \
		         'Right click again to start over.\n' \
		         'O = zoom to window\n' \
				 'P = pan image'

		legend_toggle = plt.text(0.01, 0.98, legend,
		                         horizontalalignment='left',
		                         verticalalignment='top',
		                         transform=ax.transAxes,
		                         bbox=dict(facecolor='white', alpha=0.5),
		                         fontsize=9,
		                         )

		plt.show()

	except Exception as ex:
		print('\n[EXCEPTION] The following exception has occurred: \n\n'
		      '  {}'.format(ex))
		input('\nPress any key to exit...')
