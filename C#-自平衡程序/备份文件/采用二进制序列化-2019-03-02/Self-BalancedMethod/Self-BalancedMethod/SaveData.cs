using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_BalancedMethod {
    [Serializable]
    public class SaveData {
        // 记录项目编号
        public string ProjectNumber;
        // 记录测试时间
        public string TestYear;
        public string TestMonth;
        public string TestDay;
        // 记录工地名称
        public string SiteName;
        // 记录试验桩号
        public string PileNumber;
        // 记录桩长
        public string PileLength;
        // 记录桩径
        public string PileDiameter;
        // 等效转换参数 W 荷载箱上部桩的自重与附和重量之和(kN)
        public double ParmW;
        // 等效转换参数 gamma1 受检桩的抗压摩阻力转换系数
        public double ParmGamma1;
        // 等效转换参数 Lu(m) 上段桩长度
        public double ParmLu;
        // 等效转换参数 Ep(Mpa) 桩身弹性模量
        public double ParmEp;
        // 等效转换参数 Ap(mm^2) 桩身截面积 
        public double ParmAp;
        // 等效转换参数是否有输入 bool判断
        public bool ParmBool;
        // 记录试验数据
        public List<DataClass> Data = new List<DataClass>();
        // 记录文件保存路径
        public string FileName;
        //
        public SaveData() {  
            // 项目基本信息
            ProjectNumber = ShareClass.ProjectNumber;
            TestYear = ShareClass.TestYear;
            TestMonth = ShareClass.TestMonth;
            TestDay = ShareClass.TestDay;
            SiteName = ShareClass.SiteName;
            PileNumber = ShareClass.PileNumber;
            PileLength = ShareClass.PileLength;
            PileDiameter = ShareClass.PileDiameter;
            // 自平衡参数
            ParmW = ShareClass.ParmW;
            ParmGamma1 = ShareClass.ParmGamma1;
            ParmLu = ShareClass.ParmLu;
            ParmEp = ShareClass.ParmEp;
            ParmAp = ShareClass.ParmAp;
            ParmBool = ShareClass.ParmBool;
            // 文件地址
            FileName = ShareClass.FileName;
            // 试验数据
            Data = ShareClass.Data;
        }
    }
}
