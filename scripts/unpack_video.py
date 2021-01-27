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
	from math import log

except Exception as ex:
	print('\n[EXCEPTION] Import failed: \n\n'
		  '  {}'.format(ex))
	input('\nPress any key to exit...')
	exit()


separator = '---'
MAX_FRAMES_DEFAULT = 30*60*60*24


def get_camera_parameters(path):
	cp = configparser.ConfigParser()
	cp.optionxform = str
	cp.read(path, encoding='utf-8-sig')

	sec = 'Intrinsics'
	fx = float(cp.get(sec, 'fx'))
	fy = float(cp.get(sec, 'fy'))
	s = float(cp.get(sec, 's'))
	cx = float(cp.get(sec, 'cx'))
	cy = float(cp.get(sec, 'cy'))

	sec = 'Radial'
	k1 = float(cp.get(sec, 'k1'))
	k2 = float(cp.get(sec, 'k2'))
	k3 = float(cp.get(sec, 'k3', fallback='0'))
	k4 = float(cp.get(sec, 'k4', fallback='0'))
	k5 = float(cp.get(sec, 'k5', fallback='0'))
	k6 = float(cp.get(sec, 'k6', fallback='0'))

	sec = 'Tangential'
	p1 = float(cp.get(sec, 'p1'))
	p2 = float(cp.get(sec, 'p2'))

	camera_matrix = np.array([[fx, 0, cx],
							  [0, fy, cy],
							  [0, 0, 1]])

	distortion = np.array([k1, k2, p1, p2, k3, k4, k5, k6])

	return camera_matrix, distortion


def videoToFrames(video: str, folder='.', frame_prefix='frame', ext='jpg', verbose=False,
				  start=0, start_num=0, end=MAX_FRAMES_DEFAULT, qual=95, scale=None, interp=cv2.INTER_LINEAR,
				  cm=None, dist=None) -> bool:
	"""
	Extracts all num_frames from a video to separate images. Optionally writes to a specified folder,
	creates one if it does not exist. If no folder is specified, it writes to the parent folder.
	Option to choose an image file prefix and extension. Returns True (if success) or False (if error).

	:param video: 			Path to the video. Should be a string.
	:param folder: 			Folder name to put the image files in. Default is '.', so it writes all the num_frames to the
							parent folder. Creates a folder if it does not already exist.
	:param frame_prefix:	Default prefix for the image files. Default is 'frame'.
	:param ext: 			Extension for the image files. Should be a string, without the leading dot. Default is 'jpg'.
	:param verbose: 		Whether to use a verbose output. Default is False.
	:param start:			Starting frame. Default is 0, i.e. the first frame.
	:param start_num:		Frame numbering sequence start. Default is 0.
	:param end:				End frame MAX_FRAMES_DEFAULT global.
	:param qual:			Output image quality in range (1-100). Default is 95.
	:param scale:			Scale parameter for the output images. Default is None, which preserves the original size.
	:param interp:			Interpolation algorithm for image resizing from cv2 package. Default is cv2.INTER_LINEAR.
	:return: 				True (if success) or False (if error).
	"""

	if not path.exists(video):
		print('[ERROR] Video file not found at {}'.format(video))
		input('\nPress any key to exit...')
		exit()

	vidcap = cv2.VideoCapture(video)
	numFrames = int(vidcap.get(cv2.CAP_PROP_FRAME_COUNT))
	vidcap.set(1, start)
	success, image = vidcap.read()

	if verbose:
		print('[BEGIN] Starting VIDEOTOFRAMES '.ljust(len(separator), '-'))
		print('[INFO] Starting extraction of frames from', video, 'starting from frame', start)

	i = start
	j = start_num
	success = True
	size = 0

	if end is None:
		end = MAX_FRAMES_DEFAULT

	num_len = int(log(end-start, 10)) + 1

	while success and i < end:  # If new frame exists
		if folder is None:
			n = str(j).zfill(num_len)
			save_str = '{}{}.ext'.format(frame_prefix, n, ext)
		else:
			if not path.exists(folder):  # Create :folder: if it does not already exist
				makedirs(folder)

			n = str(j).zfill(num_len)
			save_str = '{}/{}{}.{}'.format(folder, frame_prefix, n, ext)

		if cm is not None and dist is not None:
			image = cv2.undistort(image, cm, dist)

		if scale is not None and scale != 1.0:
			image = cv2.resize(image, None, fx=scale, fy=scale, interpolation=interp)

		if verbose:
			print('[INFO] Extracting frame: {}'.format(i))

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
		print(separator)
		print('[END] Images written to folder {}/'.format(folder))
		print('[END] Total number of extracted images is {}'.format(i-start))
		print('[END] Total size of extracted images is {} MB'.format(round(size, 2)))
		print(separator)

	vidcap.release()  # Clear video from memory

	if i == numFrames or i == end + 1:
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
		except:
			print(
				'[ERROR] There was a problem reading the configuration file!\nCheck if project has valid configuration.')
			exit()

		video_path = cfg.get(section, 'VideoPath')
		video_folder = path.dirname(video_path)
		results_folder = cfg.get(section, 'OutputFolder')
		remove_distortion = int(cfg.get(section, 'Undistort'))

		camera_matrix, distortion = get_camera_parameters('{}/camera_parameters.txt'.format(video_folder))\
										if remove_distortion else None, None

		videoToFrames(video=		video_path,
					  folder=		results_folder,
					  frame_prefix=	cfg.get(section, 'Prefix', fallback=''),
					  ext=			cfg.get(section, 'Extension', fallback='jpg'),
					  qual=			int(cfg.get(section, 'Quality', fallback='95')),
					  scale=		float(cfg.get(section, 'Scale', fallback='1.0')),
					  start=		int(cfg.get(section, 'Start', fallback='0')),
					  start_num=	int(cfg.get(section, 'StartNum', fallback='0')),
					  end=			int(cfg.get(section, 'End', fallback='2592000')),
					  cm=			camera_matrix,
					  dist=			distortion,
					  verbose=True,
					  )

		print('\a')
		input('Press any key to exit...')

	except Exception as ex:
		print('\n[EXCEPTION] The following exception has occurred: \n\n'
			  '  {}'.format(ex))
		input('\nPress any key to exit...')
