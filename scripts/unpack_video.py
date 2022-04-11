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
	from os import path
	from math import log
	from class_console_printer import Console_printer, tag_string
	from class_progress_bar import Progress_bar
	from feature_tracking import fresh_folder

except Exception as ex:
	print()
	print(tag_string('exception', 'Import failed: \n'))
	print('  {}'.format(ex))
	input('\nPress ENTER/RETURN key to exit...')
	exit()


MAX_FRAMES_DEFAULT = 60**3


def get_camera_parameters(path: str) -> tuple:
	"""
	Retreives camera parameters from .cpf file (INI format).

	:param path:    Path as string.
	:return:        Camera matrix and distortion coeffs vector.
	"""

	cp = configparser.ConfigParser()
	cp.optionxform = str
	cp.read(path, encoding='utf-8-sig')

	sec = 'Intrinsics'
	fx = float(cp.get(sec, 'fx'))
	fy = float(cp.get(sec, 'fy'))
	cx = float(cp.get(sec, 'cx'))
	cy = float(cp.get(sec, 'cy'))

	sec = 'Radial'
	k1 = float(cp.get(sec, 'k1'))
	k2 = float(cp.get(sec, 'k2'))
	k3 = float(cp.get(sec, 'k3', fallback='0'))

	sec = 'Tangential'
	p1 = float(cp.get(sec, 'p1'))
	p2 = float(cp.get(sec, 'p2'))

	camera_matrix = np.array([[fx, 0,  cx],
							  [0,  fy, cy],
							  [0,  0,  1]])

	distortion = np.array([k1, k2, p1, p2, k3])

	return camera_matrix, distortion


def videoToFrames(video: str, folder='.', frame_prefix='frame', ext='jpg',
				  start=0, start_num=0, end=MAX_FRAMES_DEFAULT, qual=95, scale=None, interp=cv2.INTER_LINEAR,
				  camera_matrix=None, dist=None, cp=None, pb=None, verbose=False,) -> bool:
	"""
	Extracts all num_frames from a video to separate images. Optionally writes to a specified folder,
	creates one if it does not exist. If no folder is specified, it writes to the parent folder.
	Option to choose an image file prefix and extension. Returns True (if success) or False (if error).

	:param video: 			Path to the video. Should be a string.
	:param folder: 			Folder name to put the image files in. Default is '.', so it writes all the num_frames to the
							parent folder. Creates a folder if it does not already exist.
	:param frame_prefix:	Default prefix for the image files. Default is 'frame'.
	:param ext: 			Extension for the image files. Should be a string, without the leading dot. Default is 'jpg'.
	:param start:			Starting frame. Default is 0, i.e. the first frame.
	:param start_num:		Frame numbering sequence start. Default is 0.
	:param end:				End frame MAX_FRAMES_DEFAULT global.
	:param qual:			Output image quality in range (1-100). Default is 95.
	:param scale:			Scale parameter for the output images. Default is None, which preserves the original size.
	:param interp:			Interpolation algorithm for image resizing from cv2 package. Default is cv2.INTER_LINEAR.
	:param camera_matrix:	Camera matrix. If None, no camera rectification will be performed.
							Note that parameters [fx, fy, cx, cy] are divided by image size so they are dimensionless here.
	:param dist:			Camera distortion parameters. If None, no camera rectification will be performed.
	:param pb:				Progress bar writer object.
	:param cp:				Console printer writer object.
	:param verbose: 		Whether to use a verbose output. Default is False.
	:return: 				True (if success) or False (if error).
	"""

	if not path.exists(video):
		print(tag_string('error', 'Video file not found at {}'.format(video)))
		input('\nPress ENTER/RETURN to exit...')
		exit()

	vidcap = cv2.VideoCapture(video)
	num_frames_total = int(vidcap.get(cv2.CAP_PROP_FRAME_COUNT))
	vidcap.set(1, start)
	success, image = vidcap.read()

	height, width = image.shape[:2]

	if verbose:
		print(tag_string('start', 'Starting frame extraction'))
		print(tag_string('info', 'Extraction of frames from [{}] starting from frame {}'.format(video, start)))
		print()

	i = start
	j = start_num
	success = True
	size = 0

	if not end:
		end = MAX_FRAMES_DEFAULT

	num_frames_to_extract = end - start

	if cp and pb:
		pb.set_total(num_frames_to_extract)

	num_len = int(log(end-start, 10)) + 1
	fresh_folder(folder, ext=ext)

	while success and i < end:  # If new frame exists

		if folder is None:
			n = str(j).zfill(num_len)
			save_str = '{}{}.{}'.format(frame_prefix, n, ext)
		else:
			n = str(j).zfill(num_len)
			save_str = '{}/{}{}.{}'.format(folder, frame_prefix, n, ext)

		if camera_matrix is not None and dist is not None:
			camera_matrix[0, 0] = camera_matrix[0, 0] * width			# fx
			camera_matrix[1, 1] = camera_matrix[1, 1] * width			# fy
			camera_matrix[0, 2] = camera_matrix[0, 2] * width			# cx
			camera_matrix[1, 2] = camera_matrix[1, 2] * width		    # cy
			image = cv2.undistort(image, camera_matrix, dist)

		if scale is not None and scale != 1.0:
			image = cv2.resize(image, None, fx=scale, fy=scale, interpolation=interp)

		if verbose:
			if cp and pb:
				cp.single_line(pb.get(i - start))
			else:
				print(tag_string('info', 'Extracting frame: {}'.format(i)))

		if ext.lower() in ['jpg', 'jpeg']:
			cv2.imwrite(save_str, image,
						[int(cv2.IMWRITE_JPEG_QUALITY), qual])
		elif ext.lower() == 'png':
			cv2.imwrite(save_str, image,
						[int(cv2.IMWRITE_PNG_COMPRESSION), 9 - int(0.09 * qual)])
		elif ext.lower() == 'webp':
			cv2.imwrite(save_str, image,
						[int(cv2.IMWRITE_WEBP_QUALITY), qual + 1])
		else:
			cv2.imwrite(save_str, image)

		size += path.getsize(save_str) / (1024 * 1024)
		success, image = vidcap.read()

		i += 1
		j += 1

	if verbose:
		print()
		print(tag_string('end', 'Images written to folder [{}]'.format(folder)))
		print(tag_string('end', 'Total number of extracted images is {}'.format(i-start)))
		print(tag_string('end', 'Total size of extracted images is {} MB'.format(round(size, 2))))

	vidcap.release()  # Clear video from memory

	if i == num_frames_total or i == end + 1:
		return True
	else:
		return False


