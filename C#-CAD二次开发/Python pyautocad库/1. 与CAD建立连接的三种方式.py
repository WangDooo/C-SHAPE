#!/usr/bin/env python
# -*- coding: utf-8 -*-

'''
=============================
Author = Hulunbuir & DalaiNur
Email: liyang@alu.hit.edu.cn
Last Update: 2019.09.21 13:43
=============================
'''

# *********************************************** 方式一 *********************************************** # "

# from pyautocad import Autocad

# acad = Autocad(create_if_not_exists = True)
#     # 连接正在运行的CAD程序；
#     # 若CAD未运行，则程序自动启动CAD，启动较慢，请耐心等待；
#     # 自动启动的CAD文件采用默认名称Drawing1。

# acad.prompt("Hello! AutoCAD from pyautocad.")
#     # 在CAD命令行显示"Hello! Autocad from Python."，用于测试对CAD的控制是否成功；
#     # 此时，注意观察命令行，若无反应可按F2，查看命令输入历史。

# print(acad.doc.Name)
#     # 获得与Python连接的正在运行的CAD文件名。

# *********************************************** 方式二 *********************************************** # "

# import comtypes.client

# acad = comtypes.client.GetActiveObject("AutoCAD.Application")
#     # 获取正在运行的AutoCAD应用程序对象
# doc = acad.ActiveDocument
#     # 获取当前文件
# ms = doc.ModelSpace
#     # 获取当前文件的模型空间
    
# doc.Utility.Prompt("Hello! AutoCAD from comtypes.")
# print(doc.Name)

# # ********推荐*************************************** 方式三 *********************************************** # "

import win32com.client as win32

acad = win32.Dispatch("AutoCAD.Application")
doc = acad.ActiveDocument
ms = doc.ModelSpace

doc.Utility.Prompt("Hello! AutoCAD from win32com.")
print(doc.Name)

# # ************************************************ END ************************************************ # "
