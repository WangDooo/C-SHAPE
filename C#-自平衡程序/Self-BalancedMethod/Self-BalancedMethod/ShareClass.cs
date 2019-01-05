using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_BalancedMethod {
    class ShareClass {
        // 记录项目编号
        private static string _ProjectNumber = "";
        public static string ProjectNumber {
            get { return ShareClass._ProjectNumber; }
            set { ShareClass._ProjectNumber = value; }
        }
        // 记录测试时间
        private static string _TestYear = "";
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
    }
}