if __name__ == '__main__':
	try:
		parser = ArgumentParser()
		parser.add_argument('--cfg', type=str, help='Path to configuration file')
		args = parser.parse_args()

		cfg = configparser.ConfigParser()
		cfg.optionxform = str
		section = 'Unpack video'

		try:
			cfg.read(args.cfg, encoding='utf-8-sig')
		except Exception:
			print(tag_string('error', 'There was a problem reading the configuration file!\nCheck if project has valid configuration.'))
			exit()

		video_path = cfg.get(section, 'VideoPath')
		video_folder = path.dirname(video_path)
		results_folder = cfg.get(section, 'FramesFolder')
		remove_distortion = int(cfg.get(section, 'Undistort'))

		vidcap = cv2.VideoCapture(video_path)
		num_frames_total = int(vidcap.get(cv2.CAP_PROP_FRAME_COUNT))
		vidcap.release()

		unpack_end = int(cfg.get(section, 'End', fallback=str(num_frames_total)))

		camera_matrix, distortion = get_camera_parameters('{}/camera_parameters.txt'.format(video_folder))\
										if remove_distortion else None, None

		console_printer = Console_printer()
		progress_bar = Progress_bar(total=1, prefix=tag_string('info', 'Extracting frame '))

		videoToFrames(video=		 video_path,
					  folder=		 results_folder,
					  frame_prefix=	 cfg.get(section, 'Prefix', fallback=''),
					  ext=			 cfg.get(section, 'Extension', fallback='jpg'),
					  qual=			 int(cfg.get(section, 'Quality', fallback='95')),
					  scale=		 float(cfg.get(section, 'Scale', fallback='1.0')),
					  start=		 int(cfg.get(section, 'Start', fallback='0')),
					  start_num=	 int(cfg.get(section, 'StartNum', fallback='0')),
					  end=			 unpack_end,
					  camera_matrix= camera_matrix,
					  dist=			 distortion,
					  pb=			 progress_bar,
					  cp=			 console_printer,
					  verbose=		 True,
					  )

		print('\a')
		input('\nPress ENTER/RETURN to exit...')

	except Exception as ex:
		print()
		print(tag_string('exception', 'The following exception has occurred: \n'))
		print('  {}'.format(ex))
		input('\nPress ENTER/RETURN key to exit...')
