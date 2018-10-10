using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentScoreApp.App_Code {
    class ShareClass {
        // 记录登录用户ID
        private static string _ID;
        public static string ID {
            get { return _ID; }
            set { _ID = value; }
        }
        // 记录登录用户的类型
        private static string _Type;
        public static string Type {
            get { return _Type; }
            set { _Type = value; }
        }
        // 记录登录用户的姓名
        private static string _Name;
        public static string Name {
            get { return ShareClass._Name; }
            set { ShareClass._Name = value; }
        }
    }
}
