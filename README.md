# SSIMS: SSIM-based digital image stabilization suite

Video stabilization suite based on _Structural Similarity Index (SSIM)_ metric, developed primarily for application with UAS/UAV videos. Developed in cooperation with the COST Action HARMONIOUS (CA16219, funded by the European Cooperation in Science and Technology - COST Association).


## Capabilities

This package is an entire framework for the digital stabilization of videos from unmanned aerial systems, which includes both jitter removal and complete annulment of camera motion relative to a fixed frame of reference. Since it aims to completely remove apparent camera motion, it is different than most general-purpose digital stabilization tools.

Workflow of the tool:

1. **Unpacking** of videos into images, with the possibility of removing camera distortion using the camera intrinsics, radial and tangential distortion coefficients;

2. **Tracking** of a number of selected static features throughout the image sequence;

3. **Transformation** of original images onto a constant coordinate system, with the possibility of simultaneous orthorectification;

4. **Creating** stabilized video from transformed images.

The tool can also be used for more general purposes such as the removal of camera distortion from images, creating videos from images, etc.


## Requirements and installation

The package consists of a backend (written in Python 3) and frontend GUI (written in C# with .NET Framework 4.0). While the backend is cross-platform, the GUI will only work on Windows versions 7 (with SP1), 8, 8.1 and 10. The backend can function independently of the graphical interface, using the command line with configuration files as arguments (see section on usage below).

The GUI requires only **.NET Framework 4.0+**, which can be downloaded from the [official site](https://dotnet.microsoft.com/download/dotnet-framework).

The backend requires that Python 3+ exists in the %PATH% if you are using Windows, or $PATH if you are using Linux. Please make sure that this is the case before running. If you have multiple instances of Python 3 in the %PATH% (Windows platform), the GUI will detect and use only the first instance.

**WARNING**: The GUI will look for the first instance of Python in %PATH% which it will then use. Some software (happened to me with Inkscape) are known to deploy their own Python distributions and register them in %PATH%. The best remedy is to install your Python distribution prior to any other software that might be using it. Otherwise, make sure that the main version of Python (one containing all the necessary libraries listed below) is the first one in the %PATH% listing.

So far, the package was tested using **Python** versions **3.6.8, 3.7.2** and **3.8.5**. You can download latest Python from the [official site](https://www.python.org/downloads/)

Python library requirements (other than the standard library):
```python
numpy >= 1.19                       # pip install numpy
opencv-python >= 4.0                # pip install opencv-python
matplotlib >= 3.0                   # pip install matplotlib
scipy >= 1.0                        # pip install scipy
skimage (scikit-image) >= 0.16.1    # pip install scikit-image
```

**Note #1**: If you are using distributions of Python such as Anaconda or Winpython, you will likely have all the necessary libraries with the possible exception of _opencv-python_.

**Note #2**: The scripts might also work fine with _opencv-python_ version 3.6, but this is yet to be tested.

**Note #3**: It has been noted that newer versions of _matplotlib_ (apparently those which have been installed using PIP) are failing to be properly imported due to missing _ft2font_ DLL. In this case you can try installing _matplotlib_ version 3.2.1 which worked well during testing:
```bash
pip install matplotlib==3.2.1
```


## Usage

The backend consists of five scripts. Whether called from the console of from the GUI, they require adequate configuration files to be passed as arguments. For video unpacking into images:
```bash
python unpack_video.py --cfg path_to_config_file
python feature_tracking.py --cfg path_to_config_file
python stabilize_frames.py --cfg path_to_config_file
python frames_to_video.py --cfg path_to_config_file
```
Each script requires a slightly different INI-type configuration file. Template configurations for each script can be found in the **Templates** folder. Keep in mind that names of sections and variables in configuration files **should not be changed**. If GUI is used, such files will be automatically created for each script before they are called.

Final script in the package performs an estimation of how well each feature was tracked by the _feature_tracking.py_:
```bash
python feature_goodness.py --fold path_to_SA_folder --ref {0/1/2}
```
Argument _--fold_ should point to the folder named _gcp_img_ which is automatically created by the _feature_tracking.py_ script in the output folder. Argument _--ref_ indicates which frame will be used as a reference when estimating the "goodness" of each feature: 0 - initial frame, 1 - pixel intensity averages, 2 - pixel intensity medians.


## Graphical user interface (GUI)

If used on Windows platform, a convenient alternative to the console usage is the GUI (.exe) which is located in the **gui** folder. The GUI initializes with the [MAIN form](https://github.com/ljubicicrobert/SSIMS/edit/master/screenshots/main.png) in which the feature tracking and image transformation parameters are defined.

Keep in mind that some options are only available after certain steps have already been completed (e.g. filtering features for image transformation is only available after the feature tracking stage has been completed, etc.).

[Form for video unpacking](https://github.com/ljubicicrobert/SSIMS/edit/master/screenshots/video_unpack.png) and removal of camera distortion can be called using the appropriate button in the top-left corner of the MAIN form.

[Form for creating videos](https://github.com/ljubicicrobert/SSIMS/edit/master/screenshots/video_create.png) can be called using the appropriate button in the top-right corner of the MAIN form.


### Selecting features for tracking

In the MAIN form, user can select or input the location of original images, as well as the output location. Once the images have been found, user can start feature selection and tracking using the [Track features](https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/select_features.png) button in the bottom-left corner, which will open a new form to allow the user to select the static feature which will be tracked.

Use the **RIGHT mouse button** to select the static feature. Once a feature is selected, a regions representing interrogation area (IA) and search area (SA) will be shown around it.

If you wish to delete the previously selected feature, use the **MIDDLE mouse button** or the key **D** on keyboard. This will also clear the IA/SA regions from the image.

Use keys **O** and **P** to zoom and pan the image. Use keys **LEFT** and **RIGHT** to undo and redo the zoom or pan commands. Use key **ENTER/RETURN** to accept the selected features and start the tracking.

Toggle visibility of legend and point list using **F1** key.


### Selecting features for transformation

Not all of the tracked features have to be used for the transformation (stabilization) of images. You can select features that will be used for the transformation using the [Select features for transformation](https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/filter_features.png) button. This will open a new form which display the positions and coordinates of tracked features. From the given list, you can choose which ones will be used to stabilize the original images.

To help you choose the best features, an additional analysis is available by clicking the **Analyze RMSD** button in the top-left corner of the **Select features for transformation** window. This will run the _feature_goodness.py_ script and will show a [bar graph of sumarised root-mean-squared differences](https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/features_RMSD.png) (RMSD) for all frames relative to the reference frame (initial, average, or median). In the bar graph, the best features are likely to have lower RMSD scores, which can help you decide which ones to keep and which ones to remove from the transformation.

**KEEP IN MIND** that this is a simplified analysis, as it will likely be impacted by the shape and color of the selected features. Additional metrics are to be implemented in future releases.


### Image stabilization (transformation)

The software offers several options for the final stage, i.e. image transformation: (1) chosing the output images' extension, (2) choosing image quality, (3) whether to also create a video from the transformed images, (4) image transformation method, (5) whether to use RANSAC filtering/outlier detection, and (6) orthorectification. The latter is explained in the **Orthorectification** section below.

The most important parameter is the **transformation method** which can significantly impact the stabilization accuracy. Five methods are available:

1. **Similarity**, based on [cv2.estimateAffinePartial2D](https://docs.opencv.org/3.4/d9/d0c/group__calib3d.html#gad767faff73e9cbd8b9d92b955b50062d) from OpenCV, which requires at least 2 features (4 degrees of freedom),
2. **Affine 2D (strict)**, based on [cv2.getAffineTransform](https://docs.opencv.org/3.4/da/d54/group__imgproc__transform.html#ga8f6d378f9f8eebb5cb55cd3ae295a999), which requires exactly 3 features,
3. **Affine 2D (optimal)**, based on [cv2.estimateAffine2D](https://docs.opencv.org/3.4/d9/d0c/group__calib3d.html#ga27865b1d26bac9ce91efaee83e94d4dd), which requires at least 3 features,
4. **Projective (strict)**, based on [cv2.getPerspectiveTransform](https://docs.opencv.org/3.4/da/d54/group__imgproc__transform.html#ga8c1ae0e3589a9d77fffc962c49b22043), which requires exactly 4 features, and
5. **Projective (optimal)**, based on [cv2.findHomography](https://docs.opencv.org/3.4/d9/d0c/group__calib3d.html#ga4abc2ece9fab9398f2e560d53c8c9780), which requires at least 4 features. This is the default option and is usually the best starting point.

**NOTE:** RANSAC filtering option is only available for methods labeled as **(optimal)**.


### Orthorectification

The GUI also offers a simple orthorectification to be performed by estimating the transformation matrix between the in-image positions of tracking features and their corresponding real-world coordinates.

[Form for orthorectification](https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/orthorectify.png) can be shown by clicking **Orthorectify** in the Image transformation group of the [MAIN form](https://github.com/ljubicicrobert/SSIMS/edit/master/screenshots/main.png). Here the user can specify the real-world coordinates (in meteres) of only those features **which have been selected for the image transformation** (see section **Selecting features for transformation** for more details).

Users can also set a ground sampling distance (GSD, in px/m) to rescale the image and help with the postprocessing - use this feature carefully as it will always introduce additional errors/noise in the transformaed images. **It's best to keep this ratio as close as possible to the original GSD!**


### Acknowledgements

I wish to express my gratitude to the following people (in no particular order):

[Mrs. Sophie Pierce](https://www.worcester.ac.uk/about/profiles/sophie-pearce) - for motivating me to start the work in the first place;

[Mrs. Dariia Strelnikova](https://www.fh-kaernten.at/en/en/faculty-and-staff-details?personId=4298793872) and [Dr Anette Eltner](https://tu-dresden.de/bu/umwelt/geo/ipf/photogrammetrie/die-professur/beschaeftigte/Anette_Eltner?set_language=en) - for testing the software and allowing me to learn from their own work;

[Dr Alonso Pizarro](https://www.researchgate.net/profile/Alonso_Pizarro) and [Dr Salvador Peña‐Haro](https://www.researchgate.net/profile/Salvador_Pena-Haro) - for providing me with valuable insights into their own work;

[Dr Budo Zindović](https://www.grf.bg.ac.rs/fakultet/pro/e?nid=153) - for helping me with many implementational details;

[Dr Salvatore Manfreda](https://www.salvatoremanfreda.it) and [Dr Silvano Fortunato Dal Sasso](https://www.researchgate.net/profile/Silvano_Fortunato_Dal_Sasso) - for hosting me at the Università Basilicata in Potenza where I have learned and improved my algorithm;

[Dr Matthew T. Perks](https://www.ncl.ac.uk/gps/staff/profile/matthewperks.html#teaching) - for providing me with helpfull comments, as well as providing most of the camera parameters;


### References

Wang, Z., Bovik, A. C., Sheikh, H. R. and Simoncelli, E. P.: *Image Quality Assessment: From Error Visibility to Structural Similarity*, IEEE Trans. Image Process., 13(4), 600–612, [https://doi.org/10.1109/TIP.2003.819861](https://doi.org/10.1109/TIP.2003.819861), 2004.

### How to cite
Ljubicic, R.: *SSIMS: SSIM-based digital image stabilization suite*, [https://github.com/ljubicicrobert/SSIMS](https://github.com/ljubicicrobert/SSIMS), 2021.

### Licence and disclaimer

This tool is published under the [General Public Licence v3.0](https://www.gnu.org/licenses/gpl-3.0.en.html) and can be used and distributed freely and without charge. The author does not bear the responsibility regarding any possible (mis)use of the software/code, as well as for any damages (physical, hardware, and/or software) that may arise from the use of this tool. The package was scanned using Avast Antivirus prior to the upload, but a rescan after download is always recommended.
