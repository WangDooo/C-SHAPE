using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 打字小游戏 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        
        double n, m;

        private void timer1_Tick(object sender, EventArgs e) {
            char cc;
            Random ra = new Random();
            if(lblChar1.Text == "" || lblChar1.Top >= this.Height) {
                lblChar1.Top = -20;
                cc = (char)(ra.Next(65,90));
                lblChar1.Text = Convert.ToString(cc);
            } else {
                lblChar1.Top = lblChar1.Top + 10;
            }
            if(lblChar2.Text == "" || lblChar2.Top >= this.Height) {
                lblChar2.Top = -10;
                cc = (char)(ra.Next(65,90));
                lblChar2.Text = Convert.ToString(cc);
            } else {
                lblChar2.Top = lblChar2.Top + 10;
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e) {
            m += 1;
            if(e.KeyChar == Convert.ToChar(lblChar1.Text)) {
                n += 1;
                lblChar1.Text = "";
            }else if(e.KeyChar == Convert.ToChar(lblChar2.Text)){
                n += 1;
                lblChar2.Text = "";
            }
            txtRightNum.Text = (n/m*100).ToString("F2")+"%";
            txtTotal.Text = m.ToString();
        }

        private void btnStart_Click(object sender, EventArgs e) {
            if (btnStart.Text == "开始") {
                m = 0;
                n = 0;
                btnStart.Text = "停止";
                timer1.Enabled = true;
                lblChar1.Text = "";
                lblChar2.Text = "";
            } else {
                btnStart.Text = "开始";
                timer1.Enabled = false;
                txtRightNum.Text = (n / m * 100).ToString("F2")+"%";
                txtTotal.Text = m.ToString();
            }
        }
    }
}
