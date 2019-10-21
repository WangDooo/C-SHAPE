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
import pyautocad.types

# 1. 连接及库导入
acad = Autocad(create_if_not_exists = True)
acad.prompt("Hello! AutoCAD from pyautocad.")
print(acad.doc.Name)

# 2. 文字
# 2.1 创建新文本
textString = "Harb Insti of Tec"
insertPnt = APoint(0, 0)
height = 2.5
textObj = acad.model.AddText(textString, insertPnt, height)
    # textString：文本内容；height：字高；
    # 系统默认对齐夹点位于文字基线左侧；
    # 采用系统默认对齐方式时，insertPnt为对齐夹点的位置坐标；
    # 当用户自定义对齐方式时，insertPnt以对齐方式中的为准。

# 2.2 对齐方式
# 2.2 文本缩放

# 3. 文本样式
# 3.1 创建文字样式
txtStyleObj = acad.ActiveDocument.TextStyles.Add("HIT_TxtStyle")
# 3.2 将特定文字样式设为当前
acad.ActiveDocument.ActiveTextStyle = acad.ActiveDocument.TextStyles.Item("Standard")
# 3.3 设置字体
# 3.4 设置字体倾斜角度

# 4. 尺寸标准
# 4.1 线性及旋转线性标注
import math
XLine1Point = APoint(5, 25)
XLine2Point = APoint(25, 35)
DimLineLocation = APoint(10, 20)
RotationAngle = math.radians(0)
dimRotObj = acad.model.AddDimRotated(XLine1Point, XLine2Point, DimLineLocation, RotationAngle)
    # XLine1Point 第一尺寸界线的起点；
    # XLine2Point 第二尺寸界线的起点；
    # DimLineLocation 尺寸线定位点，尺寸线或其延长线过该点；
    # RotationAngle 尺寸线与水平方向的夹角，去弧度制；
    # RotationAngle=0 水平标注，RotationAngle=90 竖直标注。

# 4.2 对齐标注
ExtLine1Point = APoint(5, 25)
ExtLine2Point = APoint(25, 35)
TextPosition = APoint(-5, 25)
dimAliObj = acad.model.AddDimAligned(ExtLine1Point, ExtLine2Point, TextPosition)
    # ExtLine1Point 第一尺寸界线的起点；
    # ExtLine2Point 第二尺寸界线的起点；
    # TextPosition 尺寸线定位点，尺寸线或其延长线过该点。
    # dimObj.Update

# 4.3 角度标注
AngleVertex = APoint(0, 0)
FirstEndPoint = APoint(6, 8)
SecondEndPoint = APoint(6, -8)
TextPoint = APoint(10, 10)
dimAngObj = acad.model.AddDimAngular(AngleVertex, FirstEndPoint, SecondEndPoint, TextPoint)
    # AngleVertex 角度顶点；
    # FirstEndPoint 第一尺寸界线端点；
    # SecondEndPoint 第二尺寸界线端点；
    # TextPoint 尺寸圆弧线定位点，即尺寸圆弧线过该点。

# 4.4 弧长标注
ArcCenter = APoint(0, 0)
FirstEndPoint = APoint(6, 8)
SecondEndPoint = APoint(6, -8)
ArcPoint = APoint(20, 0)
dimArcObj = acad.model.AddDimArc(ArcCenter, FirstEndPoint, SecondEndPoint, ArcPoint)
    # ArcCenter 圆弧中心；
    # FirstEndPoint 第一尺寸界线端点；
    # SecondEndPoint 第二尺寸界线端点；
    # ArcPoint 尺寸圆弧线定位点，即尺寸圆弧线过该点。

# 4.5 直径及半径标注
ChordPoint = APoint(0, 10)
FarChordPoint = APoint(0, -10)
LeaderLength = 40
dimDiaObj = acad.model.AddDimDiametric(ChordPoint, FarChordPoint, LeaderLength)
    # ChordPoint 圆任意一直径的端点；
    # FarChordPoint 直径的另一端点；
    # LeaderLength 引线长度，为点ChordPoint到标准文字定位夹点的距离；
    # 标注类型可在相应的系统变量如DIMUPT等中更改。
Center = APoint(0, 0)
ChordPoint = APoint(10, 0)
LeaderLength = 10
dimRadObj = acad.model.AddDimRadial(Center, ChordPoint, LeaderLength)
    # Center 被标注圆或圆弧的圆心


# 5. 标注样式
# 5.1 创建新标注样式
DimStyleObj = acad.ActiveDocument.DimStyles.Add("HIT_DimStyle")
print(dimAliObj.StyleName) 
    # 打印标注对象的标准样式名称
    # 系统自带标注样式 ISO-25

# 5.2 将特定标注样式设为当前
acad.ActiveDocument.ActiveDimStyle = acad.ActiveDocument.DimStyles.Item("HIT_DimStyle")

