using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_BalancedMethod {
    [Serializable]
    public class SaveData {
        // 记录项目编号
        public string ProjectNumber = ShareClass.ProjectNumber;
        // 记录测试时间
        public string TestYear = ShareClass.TestYear;
        public string TestMonth = ShareClass.TestMonth;
        public string TestDay = ShareClass.TestDay;
        // 记录工地名称
        public string SiteName = ShareClass.SiteName;
        // 记录试验桩号
        public string PileNumber = ShareClass.PileNumber;
        // 记录桩长
        public string PileLength = ShareClass.PileLength;
        // 记录桩径
        public string PileDiameter = ShareClass.PileDiameter;
        // 记录文件保存路径
        [NonSerialized]
        public string FileName = ShareClass.FileName;
        //
        public SaveData() {

        }
    }
}
