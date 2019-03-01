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
        // 记录试验数据
        public List<DataClass> Data = new List<DataClass>();
        // 记录文件保存路径
        public string FileName;
        //
        public SaveData() {  
            ProjectNumber = ShareClass.ProjectNumber;
            TestYear = ShareClass.TestYear;
            TestMonth = ShareClass.TestMonth;
            TestDay = ShareClass.TestDay;
            SiteName = ShareClass.SiteName;
            PileNumber = ShareClass.PileNumber;
            PileLength = ShareClass.PileLength;
            PileDiameter = ShareClass.PileDiameter;
            FileName = ShareClass.FileName;
            Data = ShareClass.Data;
        }
    }
}