# 5.3 设置尺寸线及尺寸界限
'''
说明：以下即(3)~(6)的设置将生效为<样式替代>；
程序自动画图时<样式替代>不起作用，新添加的标注仍采用系统默认；
手动画图时<样式替代>起作用，新添加的标注<样式替代>；
设置永久标注样式见(7)，后续添加详细代码；
标注样式替代是对当前标注样式中的指定设置所做的更改；
它与在不更改当前标注样式的情况下更改尺寸标注系统变量等效；
使用标注样式替代，无需更改当前标注样式便可临时更改标注系统变量。
'''

### ①、超出标记

### ②、基线间距

### ③、超出尺寸线
acad.ActiveDocument.SetVariable("DIMEXE", 1.25)
    # 指定尺寸界线超出尺寸线的距离。

### ④、起点偏移量
acad.ActiveDocument.SetVariable("DIMEXO", 0.625)
    # 设定自图形中定义标注的点到尺寸界线的偏移距离。

# 5.4 设置箭头
### ①、设定尺寸线箭头类型。
acad.ActiveDocument.SetVariable("DIMBLK", "_ARCHTICK")
    # "_ARCHTICK"表示建筑标记

### ②、设定引线箭头。

### ③、尺寸线箭头大小
acad.ActiveDocument.SetVariable("DIMASZ", 2.5)


# 5.5 设置标注文字
### ①、文字样式
acad.ActiveDocument.SetVariable("DIMTXSTY", "HIT_TxtStyle")

### ②、文字高度
acad.ActiveDocument.SetVariable("DIMTXT", 12.5)
    # 设定当前标注文字样式的高度；
    # 如果在此选项卡上指定的字样式具有固定的文字高度，则该高度将替代在此处设置的文字高度；
    # 如果要在此处设置标注文字的高度，请确保将文字样式的高度设置为 0。

### ③、文字位置

acad.ActiveDocument.SetVariable("DIMTAD", 1)
    # 控制标注文字相对尺寸线的垂直位置；
    # 0 表示标注文字在尺寸界线之间居中放置；
    # 1 表示将标注文字放置在尺寸线上方，从尺寸线到文字最低基线的距离为当前 DIMGAP 的值。
acad.ActiveDocument.SetVariable("DIMJUST", 0)
    # 水平控制标注文字在尺寸线上相对于尺寸界线的水平位置；
    # 0 表示将文字置于尺寸线之上，并在尺寸界线之间置中对正。
acad.ActiveDocument.SetVariable("DIMGAP", 0.625)
    # 设定文字相对尺寸线的位置。

### ④、文字对齐

acad.ActiveDocument.SetVariable("DIMTIH", 0)
    # 控制所有标注类型（坐标标注除外）的标注文字在尺寸界线内的位置；
    # 0 表示将文字与尺寸线对齐；
    # 1 表示水平绘制文字。
acad.ActiveDocument.SetVariable("DIMTOH", 0)
    # 控制标注文字在尺寸界线外的位置。


# 5.6 设置主单位
### ①、线性标注精度
acad.ActiveDocument.SetVariable("DIMDEC", 2)
    # 设定标注文字中的小数位数；
    # 2 表示显示小数点后三位；
          
### ②、小数分隔符
acad.ActiveDocument.SetVariable("DIMDSEP", ".")
    # "."表示小数分隔符，也可设置为","。
acad.ActiveDocument.SetVariable("DIMADEC", -1)
    # 设置角度标注精度与线性标注精度相同

# 5.7 设置永久标注样式


# 6. 多重引线
# 6.1 创建多重引线
import numpy as np

ArrowPnt = APoint(0, 0)
BaselinePnt = APoint(10, 10)
PntsArray = np.array([ArrowPnt, BaselinePnt])
PntsArray = PntsArray.reshape(1, PntsArray.shape[0] * PntsArray.shape[1])[0]
PntsArray = pyautocad.aDouble(PntsArray)
MLeaderObj = acad.model.AddMLeader(PntsArray, 0)
    # ArrowPnt 箭头位置；
    # BaselinePnt 基线位置 ；
    # 1 表示多重引线的索引号，为正整数。

# 6.2 设置箭头大小
MLeaderObj.ArrowheadSize = 2
    # 指箭头高度；
    # 此项将覆盖系统变量DIMASZ的值。

# 6.3 设置基线长度
MLeaderObj.DoglegLength = 8

# 6.4 设置基线间隙
MLeaderObj.LandingGap = 3
    # 基线端点到文字起点的距离

# 6.5 指定文字样式
MLeaderObj.TextStyleName = "HIT_TxtStyle"

# 6.6 指定文字内容
MLeaderObj.TextString = "HIT"
