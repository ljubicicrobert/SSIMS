#!/usr/bin/env python3
# -*- coding: utf-8 -*-

import setuptools

setuptools.setup(name='SSIMS: Preprocessing tool for UAV image velocimetry',
				 author='Robert Ljubicic',
				 author_email='rljubicic@grf.bg.ac.rs',
				 url='https://github.com/ljubicicrobert/SSIMS',
				 description='Preprocessing and video stabilization tool for UAS/UAV image velocimetry based on Structural Similarity (SSIM) Index metric',
				 keywords=['Unmanned Aerial Systems', 'UAS', 'Unmanned Aerial Vehicles', 'UAV', 'stabilization', 'velocimetry'],
				 maintainer='Robert Ljubicic, University of Belgrade - Civil Engineering Faculty',
				 maintainer_email='rljubicic@grf.bg.ac.rs',
				 classifiers=[
				     'Development Status :: 4 - Beta',
				     'Environment :: Console (Text Based)',
				     'Programming Language :: Python :: 3',
			     ],
				 install_requires=[
					 'numpy>=1.1',
					 'opencv-python>=4',
					 'matplotlib>=3',
					 'mplcursors>=0.3',
					 'scipy>=1',
					 'skimage>=0.16.1',
				 ],
				 python_requires='>=3.5',
				 license='GPL 3.0',
				 packages=setuptools.find_packages(),
				 scripts = [
					'__init__.py',
					'camera_calibration.py',
					'class_console_printer.py',
					'class_logger.py',
					'class_progress_bar.py',
					'colorspaces.py',
					'feature_tracking.py',
					'filter_frames.py',
					'filters.xml',
					'frames_to_video.py',
					'image_measure.py',
					'inspect_frames.py',
					'ssim_scores.py',
					'stabilize_frames.py',
					'unpack_video.py',
				]
)
