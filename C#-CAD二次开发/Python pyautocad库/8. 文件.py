#!/usr/bin/env python
# -*- coding: utf-8 -*-

'''
=============================
Author = Hulunbuir & DalaiNur
Email: liyang@alu.hit.edu.cn
Last Update: 2019.07.28 14:00
=============================
'''

from pyautocad import Autocad

# 1. 连接及库导入
acad = Autocad(create_if_not_exists = True)
acad.prompt("Hello! AutoCAD from pyautocad.")
print(acad.doc.Name)


"""
以下代码请不要在一个文件中同时运行，否则会报错，原因是逻辑冲突。
在D盘创建新文件夹并命名为AutoCAD 
"""

# 2. 打开文件
acad.ActiveDocument.Application.Documents.Open("D:\\AutoCAD\\PyAutoCAD.dwg")
    # CAD程序中至少存在一个打开的图形空间，否则报错，报错内容为：无法获取Document对象。


# 3. 新建文件
DrawingObj = acad.ActiveDocument.Application.Documents.Add("")
    # 无法直接命名，新建的文件为系统默认名称，即Drawing1、Drawing2等；
    # 若更改名称，在关闭时定义。


# 4. 设定当前
# 4.1 已知文件名设为当前
acad.ActiveDocument.Application.Documents("PyAutoCAD.dwg").Activate()
    # 将PyAutoCAD.dwg设为当前文件。

# 4.2 未知文件名设为当前
DrawingObj.Activate()
    # 将New_Drawing设为当前文件。


# 5. 关闭并保存变更
# 5.1 关闭已存在文件
acad.ActiveDocument.Application.Documents("PyAutoCAD.dwg").Close(True, "PyAutoCAD_已变更.dwg")
    # 关闭PyAutoCAD.dwg文件。
    # True 布尔值,为系统默认，表示打开文件后关闭前文件若发生变更，则保存变更，并另存为PyAutoCAD_已变更.dwg
    # 此时文件夹中同时存在未变更的"PyAutoCAD.dwg"和已变更的"PyAutoCAD_已变更.dwg"
    # 若第二项空缺，则新文件名为"PyAutoCAD.dwg"，覆盖之前未变更的文件。

# 5.2 关闭新建文件
DrawingObj .Close(True, "HIT.dwg")
    # 关闭New_Drawing文件。
    # 文件夹中仅存在"HIT.dwg"一个文件。

# 5.3 关闭当前文件
acad.ActiveDocument.Close()
    # 关闭当前文档。


# 6. 另存为
# 6.1 当前文件另存为 
acad.ActiveDocument.SaveAs("D:\\AutoCAD\\PyAutoCAD_SaveAs", 61)
    # 将当前文件另存为PyAutoCAD_SaveAs.dxf；
    # 此时，程序关闭当前文件，将PyAutoCAD_SaveAs.dxf切换为当前文件。
    # 61表示另存为文件的类型是AutoCAD 2013 DXF，常用类型如下：
    # 12 AutoCAD 2000 DWG (*.dwg)，13 AutoCAD 2000 DXF (*.dxf)；
    # 其它略，系统默认为AutoCAD 2013 DWG (*.dwg)。

# 6.2 指定文件另存为
acad.ActiveDocument.Application.Documents("PyAutoCAD.dwg").SaveAs("D:\\AutoCAD\\PyAutoCAD_SaveAs", 61)
    # 将特定文件PyAutoCAD.dwg另存为PyAutoCAD_SaveAs.dxf。
