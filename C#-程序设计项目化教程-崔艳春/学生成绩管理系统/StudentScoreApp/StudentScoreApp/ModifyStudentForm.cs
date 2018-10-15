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
    public partial class ModifyStudentForm : Form {
        public ModifyStudentForm() {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化窗体，显示详细信息
        /// </summary>
        private void ModifyStudentForm_Load(object sender, EventArgs e) {
            txtID.Text = App_Code.ShareClass.ID;
            Init();
        }

        private void Init() {
            string sqlstr = "select * from Student where ID='"+txtID.Text.Trim()+"'";
            App_Code.ConnectionClass myconnection = new App_Code.ConnectionClass();
            DataSet myDataSet = myconnection.getDataSet(sqlstr,"Student");  
            txtName.Text = myDataSet.Tables["Student"].Rows[0]["StudentName"].ToString();
            txtPhone.Text = myDataSet.Tables["Student"].Rows[0]["Phone"].ToString();
            txtIdenID.Text = myDataSet.Tables["Student"].Rows[0]["IdentityID"].ToString();
            txtAddress.Text = myDataSet.Tables["Student"].Rows[0]["Address"].ToString();
            string sex = myDataSet.Tables["Student"].Rows[0]["Sex"].ToString();
            if(sex == "男") {
                rbMale.Checked = true;
            } else {
                rbFemale.Checked = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }
        /// <summary>
        /// 修改学生信息
        /// 验证输入合法性
        /// 执行SQL
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(txtName.Text)) {
                MessageBox.Show("请输入姓名");
            }else if(string.IsNullOrEmpty(txtPhone.Text)){
                MessageBox.Show("请输入联系方式");
            }else if (string.IsNullOrEmpty(txtIdenID.Text)) {
                MessageBox.Show("请输入身份证号码");
            }else if (string.IsNullOrEmpty(txtAddress.Text)) {
                MessageBox.Show("请输入通讯地址");
            } else {
                string sex = "";
                if (rbMale.Checked) {
                    sex = "男";
                } else {
                    sex = "女";
                }
                SqlConnection mycon = null;
                try {
                    mycon = new SqlConnection(App_Code.ConnectionClass.GetConStr);
                    SqlCommand mycommand = mycon.CreateCommand();
                    mycommand.CommandText = "update Student set StudentName=@StudentName,Sex=@Sex,Phone=@Phone,IdentityID=@IdentityID,Address=@Address where ID=@ID";
                    SqlParameter ID = new SqlParameter("@ID", SqlDbType.NVarChar);
                    SqlParameter StudentName = new SqlParameter("@StudentName", SqlDbType.NVarChar);
                    SqlParameter Sex = new SqlParameter("@Sex", SqlDbType.NVarChar);
                    SqlParameter Phone = new SqlParameter("@Phone", SqlDbType.NVarChar);
                    SqlParameter IdentityID = new SqlParameter("@IdentityID", SqlDbType.NVarChar);
                    SqlParameter Address = new SqlParameter("@Address", SqlDbType.NVarChar);
                    mycommand.Parameters.Add(StudentName);
                    mycommand.Parameters.Add(Sex);
                    mycommand.Parameters.Add(Phone);
                    mycommand.Parameters.Add(IdentityID);
                    mycommand.Parameters.Add(Address);
                    mycommand.Parameters.Add(ID);
                    ID.Value = txtID.Text.Trim();
                    StudentName.Value = txtName.Text.Trim();
                    Sex.Value = sex;
                    Phone.Value = txtPhone.Text.Trim();
                    IdentityID.Value = txtIdenID.Text.Trim();
                    Address.Value = txtAddress.Text.Trim();
                    mycon.Open();
                    mycommand.ExecuteNonQuery();
                    MessageBox.Show("修改成功");
                    this.Close();
                } catch (Exception) {
                    MessageBox.Show("数据库有问题");
                } finally {
                    mycon.Close();
                }
            }
        }
    }
}
