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
	from os import path, listdir
	from glob import glob
	from math import log

	import matplotlib.pyplot as plt

except Exception as ex:
	print('\n[EXCEPTION] Import failed: \n\n'
		  '  {}'.format(ex))
	input('\nPress any key to exit...')
	exit()

try:
	parser = ArgumentParser()
	parser.add_argument('--fold', type=str, help='Path to folder containing feature tracking results')
	parser.add_argument('--ref', type=int, help='Reference frame for analysis: 0 = initial frame, 1 = pixel averages, 2 = pixel medians')
	args = parser.parse_args()

	fold = args.fold
	ref_values = args.ref

	if not path.exists(fold):
		print('[ERROR] Results folder [{}] not found!'.format(fold))
		exit()

	csv_files = glob('{}/gcps_csv/*.txt'.format(fold))
	img_files = glob('{}/gcps_img/*.jpg'.format(fold))

	init_csv_path = csv_files[0]
	init_img_path = img_files[0]

	n = int(log(len(csv_files), 10)) + 1

	num_frames = len(csv_files)

	init_csv = np.loadtxt(init_csv_path, delimiter=' ')
	num_markers = init_csv.shape[0]

	init_img = cv2.imread(init_img_path, 0)
	SA_size = init_img.shape[0]

	if num_frames < 2:
		print('[ERROR] Number of features cannot be less than 2 in [{}]'.format(init_csv_path))
		input('\nPress any key to exit...')
		exit()

	if init_img.shape[0] != init_img.shape[1]:
		print('[ERROR] Height/width mismatch for feature search area in [{}]!'.format(init_img_path))
		input('\nPress any key to exit...')
		exit()

	stack_markers = np.ndarray([num_markers, num_frames, SA_size, SA_size])
	marker_indices = list(range(1, num_markers + 1))
	rmsd = [0] * num_markers
	title_suffices = ['inital frame',
					  'pixel average',
					  'pixel median']

	for i in range(num_frames):
		for j in range(num_markers):
			marker_path = f'{str(i).rjust(n, "0")}_{j}.jpg'
			marker_img = cv2.imread(f'{fold}/gcps_img/{marker_path}', 0)
			stack_markers[j, i] = marker_img
			print('[INFO] Analyzing features in frame {}/{}'.format(i, num_frames-1))

	print('[END] Analysis complete')

	if ref_values == 0:
		for m in range(num_markers):
			ref = stack_markers[m, 0]
			list = stack_markers[m, :]
			diff = ref - list
			rmsd[m] = np.sqrt(np.sum(diff ** 2))

	elif ref_values == 1:
		stack_av = np.nanmean(stack_markers, axis=1)
		for m in range(num_markers):
			ref = stack_av[m]
			list = stack_markers[m, :]
			diff = ref - list
			rmsd[m] = np.sqrt(np.sum(diff ** 2))

	elif ref_values == 2:
		stack_med = np.nanmedian(stack_markers, axis=1)
		for m in range(num_markers):
			ref = stack_med[m]
			list = stack_markers[m, :]
			diff = ref - list
			rmsd[m] = np.sqrt(np.sum(diff ** 2))

	rmsd_norm = [(x - min(rmsd))/(max(rmsd) - min(rmsd)) for x in rmsd]

	plt.bar(marker_indices, rmsd, color=[[x, 0, 1-x] for x in rmsd_norm])
	plt.xticks(marker_indices)
	plt.xlabel('Feature # [-]')
	plt.ylabel('Total RMSD [-]')
	plt.title('RMSD of features relative to {}\n[lower is better]'.format(title_suffices[ref_values]))
	plt.show()

except Exception as ex:
	print('\n[EXCEPTION] The following exception has occurred: \n\n'
		  '  {}'.format(ex))
	input('\nPress any key to exit...')