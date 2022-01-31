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
	from matplotlib.widgets import Slider
	from sys import exit

	import glob
	import matplotlib.pyplot as plt

except Exception as ex:
	print('\n[EXCEPTION] Import failed: \n\n'
	      '  {}'.format(ex))
	input('\nPress any key to exit...')
	exit()

separator = '---'


def update_frame(val):
	img_new_path = frames_list[sl_ax_frame_num.val]
	img_new = cv2.cvtColor(cv2.imread(img_new_path), cv2.COLOR_BGR2RGB)
	h, w = img_new.shape[:2]

	img_shown.set_data(img_new)
	ax.set_title(fr'Frame #{sl_ax_frame_num.val}/{num_frames - 1}, {w}x{h}px')
	plt.draw()

	return


def keypress(event):
	if event.key == 'escape':
		exit()

	elif event.key == 'left':
		if sl_ax_frame_num.val == 0:
			sl_ax_frame_num.set_val(num_frames - 1)
		else:
			sl_ax_frame_num.set_val(sl_ax_frame_num.val - 1)

	elif event.key == 'right':
		if sl_ax_frame_num.val == num_frames - 1:
			sl_ax_frame_num.set_val(0)
		else:
			sl_ax_frame_num.set_val(sl_ax_frame_num.val + 1)

	elif event.key == 'up':
		if sl_ax_frame_num.val >= num_frames - 10:
			sl_ax_frame_num.set_val(0)
		else:
			sl_ax_frame_num.set_val(sl_ax_frame_num.val + 10)

	elif event.key == 'down':
		if sl_ax_frame_num.val <= 9:
			sl_ax_frame_num.set_val(num_frames - 1)
		else:
			sl_ax_frame_num.set_val(sl_ax_frame_num.val - 10)

	update_frame(sl_ax_frame_num.val)


if __name__ == '__main__':
	try:
		parser = ArgumentParser()
		parser.add_argument('--folder', type=str, help='Path to frames folder')
		parser.add_argument('--ext', type=str, help='Frames'' extension')
		args = parser.parse_args()

		frames_folder = args.folder
		ext = str(args.ext).replace('.', '')

		if frames_folder is None:
			print('\nFrames folder not provided!')
			input('\nPress any key to exit...')
			exit()
		elif ext is None:
			print('\nNo frame extension provided!')
			input('\nPress any key to exit...')
			exit()

		frames_list = glob.glob(fr'{frames_folder}\*.{ext}')
		num_frames = len(frames_list)

		if num_frames == 0:
			print('\nNo frames were found in folder [{0}] with extension [{1}]'.format(frames_folder, ext))
			input('\nPress any key to exit...')
			exit()

		fig, ax = plt.subplots()
		plt.subplots_adjust(bottom=0.13)
		plt.axis('off')

		axcolor = 'lightgoldenrodyellow'
		valfmt = "%d"

		ax_frame_num = plt.axes([0.2, 0.05, 0.63, 0.03], facecolor=axcolor)
		sl_ax_frame_num = Slider(ax_frame_num, 'Frame #', 0, num_frames-1, valinit=0, valstep=1, valfmt=valfmt)
		sl_ax_frame_num.on_changed(update_frame)

		legend = 'Use slider to select frame,\n' \
				 'use LEFT and RIGHT keys to move by 1 frame\n' \
				 'or UP and DOWN keys to move by 10 frames\n' \
				 'Press ESC or Q to exit'

		legend_toggle = plt.text(0.02, 0.97, legend,
		                         horizontalalignment='left',
		                         verticalalignment='top',
		                         transform=ax.transAxes,
		                         bbox=dict(facecolor='white', alpha=0.5),
		                         fontsize=9,
		                         )

		img = cv2.cvtColor(cv2.imread(frames_list[0]), cv2.COLOR_BGR2RGB)
		h, w = img.shape[:2]
		fig.canvas.mpl_connect('key_press_event', keypress)
		img_shown = ax.imshow(img)

		try:
			mng = plt.get_current_fig_manager()
			mng.window.state('zoomed')
			mng.set_window_title('Inspect frames')
		except:
			pass

		ax.set_title(fr'Frame #0/{num_frames - 1}, {w}x{h}px')
		plt.show()

	except Exception as ex:
		print('\n[EXCEPTION] The following exception has occurred: \n\n'
		      '  {}'.format(ex))
		input('\nPress any key to exit...')
