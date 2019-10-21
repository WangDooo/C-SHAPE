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
from pyautocad import APoint

# 1. 连接及库导入
acad = Autocad(create_if_not_exists = True)
acad.prompt("Hello! AutoCAD from pyautocad.")
print(acad.doc.Name)

# 2. 块 创建的是Block
# 2.1 创建新块
grip = APoint(20, 0)
blockObj = acad.ActiveDocument.Blocks.Add(grip, "HIT_Block")
    # 新建块的名称为"HIT_Block"；
    # grip为块定位夹点所在位置。
# 2.2 添加图元到块
center = APoint(0, 0)
radius = 10
CircleObj = blockObj.AddCircle(center, radius)
center = APoint(40, 10)
majAxis = APoint(10, 0, 0)
EllObj = blockObj.AddEllipse(center, majAxis, 0.5)

# 2.3  插入块 Block插入到图纸空间后为BlockReference
# （1）从当前文件中插入块
insertionPnt = APoint(0, 0)
RetVal = acad.model.InsertBlock(insertionPnt, "HIT_Block", 1, 1, 1, 0 )
    # acad.model.InsertBlock(InsertionPoint, Name, Xscale, Yscale, ZScale, Rotation)；
    # insertionPnt为块的插入点，即块的定位夹点与图纸空间中的该点对齐。
# （2）外部文件作为块插入
insertionPnt = APoint(10, 0)
# RetVal = acad.model.InsertBlock(insertionPnt, "D:\\AutoCAD\\Harbin.dwg", 1, 1, 1, 0 )
    # 外部文件名尽量与当前文件中的各块名称不同；
    # 插入后外部文件名将作为其在当前文件中的块名；
    # 外部文件的坐标原点为其作为块的定位夹点。

# 2.4 添加属性到块
# (1) 当前文件创建的块属性添加
height = 1  # 字高
mode = 2    # 模式
prompt = "Attribute_Prompt"  # 提示
insertionPoint = APoint(0, 0)
tag = "Attribute_Tag"        # 标记
value = "Attribute_Value"    # 默认
attributeObj = blockObj.AddAttribute(height, mode, prompt, insertionPoint, tag, value)
# (2) 外部文件作为插入块的属性添加

# 3. 组
# 3.1 创建新组
groupObj = acad.ActiveDocument.Groups.Add("Harbin Institute of Technology")
    # 新建组的名称为"Harbin Institute of Technology"
