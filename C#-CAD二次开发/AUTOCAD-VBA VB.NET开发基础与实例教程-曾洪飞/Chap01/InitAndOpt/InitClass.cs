using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;


[assembly: ExtensionApplication(typeof(InitAndOpt.InitClass))]
[assembly: CommandClass(typeof(InitAndOpt.OptimizeClass))]
namespace InitAndOpt
{
    
    public class InitClass : IExtensionApplication {
        public void Initialize() {
            Editor ed =Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("程序开始初始化...");
        }

        public void Terminate() {
            MessageBox.Show("程序结束..." );
            System.Diagnostics.Debug.WriteLine("程序结束...");
        }

        [CommandMethod("InitCommand")]
        public void InitCommand() {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("Test...");
        }
    }
}
