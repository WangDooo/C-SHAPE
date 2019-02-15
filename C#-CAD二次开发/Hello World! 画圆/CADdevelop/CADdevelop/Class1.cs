using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.EditorInput;

using System.Collections;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;
using System.Reflection;


namespace CADdevelop
{
    public class Class1{
        [CommandMethod("HelloWorld")]
        public void HelloWorld(){
            Editor ed = AcadApp.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("Hello World222");
        }

        [CommandMethod("test")]
        public void createCircle(){ 
            //首先声明我们要使用的对象
            Circle circle; //这个是我们要加入到模型空间的圆
            BlockTableRecord btr;//要加入圆，我们必须打开模型空间
            BlockTable bt; //要打开模型空间，我们必须通过块表(BlockTable)来访问它

            //我们使用一个名为‘Transaction’的对象，把函数中有关数据库的操作封装起来
            Transaction trans;
            //使用 TransactionManager 的 StartTransaction()成员来开始事务处理
            trans = HostApplicationServices.WorkingDatabase.TransactionManager.StartTransaction();
            //现在创建圆……请仔细看这些参数——注意创建 Point3d 对象的‘New’和 Vector3d 的静态成员 ZAxis
            circle = new Circle(new Point3d(10, 10, 0), Vector3d.ZAxis, 2);
            bt = (BlockTable)trans.GetObject(HostApplicationServices.WorkingDatabase.BlockTableId, OpenMode.ForRead);
            //使用当前的空间 Id 来获取块表记录——注意我们是打开它用来写入
            btr = (BlockTableRecord)trans.GetObject(HostApplicationServices.WorkingDatabase.CurrentSpaceId, OpenMode.ForWrite);
            //现在使用 btr 对象来加入圆
            btr.AppendEntity(circle);
            trans.AddNewlyCreatedDBObject(circle, true); //并确定事务处理知道要加入圆！
                                                         //一旦完成以上操作，我们就提交事务处理，这样以上所做的改变就被保存了……
            trans.Commit();
            //…然后销毁事务处理，因为我们已经完成了相关的操作（事务处理不是数据库驻留对象，可以销毁）
            trans.Dispose();
        }
    }
}
