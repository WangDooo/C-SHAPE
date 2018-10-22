using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MyProject.Models;

namespace MyProject.DAL
{
    public class UserService{
        public bool login(User user) {
            bool loginResult = false;
            string sql = "select count(*) from Users where username='" + user.UserName + "' and userpassword ='" + user.UserPassword + "'";
            string strSqlCon = "Data Source=DESKTOP-403292K\\WANGDOO; Integrated Security=True; database=ScoreDB";
            SqlConnection cn = new SqlConnection(strSqlCon);
            cn.Open();
            SqlCommand cmd = new SqlCommand(sql, cn);
            int result = (int)cmd.ExecuteScalar();
            cn.Close();
            if (result > 0) {
                loginResult = true;
            } else {
                loginResult = false;
            }
            return loginResult;
        }
    }
}
