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
import math
import pyautocad.types

# 1. 连接及库导入
acad = Autocad(create_if_not_exists = True)
acad.prompt("Hello! AutoCAD from pyautocad.")
print(acad.doc.Name)

# 2. 创建点及直线
startPoint = APoint(5, 25)
endPoint = APoint(25, 35)
    # z坐标可空缺，空缺时系统默认其为0，即点Pnt1在CAD中坐标为（5,25,0）；
    # 系统自动将各坐标转化为双精度浮点数。

LineObj = acad.model.AddLine(startPoint, endPoint)


# 3. 添加多段线及样条曲线
# 3.1 添加普通多段线
pnts = [APoint(35, 35), APoint(40, 35), APoint(43, 32)]
pnts = np.array([j for i in pnts for j in i], dtype=np.float)
    # 将各点坐标顺序变换为行数据；
    # 添加样条曲线时参数仅支持1行多列的1维数组，即将各点x,y,z坐标顺序排列构成的数组。
    # 如报错可尝试增加该行代码 pnts = aDouble(pnts)
pnts = pyautocad.aDouble(pnts)
PlineObj = acad.model.AddPolyLine(pnts)

# 3.2 添加含圆弧多段线（倒角矩形）

# 3.3 设置多段线线宽
SegmentIndex = 1    # 多段线的段号
StartWidth = 10     # 段起点处线宽
EndWidth = 20       # 段终点处线宽
PlineObj.SetWidth (SegmentIndex, StartWidth, EndWidth)
    # 为多段线PlineObj的第二段设置变宽度线宽

# 3.4 添加样条曲线
SplinePnts = [APoint(35, 32), APoint(42, 25), APoint(48, 28), APoint(55, 25)]
SplinePnts = np.array([j for i in SplinePnts for j in i], dtype=np.float)
SplinePnts = pyautocad.aDouble(SplinePnts)
startTan = APoint(1, -10)
endTan = APoint(1, -5)
SplineObj = acad.model.AddSpline(SplinePnts, startTan, endTan)
    # startTan为样条曲线起点处切线的方向向量；
    # endTan为样条曲线终点处切线的方向向量。

# 4. 添加圆及圆弧
# 4.1 创建圆
CircleCenter = APoint(10, 10)
CircleObj = acad.model.AddCircle(CircleCenter, 5)
    # AddCircle(圆心, 半径)

# 4.2 创建圆弧
ArcCenter = APoint(20, 10)
ArcObj = acad.model.AddArc(ArcCenter, 5, math.radians(-60), math.radians(60))
    # AddArc（圆心，半径，始边角度（弧度制），终边角度（弧度制））


# 5. 添加椭圆及椭圆弧
# 5.1 创建椭圆
EllCenter = APoint(40, 10, 0)
majAxis = APoint(5, 0, 0)
EllObj = acad.model.AddEllipse(EllCenter, majAxis, 0.5)
    # 该椭圆以EllCenter为椭圆中心，长轴一端点为（45, 10, 0），且短轴长度为长轴的0.5倍；
    # majAxis为主轴（长轴）端点相对于椭圆中心的坐标增量；
    # 0.5短长轴之比。

# 5.2 创建椭圆弧
EllArcCenter = APoint(50, 10)
majAxis = APoint(5, 0, 0)
EllArcObj = acad.model.AddEllipse(EllArcCenter, majAxis, 0.5)
EllArcObj.startAngle = -90 * (3.14 / 180)
EllArcObj.endAngle = 90 * (3.14 / 180)

# 6. 实体填充
Pnt1 = APoint(65, 5, 0)
Pnt2 = APoint(65, 35, 0)
Pnt3 = APoint(75, 5, 0)
Pnt4 = APoint(75, 35, 0)
solidObj = acad.model.AddSolid(Pnt1, Pnt2, Pnt3, Pnt4)


# 7. 图案填充
# 特别感谢：ke1078 Email:354****89@qq.com
# 以下代码由ke1078同学提供

import pythoncom
import win32com.client

acad = win32com.client.Dispatch("AutoCAD.Application")
doc = acad.ActiveDocument
doc.Utility.Prompt("图案填充\n")
mp = doc.ModelSpace

def vtpt(x,y,z=0):
   return win32com.client.VARIANT(pythoncom.VT_ARRAY | pythoncom.VT_R8, (x,y,z))

def vtobj(obj):
    return win32com.client.VARIANT(pythoncom.VT_ARRAY | pythoncom.VT_DISPATCH, obj)

# 7.1 圆形图案填充
[ptnName, ptnType, bAss, center, radius ] = ["ANSI31", 0, True, vtpt(0, 0, 0), 10]
outerLoop = []
outerLoop.append(mp.AddCircle(center, radius))
outerLoop = vtobj(outerLoop)
hatchObj = mp.AddHatch(ptnType, ptnName, bAss)
hatchObj.AppendOuterLoop(outerLoop)
hatchObj.Evaluate()  # 进行填充计算，使图案吻合于边界

# 7.2 闭合多段线图案填充

# 8. 其他
import pyautocad.types

pyautocad.types.distance(Pnt1, Pnt2)       # 计算点Pnt1和点Pnt2间的距离
print(Pnt1)
print(tuple(Pnt1))                         # 将点Pnt1坐标转化为元组
print(list(Pnt1))                          # 将点Pnt1坐标转化为列表
print(Pnt1+Pnt2)                           # 两点对应坐标相加，也支持其他数学运算。

LineObj.layer = "0"                   # 指定图层
