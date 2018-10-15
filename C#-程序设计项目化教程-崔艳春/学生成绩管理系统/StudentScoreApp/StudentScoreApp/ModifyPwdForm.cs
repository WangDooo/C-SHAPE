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
    public partial class ModifyPwdForm : Form {
        public ModifyPwdForm() {
            InitializeComponent();
        }

        // 初始化窗体，在只读文本框中显示登录用户的账号
        private void ModifyPwdForm_Load(object sender, EventArgs e) {
            txtID.Text = App_Code.ShareClass.ID;
        }       

        /// <summary>
        /// 修改学生密码
        /// 判断密码格式，两次输入一致
        /// 执行SQL语句，修改当前用户的密码
        /// </summary>

        private void btnOK_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(txtPwd.Text.Trim())) {
                MessageBox.Show("请输入新密码");
            } 
            else {
                if(txtPwd.Text.Trim() != txtPwd2.Text.Trim()) {
                    MessageBox.Show("两次密码不一致！");
                } 
                else {
                    try {
                        SqlConnection mycon = new SqlConnection(App_Code.ConnectionClass.GetConStr);
                        SqlCommand mycommand = mycon.CreateCommand();
                        string sqlstr = "update Student ";
                        mycommand.CommandText = sqlstr+"set Password=@pwd where ID = @ID";
                        SqlParameter Id = new SqlParameter("@ID", SqlDbType.NVarChar);
                        SqlParameter Pwd = new SqlParameter("@pwd", SqlDbType.NVarChar);
                        mycommand.Parameters.Add(Pwd);
                        mycommand.Parameters.Add(Id);
                        Id.Value = txtID.Text.Trim();
                        Pwd.Value = txtPwd.Text.Trim();
                        mycon.Open();
                        int i = mycommand.ExecuteNonQuery();
                        mycon.Close();
                        if (i != 0) {
                            MessageBox.Show("密码修改完成");
                            this.Close();
                        } else {
                            MessageBox.Show("密码修改错误");
                        }
                    } 
                    catch(Exception e1) {
                        MessageBox.Show("数据库连接存在问题");
                        throw e1;
                    }
                }
            } 
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
