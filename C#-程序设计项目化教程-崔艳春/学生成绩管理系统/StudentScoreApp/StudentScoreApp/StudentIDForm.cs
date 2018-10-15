using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StudentScoreApp {
    public partial class StudentIDForm : Form {
        public StudentIDForm() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            try {
                SqlConnection mycon = new SqlConnection(App_Code.ConnectionClass.GetConStr);
            SqlCommand mycommand = mycon.CreateCommand();
            mycommand.CommandType = CommandType.StoredProcedure;
            mycommand.CommandText = "GetStudentNameByStudentID";
            SqlParameter studentid = mycommand.Parameters.Add("@StudentID", SqlDbType.NVarChar);
            studentid.Value = textBox1.Text.Trim();
            mycon.Open();
            string name = mycommand.ExecuteScalar().ToString();
            mycon.Close();
            MessageBox.Show(name);
            } catch (Exception) {
                MessageBox.Show("未找到该用户");
            }
        }
    }
}
