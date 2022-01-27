import setuptools

setuptools.setup(name='SSIMS: SSIM-based video stabilization suite for UAVs"',
				 author='Robert Ljubicic',
				 author_email='rljubicic@grf.bg.ac.rs',
				 url='https://github.com/ljubicicrobert/SSIMS',
				 description='Video stabilization suite for UAS/UAV purposes based on Structural Similarity Index metric',
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
					 'opencv_python>=4',
					 'matplotlib>=3',
					 'scipy>=1',
					 'skimage>=0.16.1',
				 ],
				 python_requires='>=3.5',
				 license='GPL 3.0',
				 packages=setuptools.find_packages(),
				 scripts=[
					 '__init__.py',
					 'feature_tracking.py',
					 'stabilize_frames.py',
					 'feature_goodness.py',
					 'unpack_video.py',
					 'frames_to_video.py',
				 ],
)
