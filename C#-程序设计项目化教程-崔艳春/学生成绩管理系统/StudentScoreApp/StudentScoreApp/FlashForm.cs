using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace StudentScoreApp {
    public partial class FlashForm : Form {
        public FlashForm() {
            InitializeComponent();
        }

        private void linkAdminLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            AdminLoginForm adminLoginForm = new AdminLoginForm();
            adminLoginForm.Show();
            this.Close();
        }

        private void linkExit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            this.Close();
        }

        public void LoginShow() {
            LoginForm loginForm = new LoginForm();
            Application.Run(loginForm);
        }

        private void linkUserLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Thread t = new Thread(new System.Threading.ThreadStart(LoginShow));
            t.Start();
            this.Close();
        }
    }
}
