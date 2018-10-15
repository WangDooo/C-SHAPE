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
    public partial class MyScoreForm : Form {
        public MyScoreForm() {
            InitializeComponent();
        }

        private void MyScoreForm_Load(object sender, EventArgs e) {
            // 初始化学期
            cmbTerm.Items.Clear();
            cmbTerm.Items.Add("2012-2013第一学期");
            cmbTerm.Items.Add("2012-2013第二学期");
            cmbTerm.Items.Add("2013-2014第一学期");
            cmbTerm.Items.Add("2013-2014第二学期");
            // 初始化学号
            lblname.Text = App_Code.ShareClass.Name;
        }
        private SqlConnection mycon = new SqlConnection(App_Code.ConnectionClass.GetConStr);
        private SqlCommand mycommand = null;
        App_Code.ConnectionClass conclass = new App_Code.ConnectionClass();
        DataSet myds = null;
        private void btnDisplay_Click(object sender, EventArgs e) {
            mycommand = mycon.CreateCommand();
            if(cmbTerm.SelectedIndex == -1) {
                mycommand.CommandText = "DisplayScore '"+App_Code.ShareClass.ID+"','0'";
            } else {
                mycommand.CommandText = "DisplayScore '"+App_Code.ShareClass.ID+"','"+cmbTerm.SelectedItem+"'";
            }
            myds = conclass.getDataSet(mycommand.CommandText,"Score");
            // MessageBox.Show(cmbTerm.SelectedItem.ToString());
            dataGridView1.DataSource = myds.Tables["Score"];
        }
    }
}
