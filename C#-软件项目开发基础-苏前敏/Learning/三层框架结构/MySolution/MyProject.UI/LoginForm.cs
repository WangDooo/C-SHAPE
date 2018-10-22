using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyProject.BLL;
using MyProject.Models;

namespace MyProject.UI {
    public partial class LoginForm : Form {
        public LoginForm() {
            InitializeComponent();
        }
        UserManager um = new UserManager();
        private void button1_Click(object sender, EventArgs e) {
            User user = new User();
            user.UserName = textBox1.Text;
            user.UserPassword = textBox2.Text;
            bool result = um.login(user);
            if (result) {
                MessageBox.Show("登陆成功");
                MainForm f = new MainForm();
                f.Show();
            } else {
                MessageBox.Show("登录失败");
            }
        }
    }
}
