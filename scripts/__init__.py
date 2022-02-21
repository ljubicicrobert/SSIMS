#!/usr/bin/env python3
# -*- coding: utf-8 -*-

import cv2
import numpy as np
import configparser
from argparse import ArgumentParser
from datetime import datetime

__package_name__ = 'SSIMS: Preprocessing tool for UAV image velocimetry'
__description__ = 'Preprocessing and video stabilization tool for UAS/UAV image velocimetry based on Structural Similarity (SSIM) Index metric'
__version__ = '0.3.1.3'
__status__ = 'beta'
__date_deployed__ = '2022-02-21'

__author__ = 'Robert Ljubicic, University of Belgrade - Civil Engineering Faculty'
__author_email__ = 'rljubicic@grf.bg.ac.rs'
__author_webpage__ = 'https://www.grf.bg.ac.rs/fakultet/pro/e?nid=216'
__project_url__ = 'https://github.com/ljubicicrobert/SSIMS'
__license__ = 'GPL 3.0'

__header__ = '''
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
			 '''
