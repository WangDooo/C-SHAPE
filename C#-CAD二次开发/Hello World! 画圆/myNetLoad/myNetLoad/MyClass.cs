using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using System.Windows.Forms;
using System.Reflection;

namespace myNetLoad
{
    public class MyClass{
        //本程序在AutoCAD的快捷命令是"NL"
        [CommandMethod("NL")]
        public void myLoad(){
            //AutoCAD命令栏
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //调用Windows.Forms选择一个文件
            OpenFileDialog fileDialog = new OpenFileDialog();
            //判断确认按钮
            if (fileDialog.ShowDialog() == DialogResult.OK){
                //选择的文件路径
                string file_dir = fileDialog.FileName;
                //在AutoCAD命令栏输出选择的文件路径
                ed.WriteMessage("文件路径:" + file_dir);
                //打开文件，将文件以二进制方式复制到内存，自动关闭文件
                byte[] buffer = System.IO.File.ReadAllBytes(file_dir);
                //加载内存中的文件
                Assembly assembly = Assembly.Load(buffer);
            }
        }
    }
}
