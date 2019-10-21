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

# 2. 颜色设置
# 2.1 设置模型空间背景色
acad.ActiveDocument.Application.preferences.Display.GraphicsWinModelBackgrndColor = 0
    # 等号后为非负整数，取值范围为[0,16777215]，为十进制下的颜色代号，由RGB颜色值转化而来；
    # 具体计算公式为：R+256*G+256*256*B；
    # 例如：对于白色（R=255,G=255,B=255），颜色代号为255+256*255+256*256*255=16777215。
    # 常用标准颜色代号如下：
    # 0、黑色；255、红；65535、黄；65280、绿；16776960、青；16711680、蓝色；16711935、洋红；16777215、白色；
    # 将GraphicsWinModelBackgrndColor替换为GraphicsWinLayoutBackgrndColor，即可设置图纸空间背景色。

# 2.2 设置十字光标颜色

# 3.显示设置

# 3.1 显示线宽
acad.ActiveDocument.preferences.LineweightDisplay = 1
    # 等号后为布尔值，TRUE = 1，显示线宽，FALSE = 0，隐藏线宽。

# 3.2 显示自动捕捉靶框
# 3.3 显示自动捕捉标记
# 3.4 显示极轴追踪矢量

# 3.5显示点样式
acad.ActiveDocument.SetVariable("PDMODE", 35)
    # 系统默认值为0；
    # 详细信息见CAD帮助文档AUTOSNAP（系统变量）。

# 3.6 关闭实体填充显示
acad.ActiveDocument.Preferences.SolidFill = 0
    # 执行重新生成图形命令后才变更显示
acad.ActiveDocument.Regen(0)
    # 重新生成图形

# 4 尺寸设置

# 4.1 设置十字光标大小
acad.ActiveDocument.Application.preferences.Display.CursorSize = 5
    # 等号后取1到100的整数，表示十字光标占屏幕面积的百分比，10代表占比为10%，系统默认值为5。
# 4.2 设置自动捕捉靶框大小
# 4.3 设置自动捕捉标记大小
# 4.4 设置点大小
acad.ActiveDocument.SetVariable("PDSIZE", 10)

# 5 草图设置

# 5.1 开启栅格显示
acad.ActiveDocument.SetVariable("GRIDMODE", 1)
    # 0 关闭栅格显示

# 5.2  开启正交模式
# 5.3 极轴追踪
	# ①、开启极轴追踪
	# ②、用所有极轴角设置追踪并相对上一段测量极轴角
# 5.4 开启对象捕捉

# 6 设置视图

# 6.1 俯视图及西南等轴测图
# 6.2 全视图
acad.ActiveDocument.Application.ZoomAll()

# 7 设置文件自动保存

acad.ActiveDocument.Application.preferences.OpenSave.AutoSaveInterval = 0.1
    # 等号后数值为自动保存间隔分钟数，非负，可为小数；
    # 当为小数时，系统自动取整，取整规则为：小数部分<=0.5时，向下取整，>0.5时，向上取整；
    # 例如：当等号后值为4.5时，系统自动取为4，当为4.51时系统自动取为5；
    # 当数值<=0.5时，自动保存功能关闭。
