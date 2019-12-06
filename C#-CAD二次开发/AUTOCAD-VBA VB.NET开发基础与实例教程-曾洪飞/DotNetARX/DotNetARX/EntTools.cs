using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;

namespace DotNetARX
{
    public static class EntTools{
        /// <summary>
        /// 将实体添加到模型空间
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="ent">要添加的实体</param>
        /// <returns>返回添加到模型空间中的实体ObjectId</returns>
        public static ObjectId AddToModelSpace(this Database db, Entity ent) {
            ObjectId endId; // 用于返回添加到模型空间中的实体ObjectId
            // 定义一个指向当前数据库的事务处理
            using (Transaction trans = db.TransactionManager.StartTransaction()) { 
                BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForWrite); // 以只读方式打开表
                // 以写方式打开模型空间块表 记录
                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                // 将图形对象的信息添加到块表记录中，并返回ObjectID对象
                endId = btr.AppendEntity(ent);
                trans.AddNewlyCreatedDBObject(ent, true); // 把对象添加到事务处理中
                trans.Commit();
            }
            return endId; // 返回实体的 ObjectId
        }
    }
}
