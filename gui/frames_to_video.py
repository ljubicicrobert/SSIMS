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
	from stabilize_frames import framesToVideo

except Exception as ex:
	print('\n[EXCEPTION] Import failed: \n\n'
		  '  {}'.format(ex))
	input('\nPress any key to exit...')
	exit()


if __name__ == '__main__':
	try:
		parser = ArgumentParser()
		parser.add_argument('--cfg', type=str, help='Path to configuration file')
		args = parser.parse_args()

		cfg = configparser.ConfigParser()
		cfg.optionxform = str
		section = 'Create video'

		try:
			cfg.read(args.cfg, encoding='utf-8-sig')
		except:
			print(
				'[ERROR] There was a problem reading the configuration file!\nCheck if project has valid configuration.')
			exit()

		interp_methods = {0: cv2.INTER_LINEAR,
						  1: cv2.INTER_CUBIC,
						  2: cv2.INTER_LANCZOS4}

		framesToVideo(cfg.get(section, 'VideoName'),
					  folder=	cfg.get(section, 'FramesFolder'),
					  ext=		cfg.get(section, 'ImageExtension', fallback='jpg'),
					  codec=	cfg.get(section, 'Codec', fallback='MJPG'),
					  fps=		float(cfg.get(section, 'FPS')),
					  scale=	float(cfg.get(section, 'Scale', fallback='1.0')),
					  interp=	interp_methods[int(cfg.get(section, 'Interpolation', fallback='0'))])

		print('\a')
		input('Press any key to exit...')

	except Exception as ex:
		print('\n[EXCEPTION] The following exception has occurred: \n\n'
			  '  {}'.format(ex))
		input('\nPress any key to exit...')