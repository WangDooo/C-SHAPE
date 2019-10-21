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

# 2. 新建图层
LayerObj = acad.ActiveDocument.Layers.Add("HIT_Layer")
    # 添加新图层，图层名称为"HIT_Layer"。
acad.ActiveDocument.ActiveLayer = LayerObj
    # 将"HIT_Layer"图层设置为当前图层。

# 3. 颜色设置
ClrNum = 1
LayerObj.color = ClrNum
    # ClrNum为颜色索引号，其取值范围为[0,256]；
    # 标准颜色的颜色索引号指定如下：：1 红、2 黄、3 绿、4 青、5 蓝、6 洋红、7 白/黑；
    # 0 ByBlock、256 ByLayer；
    # 其他颜色索引号见 https://wenku.baidu.com/view/9d458b70195f312b3069a505.html。

# 4. 线型设置

# 5. 线宽设置

# 6. 批量创建
clr_num = [1, 2, 3]  
    # 图层颜色列表
layers_name = ["HIT_图层_1", "HIT_图层_2", "HIT_图层_3"] 
    # 图层名称列表

try:
    len(clr_num) == len(layers_name)
except:
    print("图层颜色号个数与图层个数不匹配")

layers_obj = [acad.ActiveDocument.Layers.Add(i) for i in layers_name]  
    # 批量创建图层

for j in range(len(layers_obj)):
    layers_obj[j].color = clr_num[j]
    # 批量指定图层颜色

# 7. 图层读取
layers_num = acad.ActiveDocument.Layers.count  
    # 当前文件模型空间中所包含的图层总数
layers_name = [acad.ActiveDocument.Layers.Item(i).Name for i in range(layers_num)]
	# 当前文件模型空间中所包含的所有图层名称
index = layers_name.index("HIT_图层_2") 
	# 获取指定图层索引号
acad.ActiveDocument.ActiveLayer = acad.ActiveDocument.Layers.Item(index)
	# 将指定图层设定当前
print("当前文件模型空间中所包含的图层总数 = ",layers_num)
print("当前文件模型空间中所包含的所有图层名称 = ",layers_name)
print("获取指定图层索引号 = ",index)
