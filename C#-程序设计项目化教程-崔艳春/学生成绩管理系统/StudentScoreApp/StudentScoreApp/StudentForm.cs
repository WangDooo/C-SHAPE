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
    public partial class StudentForm : Form {
        public StudentForm() {
            InitializeComponent();
        }

        // 设置背景图片方法
        private void Picture_Resize() {
            //this.BackgroundImage = pictureBox1.Image;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
        // 主窗体Resize方法
        private void StudentForm_Resize(object sender, EventArgs e) {
            Picture_Resize();
        }
        // 初始化窗体
        private void StudentForm_Load(object sender, EventArgs e) {
            Picture_Resize();
            toolStripStatusLabel2.Text = App_Code.ShareClass.Name;
            toolStripStatusLabel3.Text = DateTime.Now.ToShortDateString();
            toolStripStatusLabel4.Text = DateTime.Now.ToLongTimeString();
            toolStripStatusLabel3.Alignment = ToolStripItemAlignment.Right;
            toolStripStatusLabel4.Alignment = ToolStripItemAlignment.Right;
        }

        private void timer1_Tick(object sender, EventArgs e) {
            toolStripStatusLabel3.Text = DateTime.Now.ToShortDateString();
            toolStripStatusLabel4.Text = DateTime.Now.ToLongTimeString();
        }

        private void toolStripButton1_Click(object sender, EventArgs e) {
            ModifyPwdForm modifyPwdForm = new ModifyPwdForm();
            modifyPwdForm.ShowDialog();
        }

        private void toolStripButton4_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
