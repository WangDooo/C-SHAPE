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
import math

# 1. 连接及库导入
acad = Autocad(create_if_not_exists = True)
acad.prompt("Hello! AutoCAD from pyautocad.")
print(acad.doc.Name)

# 2. 复制及删除
startPoint = APoint(0, 0)
endPoint = APoint(50, 50)
     # z坐标可空缺，空缺时系统默认其为0，即点Pnt1在CAD中坐标为（5,25,0）；
     # 系统自动将各坐标转化为双精度浮点数。

EllObj = acad.model.AddLine(startPoint, endPoint)

copyObj = EllObj.Copy()
    # 原位置复制，复制的图元与原图元重合。
copyObj.Delete()


# 3. 平移及旋转

startPnt = APoint(0, 0)
endPnt = APoint(30, 0)
EllObj.Move(startPnt,endPnt)

BasePoint = APoint(0, 0)
RotationAngle = math.radians(30)
EllObj.Rotate(BasePoint, RotationAngle)
    # BasePoint为旋转基点，即旋转轴过此点且平行于z轴；
    # RotationAngle为旋转角度（弧度制），角度正负由右手系确定。

# 4. 镜像及缩放
startPnt = APoint(50, 50)
endPnt = APoint(50, -50)
MirObj = EllObj.Mirror(startPnt, endPnt)
    # startPnt为镜像线起点，endPnt为镜像线终点；
    # 镜像后原图元不删除。

BasePoint = APoint(0, 0)
ScaleFactor = 5
SclEntObj = EllObj.ScaleEntity(BasePoint, ScaleFactor)
     # ScaleFactor为缩放比例。

# 5. 阵列
# 5.1 矩形阵列
numberOfRows = 5
numberOfColumns = 5
numberOfLevels = 1
distanceBwtnRows = 20
distanceBwtnColumns = 20
distanceBwtnLevels = 1
# retObj = EllObj.ArrayRectangular(numberOfRows, numberOfColumns, numberOfLevels, 
#     distanceBwtnRows, distanceBwtnColumns, distanceBwtnLevels)

# 5.2 环形阵列
centerPoint = APoint(30, 0)
noOfObjects = 4
angleToFill = 3.14
retObj = EllObj.ArrayPolar(noOfObjects, angleToFill, centerPoint)


