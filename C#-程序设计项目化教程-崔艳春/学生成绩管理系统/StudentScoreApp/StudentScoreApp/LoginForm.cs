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
using System.Threading;

namespace StudentScoreApp {
    public partial class LoginForm : Form {
        public LoginForm() {
            InitializeComponent();
        }
        
        public void StudentShow() {
            StudentForm studentForm = new StudentForm();
            Application.Run(studentForm);
        }

        // btnLogin_Click
        private void btnLogin_Click(object sender, EventArgs e) {
            SqlConnection mycon;
            try {
                mycon = new SqlConnection(App_Code.ConnectionClass.GetConStr);
                SqlCommand mycommand = mycon.CreateCommand();
                SqlDataReader mydr;
                mycon.Open();
                if (cmbType.Text.Trim() == "教师") {
                    // 教师登录功能
                }
                else if (cmbType.Text.Trim() == "学生") {
                    if (string.IsNullOrEmpty(txtID.Text.Trim())) {
                        MessageBox.Show("请输入用户名");
                    }
                    else if (string.IsNullOrEmpty(txtID.Text.Trim())) {
                        MessageBox.Show("请输入密码");
                    } 
                    else {
                        Thread t = new Thread(new ThreadStart(StudentShow));
                        t.Start();
                        this.Close();
                    }
                } 
                else {
                    MessageBox.Show("用户类别不符，请在列表中选择。");
                }
                mycon.Close();
            }
            catch(Exception e1) {
                MessageBox.Show("连接问题");
            }
        }
    }
}
