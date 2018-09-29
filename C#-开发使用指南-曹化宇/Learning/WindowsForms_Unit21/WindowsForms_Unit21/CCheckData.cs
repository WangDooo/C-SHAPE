using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForms_Unit21 {
    class CCheckData {
        public static bool IsDate(string s) {
            DateTime result;
            return DateTime.TryParse(s, out result);
        }
    }
}
