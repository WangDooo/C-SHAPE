using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1 {
    public partial class WidgetForm : Form {
        public WidgetForm() { //string name = "", string pwd = ""
            InitializeComponent();
            //label5.Text = "用户名" + name;
            //label6.Text = "密码" + pwd;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            string temp = label1.Text;
            label1.Text = label2.Text;
            label2.Text = temp;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            label3.Text = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e) {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void button2_Click(object sender, EventArgs e) {
            RegForm regForm = new RegForm();
            regForm.Show();
        }
    }
}
