using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

namespace InitAndOpt {
    public class OptimizeClass {
        [CommandMethod("OptCommand")]
        public void OptCommand() {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            string filename = "C:\\Users\\123\\Desktop\\test\\Hello.dll";
            ExtensionLoader.Load(filename); 
            ed.WriteMessage("\n"+filename+"被载入，输入hello进行测试");
        }
    }
}
