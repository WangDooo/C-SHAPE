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
import numpy as np
import pythoncom
import win32com.client
import pyautocad.types

# 1. 连接及库导入
acad = Autocad(create_if_not_exists = True)
acad.prompt("Hello! AutoCAD from pyautocad.")
print(acad.doc.Name)

# 2. 常规选择
# "创建名称为SS1的选择集"

try:
    acad.ActiveDocument.SelectionSets.Item("SS1").Delete()
except:
    print("Delete selection failed")

slt = acad.ActiveDocument.SelectionSets.Add('SS1')

# 2.1 屏幕拾取
slt.SelectOnScreen()
print("请在屏幕拾取图元，以Enter键结束")
obj = slt[0]
print(obj.ObjectID)
print(slt)

# 2.2 选择过定点图元
pnt = APoint(0, 0)
slt.SelectAtPoint(pnt)
obj = slt[0]
print(obj.StartPoint)
    # 当点pnt存在于不止一个图元上时
    # 该选择方法仅能选择出其中的一个图元
    # 多个选择不出来

# 2.3 多边形框选
# "选择pnts内各点围成的边界内全部图元"

pnts = [APoint(0, 0), APoint(15000, 0), APoint(15000, 15000),  
              APoint(0, 15000), APoint(0, 0)]
pnts = np.array([j for i in pnts for j in i], dtype = np.float)

slt.SelectByPolygon(6, pnts)
      # acSelectionSetWindowPolygon = 6
obj = slt[0]
print(obj.StartPoint)

# 2.4 全选
slt.Select(5)
      # acSelectionSetAll = 5
obj = slt[0]
print(obj.layer)
print(obj.StartPoint)


# 3. 快速选择 [过滤器的实现]
# 3.1 滤出0图层上的所有圆
# " ********************************* ---- 创建图元 ---- ********************************* "

[pnt1,pnt2,pnt3,pnt4,pnt5,pnt6] = [APoint(40, 40),APoint(500, 500),APoint(300, 200),
                                   APoint(600, 200),APoint(700, 200), APoint(100, 0)]

# 创建点的另一种方式
pnt = np.array([50, 50, 0], dtype = np.float)
pnt = pyautocad.aDouble(pnt)
acad.model.AddPoint(pnt)

LineObj = acad.model.AddLine(pnt1, pnt2)
CircleObj = acad.model.AddCircle(pnt3, 100)
ArcObj = acad.model.AddArc(pnt4, 50, 0, 1.57)
EllObj = acad.model.AddEllipse(pnt5, pnt6, 0.5)


