#!/usr/bin/env python3
# -*- coding: utf-8 -*-

"""
Fast SSIM implementation from https://github.com/chinue/Fast-SSIM.
"""

from os import path, name
from skimage.metrics import structural_similarity as skimage_ssim
from re import search

import numpy as np
import ctypes

ssim_dll_path = path.split(path.realpath(__file__))[0]
ssim_dll_name = 'ssim.dll' if name=='nt' else 'libssim.so'


class Loader:
    if(path.exists(path.join(ssim_dll_path, ssim_dll_name))):
        dll = np.ctypeslib.load_library(ssim_dll_name, ssim_dll_path)

    type_dict = {'int':ctypes.c_int, 'float':ctypes.c_float, 'double':ctypes.c_double, 'void':None,
                 'int32':ctypes.c_int32, 'uint32':ctypes.c_uint32, 'int16':ctypes.c_int16, 'uint16':ctypes.c_uint16,
                 'int8':ctypes.c_int8, 'uint8':ctypes.c_uint8, 'byte':ctypes.c_uint8,
                 'char*':ctypes.c_char_p,
                 'float*':np.ctypeslib.ndpointer(dtype='float32', ndim=1, flags='CONTIGUOUS'),
                 'int*':np.ctypeslib.ndpointer(dtype='int32', ndim=1, flags='CONTIGUOUS'),
                 'byte*':np.ctypeslib.ndpointer(dtype='uint8', ndim=1, flags='CONTIGUOUS')}

    @staticmethod
    def get_function(res_type='float', func_name='PSNR_Byte', arg_types=['Byte*', 'int', 'int', 'int', 'Byte*']):
        func = Loader.dll.__getattr__(func_name)
        func.restype = Loader.type_dict[res_type]
        func.argtypes = [Loader.type_dict[str.lower(x).replace(' ', '')] for x in arg_types]
        return func

    @staticmethod
    def get_function2(c_define='DLL_API float PSNR_Byte(const Byte* pSrcData, int step, int width, int height, OUT Byte* pDstData);'):
        r = search(r'(\w+)\s+(\w+)\s*\((.+)\)', c_define)
        assert(r!=None)
        r = r.groups()
        arg_list = r[2].split(',')
        arg_types=[]

        for a in arg_list:
            a_list = a.split()

            if('*' in a_list[-1]):
                arg = a_list[-1].split('*')[0]+'*' if(a_list[-1][0]!='*') else a_list[-2]+'*'
            else:
                arg = a_list[-3]+'*' if(a_list[-2]=='*') else a_list[-2]

            arg_types.append(arg)

        return Loader.get_function(r[0], r[1], arg_types)

    @staticmethod
    def had_member(name='dll'):
        return (name in Loader.__dict__.keys())


class DLL:
    @staticmethod
    def had_function(name='PSNR_Byte'):
        return (name in DLL.__dict__.keys())

    if(Loader.had_member('dll')):
        # float SSIM_Byte(Byte* pDataX, Byte* pDataY, int step, int width, int height, int win_size, int maxVal);
        SSIM_Byte = Loader.get_function('float', 'SSIM_Byte', ['Byte*', 'Byte*', 'int', 'int', 'int', 'int', 'int'])


def SSIM(x, y, win_size=7):
    h, w = x.shape

    if(DLL.had_function('SSIM_Byte') and x.dtype=='uint8' and y.dtype=='uint8'):
        return DLL.SSIM_Byte(x.reshape([-1]), y.reshape([-1]), w, w, h, win_size, 255)
    else:
        return skimage_ssim(x,y, win_size=win_size, data_range=255)
