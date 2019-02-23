using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace 数据库连接 {
    class Program {
        static void Main(string[] args) {
            string constr = "Data Source=DESKTOP-403292K\\WANGDOO; Integrated Security=True; database=ScoreDB";
            SqlConnection sqlConn = new SqlConnection(constr);
            sqlConn.Open();

            SqlCommand mycomm = sqlConn.CreateCommand();
            mycomm.CommandText = "select count(*) from student";
            int sum = (int)(mycomm.ExecuteScalar());
            Console.WriteLine(sum);
            Console.WriteLine("-----------");
            mycomm.CommandText = "select * from Student";
            SqlDataReader mydr = mycomm.ExecuteReader();
            string str = "";
            while (mydr.Read()) {
                str += mydr["ID"]+"\t";
                str += mydr["StudentName"];
                str += "\n";
            }
            Console.WriteLine(str);
            Console.WriteLine("-----------");
            mycomm.CommandText = "select * from Student where ID='1' and Passwoed ='123'";

        }
    }
}
