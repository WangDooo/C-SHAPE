using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 四则运算器 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        int total = 0;
        int correct = 0;
        int answer = 0;
        int times = 0;

        private void Form1_Load(object sender, EventArgs e) {
            radioButton1.Checked = true;
            textBox1.ReadOnly = true;
            label1.Text = "0:00:00";
            timer1.Interval = 1000;
            button1.Enabled = false;
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e) {
            times = 60;
            timer1.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e) {
            int h,m,s;
            times--;
            if (times <= 0) {
                timer1.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
            } else {
                h = times / 3600;
                m = times / 60 % 60;
                s = times % 60;
                label1.Text = h.ToString()+":"+m.ToString()+":"+s.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            int op1, op2, op;
            label3.Enabled = false;
            label3.Text = "";
            Random r = new Random();
            op1 = r.Next(101);
            if (radioButton1.Checked) {
                op2 = r.Next(1,101);
                op = r.Next(2);
            } else {
                op2 = r.Next(1,51);
                op = r.Next(2,4);
            }

            switch (op) {
                case 0: 
                    textBox1.Text = op1.ToString()+" + "+op2.ToString()+" = ";
                    answer = op1 + op2;
                    break;
                case 1:
                    while(op1<op2){
                        op2 = r.Next(1,101);
                    }
                    answer = op1 - op2;
                    textBox1.Text = op1.ToString()+" - "+op2.ToString()+" = ";
                    break;
                case 2: 
                    textBox1.Text = op1.ToString()+" X "+op2.ToString()+" = ";
                    answer = op1 * op2;
                    break;
                case 3:
                    textBox1.Text = op1.ToString()+" / "+op2.ToString()+" = ";
                    answer = op1 / op2;
                    if(op1 < op2 || op1%op2 != 0 || op2== 0) {
                        label3.Enabled = true;
                        label3.Text = "请重新出题";
                    }
                    break;
            }
            textBox2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e) {
            if(textBox2.Text == "") {
                MessageBox.Show("文本框目前是空值");
            } else {
                int result = int.Parse(textBox2.Text);
                total++;
                if(result == answer) {
                    correct++;
                    listBox1.Items.Add(textBox1.Text+textBox2.Text+"\t正确");
                } else {
                    listBox1.Items.Insert(0, textBox1.Text+textBox2.Text+"\t错误");
                }
                double c = correct * 100 / total;
                label4.Text = c.ToString()+"%";
            }
        }
    }
}
