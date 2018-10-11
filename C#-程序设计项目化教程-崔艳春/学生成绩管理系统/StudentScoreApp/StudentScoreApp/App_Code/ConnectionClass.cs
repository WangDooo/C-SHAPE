using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentScoreApp.App_Code {
    class ConnectionClass {
        public static string GetConStr {
            get {
                return "Data Source=DESKTOP-403292K\\WANGDOO; Integrated Security=True; database=ScoreDB";
            }
        }

    }
}
