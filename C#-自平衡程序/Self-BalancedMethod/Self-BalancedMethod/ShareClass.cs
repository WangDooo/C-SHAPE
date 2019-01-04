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
    }
}
