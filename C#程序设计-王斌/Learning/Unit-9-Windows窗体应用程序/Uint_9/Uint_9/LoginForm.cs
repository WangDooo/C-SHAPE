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
    public partial class LoginForm : Form {
        public LoginForm() {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            string username = textBox1.Text;
            string password = textBox2.Text;
            if ("qwe".Equals(username) && "123".Equals(password)) {
                MessageBox.Show("登陆成功！");
            } else {
                MessageBox.Show("Wrong!");
            }
        }
    }
}
