using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_BalancedMethod {
    [Serializable]
    class ShareClass {
        // 记录项目编号
        public static string _ProjectNumber = "";
        public static string ProjectNumber {
            get { return ShareClass._ProjectNumber; }
            set { ShareClass._ProjectNumber = value; }
        }
        // 记录测试时间
        public static string _TestYear = "";
        public static string TestYear {
            get { return ShareClass._TestYear; }
            set { ShareClass._TestYear = value; }
        }
        private static string _TestMonth = "";
        public static string TestMonth {
            get { return ShareClass._TestMonth; }
            set { ShareClass._TestMonth = value; }
        }
        private static string _TestDay = "";
        public static string TestDay {
            get { return ShareClass._TestDay; }
            set { ShareClass._TestDay = value; }
        }
        // 记录工地名称
        private static string _SiteName = "";
        public static string SiteName {
            get { return ShareClass._SiteName; }
            set { ShareClass._SiteName = value; }
        }
        // 记录试验桩号
        private static string _PileNumber = "";
        public static string PileNumber {
            get { return ShareClass._PileNumber; }
            set { ShareClass._PileNumber = value; }
        }
        // 记录桩长
        private static string _PileLength = "";
        public static string PileLength {
            get { return ShareClass._PileLength; }
            set { ShareClass._PileLength = value; }
        }
        // 记录桩径
        private static string _PileDiameter = "";
        public static string PileDiameter {
            get { return ShareClass._PileDiameter; }
            set { ShareClass._PileDiameter = value; }
        }
        // 等效转换参数 W 荷载箱上部桩的自重与附和重量之和(kN)
        private static double _ParmW = 0;
        public static double ParmW {
            get { return ShareClass._ParmW; }
            set { ShareClass._ParmW = value; }
        }
        // 等效转换参数 gamma1 受检桩的抗压摩阻力转换系数
        private static double _ParmGamma1 = 0;
        public static double ParmGamma1 {
            get { return ShareClass._ParmGamma1; }
            set { ShareClass._ParmGamma1 = value; }
        }
        // 等效转换参数 Lu(m) 上段桩长度
        private static double _ParmLu = 0;
        public static double ParmLu {
            get { return ShareClass._ParmLu; }
            set { ShareClass._ParmLu = value; }
        }
        // 等效转换参数 Ep(Mpa) 桩身弹性模量
        private static double _ParmEp = 0;
        public static double ParmEp {
            get { return ShareClass._ParmEp; }
            set { ShareClass._ParmEp = value; }
        }
        // 等效转换参数 Ap(mm^2) 桩身截面积 
        private static double _ParmAp = 0;
        public static double ParmAp {
            get { return ShareClass._ParmAp; }
            set { ShareClass._ParmAp = value; }
        }
        // 等效转换参数是否有输入 bool判断
        private static bool _ParmBool = false;
        public static bool ParmBool {
            get { return ShareClass._ParmBool; }
            set { ShareClass._ParmBool = value; }
        }
        // 记录文件保存路径
        private static string _FileName = "";
        public static string FileName {
            get { return ShareClass._FileName; }
            set { ShareClass._FileName = value; }
        }
    }
}
