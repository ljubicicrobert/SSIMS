# SSIMS: Preprocessing tool for UAV image velocimetry

Complete preprocessing tool for UAV image velocimetry - unpacking, filtering, stabilization, orthorectification and video creation. Video stabilization algorithm is based on _Structural Similarity Index (SSIM)_ metric. Developed in cooperation with the COST Action HARMONIOUS (CA16219, funded by the European Cooperation in Science and Technology - COST Association).


## Contents

1. [Capabilities](#capabilities)
2. [Requirements and installation](#requirements-and-installation)
3. [New versions](#new-versions)
4. [Usage: Scripts](#usage-scripts)
5. [Usage: Graphical user interface (GUI)](#usage-graphical-user-interface-gui)<br/>
    5.1. [Main form](#main-form)<br/>
    5.2. [Camera calibration](#camera-calibration)<br/>
    5.3. [Selecting features for tracking](#selecting-features-for-tracking)<br/>
    5.4. [Orthorectification](#orthorectification)<br/>
    5.5. [Image filtering](#image-filtering)
6. [Future features](#future-features)
7. [Acknowledgements](#acknowledgements)
8. [References](#references)
9. [How to cite](#how-to-cite)
10. [License and disclaimer](#license-and-disclaimer)


## Capabilities

The most important functionality of the tool is video stabilization which includes both jitter removal and annulment of camera motion relative to a fixed frame of reference. Since it aims to completely remove apparent camera motion, it is different than most general-purpose digital stabilization tools.

Stabilization workflow:

1. **Unpacking** of videos into images, with the possibility of removing camera distortion using the camera intrinsics, radial and tangential distortion coefficients;

2. **Tracking** of a number of selected static features throughout the image sequence. These features will be used to identify and compensate camera motion;

3. **Transformation** of original images onto a constant coordinate system, with the possibility of simultaneous orthorectification;

4. **Creating** stabilized video from transformed images.

Other, more general capabilities of the tool include image filtering, creating videos from images, etc.


## Requirements and installation

The package consists of a backend (written in Python 3) and frontend GUI (written in C# with .NET Framework 4.5). While the backend is cross-platform, the GUI will only work on Windows versions 7 (with SP1), 8, 8.1 and 10. The backend can function independently of the graphical interface, using the command line with configuration files as arguments (see section on usage below).

The GUI requires only **.NET Framework 4.5+**, which can be downloaded from the [official site](https://dotnet.microsoft.com/download/dotnet-framework).

The backend requires that Python 3+ exists in the **%PATH% environmental variable** if you are using Windows, or **$PATH** if you are using Linux. Please make sure that this is the case before running! If you have multiple instances of Python 3 in the %PATH% variable (Windows platform), the GUI will detect and use the FIRST instance.

> **WARNING**: Some software (happened to me with Inkscape) are known to deploy their own Python distributions and register them in %PATH%, which can interfere with the registration of other Python distributions. The best remedy is to install your Python distribution prior to any other software that might be using it. Otherwise, make sure that the main version of Python (one containing all the necessary libraries listed below) comes before any other Python distributions in the %PATH% listing.
>
> **Note #1**: Clicking on _Python version: ..._ text in the bottom-left corner of the MAIN form will open %PATH% variables window. This window will also automatically open if Python was not found in the %PATH% variable.

So far, the package was tested using **Python** versions **3.6.8, 3.7.2, 3.7.6, 3.8.5** and **3.9.10**. You can download latest Python from the [official site](https://www.python.org/downloads/)

Python library requirements (other than the standard library):
```python
numpy >= 1.19                       # pip install numpy
opencv-python >= 4.0                # pip install opencv-python
matplotlib >= 3.0                   # pip install matplotlib
mplcursors >= 0.3                   # pip install mplcursors
scipy >= 1.0                        # pip install scipy
skimage (scikit-image) >= 0.16.1    # pip install scikit-image
```

>**Note #2**: If you are using distributions of Python such as Anaconda or Winpython, you will likely have all the necessary libraries with the possible exception of `opencv-python`.
>
>**Note #3**: The tool might also work fine with `opencv-python` version 3.6, but this is yet to be tested.
>
>**Note #4**: It has been noted that newer versions of `matplotlib` (apparently those which have been installed using PIP) are failing to be properly imported due to missing `ft2font` DLL. In this case you can try installing `matplotlib` version 3.2.1 which worked well during testing:
>```bash
>pip install matplotlib==3.2.1
>```


## New versions

The GUI will automatically check for latest releases of the tool on program start. If new release was found in the official repository a form will be displayed from where the user can read the new release information, choose to view/download the new release on/from GitHub, or pause automatic checking for new versions for some period of time.

> **Note #5**: User can also manually check for new releases by clicking on _Build version: ..._ text in the bottom-left corner of the MAIN form.


## Usage: Scripts

The backend consists of seven scripts. Whether called from the console of from the GUI, they require adequate configuration files to be passed as arguments. The following scripts are used in the video stabilization framework:
```bash
python unpack_video.py --cfg [path_to_config_file]
python feature_tracking.py --cfg [path_to_config_file]
python stabilize_frames.py --cfg [path_to_config_file]
python frames_to_video.py --cfg [path_to_config_file]
```
Each script requires a slightly different INI-type configuration file. Template configurations for each script can be found in the **Templates** folder. Keep in mind that names of sections and variables in configuration files **should not be changed**. If GUI is used, such files will be automatically created for each script before they are called, and no manual intervention is needed.

The tool provides a script for camera calibration:
```bash
python camera_calibration.py --model [camera_model_name] --folder [path_to_images_folder] --ext [image_extension] -w [num_chequerboard_squares_longside] -h [num_chequerboard_squares_shortside] --use_k3 [0/1] --output [0/1]
```
Argument `--use_k3` defines whether two or three radial distortion coefficients will be used, while `--output` defines whether or not the chequerboard calibration images will be undistorted (the results will be saved to `[path_to_images_folder]/undistorted`).

There is an option to (p)review frames in the given folder with a specified extension (excluding dot) using the script:
```bash
python inspect_frames.py --folder [path_to_frames_folder] --ext [frames_extension]
```

The tool also offers frame filtering using the following script:
```bash
python filter_frames.py --folder [path_to_frames_folder] --ext [frames_extension] --multi [0/1]
```
> **Note #6**: The use of the previous script is discouraged due to the difficulties of writing manual filter stack definitions. Users are advised to either use the GUI for this functionality or to fork (i.e., copy and edit) the `filter_frames.py` script to their liking. **DO NOT** change the original script as this will prevent the **Filter frames** form from working properly or at all.

Final script in the package performs an estimation of how well each feature was tracked by the `feature_tracking.py`:
```bash
python ssim_scores.py --folder [path_to_output_folder]
```
Argument `--folder` should point to the stabilization folder created by the `feature_tracking.py` script. The output of the script is a boxplot of SSIM tracking scores for individual features selected by the `feature_tracking.py`.


## Usage: Graphical user interface (GUI)

### Main form

<img align="right" width="400" src="https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/main.png">

If used on Windows platform, a more convenient alternative to the console usage is the GUI (.exe) which is located in the **gui** folder. The GUI initializes with the [MAIN form](https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/main.png) in which the feature tracking and image transformation parameters are defined.

Keep in mind that some options are only available after certain steps have already been completed (e.g. choosing features for image transformation is only available after the feature tracking stage has been completed, etc.).

[Form for video unpacking](https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/video_unpack.png) and removal of camera distortion can be called using the appropriate button in the top-left corner of the MAIN form.

[Form for creating videos](https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/video_create.png) can be called using the appropriate button in the top-right corner of the MAIN form.

[Form for image filtering](https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/filter_form.png) can be called using the appropriate button in the top-middle section of the MAIN form.

After the **Frames folder** has been selected, user can inspect the frames using the **Inspect frames** button to the left of the **Frames folder** text box.

If the **Output folder** exists, user can open it quickly using the **Open output folder** button to the left of the **Output folder** text box.


### Camera calibration

<img align="right" width="250" src="https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/camera_parameters_file.png">

Form for camera calibration is a part of the [Unpack video](https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/video_unpack.png) form. Clicking on the **Camera parameters** button will open a [Camera parameters](https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/camera_parameters_file.png) form which allows two methods of selecting camera parameters: (1) from an existing calibration, or (2) by performing a new calibration.

If option (1) is selected, camera model can be chosen from a dropdown list, from a file by browsing, or by manual input of parameters.

When option (2) is selected, the [form will expand](https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/camera_parameters_calibration.png) to reveal additional options and explanations on how to perform a calibration of a new camera. The calibration process involves detection of [chequerboard patterns](https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/camera_calibration_plot.png) in a series of images, and minimizing the reprojection errors between the image- and object-space by modifying the camera parameters. At the end of the procedure, a report will show the [RMS reprojection errors](https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/camera_calibration_bars.png) obtained using the new camera parameters.

> **Note #7**: If certain images have a relatively high reprojection error value, it can be beneficial to remove them from the calibration folder and repeat the calibration procedure, as this might improve the calibration accuracy. For best results try to keep between 15 and 30 images with chequerboard (target) pattern. Also, generation and manual inspection of undistorted chequerboard images is highly advised.

Calibration script will create a `ret_list.txt` file which contains per-image mean reprojection errors, and a `[camera_model_name].cpf` file which contains the camera parameters - camera intrinsics, radial and tangential distortion coefficients. Copying this file to the `camera_profiles` folder of the **SSIMS** tool will make this camera profile available in the dropdown list of the Camera parameters form.


### Selecting features for tracking

<img align="right" width="550" src="https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/select_features.png">

In the MAIN form, user can select or input the location of original frames, as well as the location for storing results (output). Once the images have been found, user can start feature selection and tracking using the Track features button in the bottom-left corner, which will open [a new window](https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/select_features.png) to allow the user to manually select static features which will be tracked.

Use the **RIGHT mouse button** to select the static feature. Once a feature is selected, a regions representing interrogation area (IA) and search area (SA) will be shown around it.

Use the sliders at the bottom to adjust the IA/SA sizes.

If you wish to delete the previously selected feature, use the **MIDDLE mouse button** or the key **D** on keyboard. This will also clear the IA/SA regions from the image.

Use keys **O** and **P** to zoom and pan the image. Use keys **LEFT** and **RIGHT** to undo and redo the zoom or pan commands. Use key **ENTER/RETURN** to accept the selected features and start the tracking in all frames.

Toggle visibility of legend and point list using **F1** key.

> **Note #8**: Feature tracking will not immediately produce stabilized images. This will be done after the two following steps have been completed.


### Selecting features for transformation

Not all of the tracked features have to be used for the transformation (stabilization) of images. You can select features that will be used for the transformation using the [Select features for transformation](https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/filter_features.png) button. This will open a new form which display the positions and coordinates of tracked features. From the given list, you can choose which ones will be used to stabilize the original images.

> **Note #9**: An average SSIM tracking score (higher is better, 1 is ideal) will be shown next to each feature coordinates on the right side of the form.

To help you choose the best features, an additional analysis is available by clicking the **Plot SSIM scores** button in the top-left corner of the **Select features for transformation** window. This will run the `ssim_scores.py` script and will show a [bar graph of SSIM tracking scores for all frames](https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/ssim_boxplot.png). In the bar graph, better features will have a higher SSIM score and lower variance, which can help you decide which ones to keep and which ones to remove from the transformation.

<img width="800" align="center" src="https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/filter_features.png">


### Image stabilization (transformation)

The software offers several options for the final stage, i.e. image transformation: (1) choosing the output images' extension, (2) choosing image quality, (3) whether to also create a video from the transformed images, (4) image transformation method, (5) whether to use RANSAC filtering/outlier detection, and (6) orthorectification. The latter is explained in the **Orthorectification** section below.

The most important parameter is the **transformation method** which can significantly impact the stabilization accuracy. Five methods are available:

1. **Similarity**, based on [cv2.estimateAffinePartial2D](https://docs.opencv.org/3.4/d9/d0c/group__calib3d.html#gad767faff73e9cbd8b9d92b955b50062d), which requires at least 2 features (4 degrees of freedom),
2. **Affine 2D (strict)**, based on [cv2.getAffineTransform](https://docs.opencv.org/3.4/da/d54/group__imgproc__transform.html#ga8f6d378f9f8eebb5cb55cd3ae295a999), which requires exactly 3 features,
3. **Affine 2D (optimal)**, based on [cv2.estimateAffine2D](https://docs.opencv.org/3.4/d9/d0c/group__calib3d.html#ga27865b1d26bac9ce91efaee83e94d4dd), which requires at least 3 features,
4. **Projective (strict)**, based on [cv2.getPerspectiveTransform](https://docs.opencv.org/3.4/da/d54/group__imgproc__transform.html#ga8c1ae0e3589a9d77fffc962c49b22043), which requires exactly 4 features, and
5. **Projective (optimal)**, based on [cv2.findHomography](https://docs.opencv.org/3.4/d9/d0c/group__calib3d.html#ga4abc2ece9fab9398f2e560d53c8c9780), which requires at least 4 features. This is the default option and is usually the best starting point.

**Note #10:** RANSAC filtering option is only available for methods labeled as **(optimal)**.

<img align="right" width="230" src="https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/orthorectify.png">

### Orthorectification

The GUI also offers a simple orthorectification to be performed by estimating the transformation matrix between the in-image positions of tracking features and their corresponding real-world coordinates.

[Form for orthorectification](https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/orthorectify.png) can be shown by clicking **Orthorectify** in the Image transformation group of the [MAIN form](https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/main.png). Here the user can specify the known real-world coordinates (in meters) of a number of ground control points (at least 3). By clicking **Apply** the user will be prompted to select in-image positions of these GCPs, which **CAN BE DIFFERENT** from those features tracked for the stabilization purposes.

Users can also set a ground sampling distance (GSD, in px/m) to rescale the image and help with the postprocessing - use this feature carefully as it will always introduce additional errors/noise in the transformed images. **It's best to keep this ratio as close as possible to the original GSD!**. For these reasons user can click **Measure** button to quickly measure in-image distances and to compare them with real-world data in order to help determine appropriate GSD value.


### Image filtering

Users can perform filtering of images in a folder using the [Filter frames](https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/filter_form.png) form. In the form, user should first define the _frame folder path_ and the _extension_ of images in the folder. Available filters are listed on left side of the form, and the user can add them to the **filtering stack** on the right side by clicking on the desired filter. Only one filter of a given type can be selected at a time.

<img align="right" width="320" src="https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/filter_form.png">

Once the filters have been added to the filtering stack, user can edit the filter by clicking on the filter button in the filtering stack, which opens the [Filter parameters](https://github.com/ljubicicrobert/SSIMS/blob/master/screenshots/filter_parameters.png) form, or reorder the filters by dragging and dropping them at the desired position. IMPORTANT: Filters will be applied in the top-down order from the filtering stack.

In the **Filter parameters** form, the user can remove the filter from the stack using the **Remove** button, or adjust the parameters of the filter (using the trackbar of using the neighboring numerical box) and finally apply the parameters using the **Apply** button.

Once the filters have been chosen, user can preview the filtering results using the **Preview results** button in the bottom-middle section of the **Filter frames** form. In the opened window, user can toggle between the filtered and original image using the **SPACE** key.

Clicking **Apply filters** will initiate filtering on all frames in the selected folder.


### Future features

&#9744; Additional filters for preprocessing

&#9746; Complete camera calibration form (as of v0.3.1.0)

&#9744; Generation of local coordinate system from inter-GCP distances


### Acknowledgements

I wish to express my gratitude to the following people (in no particular order):

[Dr Budo Zindović](https://www.grf.bg.ac.rs/fakultet/pro/e?nid=153) - for helping me with many implementational details and extensive testing of the tool;

[Mrs. Sophie Pierce](https://www.worcester.ac.uk/about/profiles/sophie-pearce) - for motivating me to start the work in the first place;

[Mrs. Dariia Strelnikova](https://www.fh-kaernten.at/en/en/faculty-and-staff-details?personId=4298793872) and [Dr Anette Eltner](https://tu-dresden.de/bu/umwelt/geo/ipf/photogrammetrie/die-professur/beschaeftigte/Anette_Eltner?set_language=en) - for testing the software and allowing me to learn from their own work;

[Dr Alonso Pizarro](https://www.researchgate.net/profile/Alonso_Pizarro) and [Dr Salvador Peña‐Haro](https://www.researchgate.net/profile/Salvador_Pena-Haro) - for providing me with valuable insights into their own work;

[Dr Salvatore Manfreda](https://www.salvatoremanfreda.it) and [Dr Silvano Fortunato Dal Sasso](https://www.researchgate.net/profile/Silvano_Fortunato_Dal_Sasso) - for hosting me at the Università Basilicata in Potenza where I have learned and improved my algorithm;

[Dr Matthew T. Perks](https://www.ncl.ac.uk/gps/staff/profile/matthewperks.html#teaching) - for providing me with helpful comments, as well as providing most of the camera parameters.


### References

Wang, Z., Bovik, A. C., Sheikh, H. R. and Simoncelli, E. P. (2004) *Image Quality Assessment: From Error Visibility to Structural Similarity*, IEEE Trans. Image Process., 13(4), 600–612, [https://doi.org/10.1109/TIP.2003.819861](https://doi.org/10.1109/TIP.2003.819861)

### How to cite
Ljubicic, R. (2021) *SSIMS: Preprocessing tool for UAV image velocimetry*, [https://github.com/ljubicicrobert/SSIMS](https://github.com/ljubicicrobert/SSIMS)

&nbsp;&nbsp;&nbsp;&nbsp;or

Ljubičić R., Strelnikova D., Perks M.T., Eltner A., Pena-Haro S., Pizarro A., Dal Sasso S.F., Scherling U., Vuono P. and Manfreda S (2021) _A comparison of tools and techniques for stabilising unmanned aerial system (UAS) imagery for surface flow observations_. Hydrology and Earth System Sciences. 25 (9), pp.5105--5132, [https://doi.org/10.5194/hess-25-5105-2021](https://doi.org/10.5194/hess-25-5105-2021)

Performance evaluation and comparison results to similar tools available at [https://doi.org/10.5281/zenodo.4557921](https://doi.org/10.5281/zenodo.4557921).


### License and disclaimer

This tool is published under the [General Public License v3.0](https://www.gnu.org/licenses/gpl-3.0.en.html) and can be used and distributed freely and without charge. The author does not bear the responsibility regarding any possible (mis)use of the software/code, as well as for any damages (physical, hardware, and/or software) that may arise from the use of this tool. The package was scanned using Avast Antivirus prior to the upload, but a rescan after download is always recommended.
