using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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
            DataSet myDataSet = myconnection.GetDataSet(sqlstr,"Student");  
        }
    }
}
