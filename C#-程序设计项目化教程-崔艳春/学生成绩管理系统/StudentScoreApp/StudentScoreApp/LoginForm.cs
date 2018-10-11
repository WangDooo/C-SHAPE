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
                    else if (string.IsNullOrEmpty(txtPwd.Text.Trim())) {
                        MessageBox.Show("请输入密码");
                    } 
                    else {
                        mycommand.CommandText = "select * from Student where ID=@name and Password =@pwd";
                        SqlParameter TName = new SqlParameter ("@name", SqlDbType.NVarChar);
                        SqlParameter TPwd = new SqlParameter ("@pwd", SqlDbType.NVarChar);
                        mycommand.Parameters.Add(TName);
                        mycommand.Parameters.Add(TPwd);
                        TName.Value = txtID.Text.Trim();
                        TPwd.Value = txtPwd.Text.Trim();
                        mydr = mycommand.ExecuteReader();
                        if (mydr.HasRows) {
                            mydr.Read();
                            App_Code.ShareClass.ID = mydr["ID"].ToString();
                            App_Code.ShareClass.Name = mydr["StudentName"].ToString();
                            App_Code.ShareClass.Type = "2";
                            Thread t = new Thread(new ThreadStart(StudentShow));
                            t.Start();
                            this.Close();
                        } 
                        else {
                            MessageBox.Show("用户名和密码不匹配，请重新输入");
                            txtPwd.Clear();
                        }
                    }
                } 
                else {
                    MessageBox.Show("用户类别不符，请在列表中选择。");
                }
                mycon.Close();
            }
            catch(Exception e1) {
                MessageBox.Show("连接存在问题");
            }
        }

        private void btnExit_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            if (lblmessage.Left >= this.Width) {
                lblmessage.Left = -lblmessage.Width;
            } 
            else {
                lblmessage.Left += 10;
            }
        }
    }
}
