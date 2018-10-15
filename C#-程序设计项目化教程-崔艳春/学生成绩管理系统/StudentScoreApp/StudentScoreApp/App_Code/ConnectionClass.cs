using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace StudentScoreApp.App_Code {
    class ConnectionClass {
        public static string GetConStr {
            get {
                return "Data Source=DESKTOP-403292K\\WANGDOO; Integrated Security=True; database=ScoreDB";
            }
        }

        public static SqlConnection getConnection() {
            SqlConnection myCon;
            try {
                string strSqlCon = "Data Source=DESKTOP-403292K\\WANGDOO; Integrated Security=True; database=ScoreDB";
                myCon = new SqlConnection(strSqlCon);
            } catch(Exception e) {
                throw e;
            }
            return myCon;
        }
        /// <summary>
        /// 创建一个DataSet对象
        /// </summary>
        /// <param name="strSqlCommand">SQL语句</param>
        /// <param name="strTable">表名</param>
        /// <returns></returns>
        public DataSet getDataSet(string strSqlCommand, string strTable) {
            SqlConnection sqlcon = getConnection();
            DataSet myds;
            try {
                sqlcon.Open();
                // 用于填充DataSet和更新数据库的一组数据命令和一个数据库连接
                SqlDataAdapter sqlda = new SqlDataAdapter(strSqlCommand, sqlcon);
                // DataSet是数据的内存驻留表示形式， 表示一个数据集
                myds = new DataSet();
                // 填充DataSet数据集
                sqlda.Fill(myds, strTable);
            } catch (Exception e){
                throw e;
            } finally {
                sqlcon.Close();
            }
            return myds;
        }
    }
}
