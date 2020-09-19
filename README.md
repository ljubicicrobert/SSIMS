# SSIMS

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
Argument _--fold_ should point to the folder named _gcp_img_ which is automatically created by the _feature_tracking.py_ script in the output folder. Argument _--ref_ indicates which frame will be used as a reference when estimating the "goodness" of each feature: 1 - initial frame, 2 - pixel intensity averages, 3 - pixel intensity medians.


## Graphical user interface (GUI)

If used on Windows platform, a convenient alternative to the console usage is the GUI (.exe) which is located in the **gui** folder. The GUI initializes with the [MAIN form]() in which the feature tracking and image transformation parameters are defined.

[Form for video unpacking]() and removal of camera distortion can be called using the appropriate button in the top-left corner of the MAIN form.

[Form for creating videos]() can be called using the appropriate button in the top-right corner of the MAIN form.

### Selecting features for tracking
In the MAIN form, user can select or input the location of original images, as well as the output location. Once the images have been found, user can start feature selection and tracking using the **Track features** button in the bottom-left corner, which will open a new form to allow the user to select the static feature which will be tracked.

Use the **RIGHT mouse button** to select the static feature. Once a feature is selected, a regions representing interrogation area (IA) and search area (SA) will be shown around it.

If you wish to delete the previously selected feature, use the **MIDDLE mouse button** or the key **D** on keyboard. This will also clear the IA/SA regions from the image.

Use keys **O** and **P** to zoom and pan the image. Use keys **LEFT** and **RIGHT** to undo and redo the zoom or pan commands. Use key **ENTER/RETURN** to accept the selected features and start the tracking.

Toggle visibility of legend and point list using **F1** key.


### Selecting features for transformation
Not all of the tracked features have to be used for the transformation (stabilization) of images. You can select features that will be used for the transformation using the **Select features for transformation** button. This will open a new form which display the positions and coordinates of tracked features. From the given list, you can choose which ones will be used to stabilize the original images.

To help you choose the best features, an additional analysis is available by clicking the **Analyze RMSD** button in the top-left corner. This will run the _feature_goodness.py_ script and will show a bar graph of summed root-mean-squared differences for all frames relative to the reference frame (initial, average, or median). In the bar graph, the best features are likely to have lower RMSD scores, which can help you decide which ones to keep and which ones to remove from the transformation.

**KEEP IN MIND** that this is a simplified analysis, as it will likely be impacted by the shape and color of the selected features.


### Orthorectification


### Image stabilization (transformation)


### Acknowledgements


### References


### Licence
