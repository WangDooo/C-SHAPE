using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetARX;

namespace Lines
{
    public class Lines{
        [CommandMethod("FirstLine")]
        public static void FirstLine() {
            // 获取当前活动图形数据库
            Database db = HostApplicationServices.WorkingDatabase;
            Point3d startPoint = new Point3d(0,100,0);
            Point3d endPoint = new Point3d(100,100,0);
            Line line = new Line(startPoint, endPoint);
            // 定义一个指向当前数据库的事务处理，以添加直线
            using (Transaction trans = db.TransactionManager.StartTransaction()) {
                BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForWrite); // 以只读方式打开表
                // 以写方式打开模型空间块表 记录
                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                // 将图形对象的信息添加到块表记录中，并返回ObjectID对象
                btr.AppendEntity(line);
                trans.AddNewlyCreatedDBObject(line, true); // 把对象添加到事务处理中
                trans.Commit();
            }
        }

        [CommandMethod("SecondLine")]
        public static void SecondLine() {
            Database db = HostApplicationServices.WorkingDatabase;
            Point3d startPoint = new Point3d(0,100,0);
            Point3d endPoint = new Point3d(0,200,0);
            Line line = new Line(startPoint, endPoint);
            db.AddToModelSpace(line);
        }

    }
}
