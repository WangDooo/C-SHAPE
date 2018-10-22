using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartPigForm {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        public class SmartPig {
            private string name;
            private int weight;
            public SmartPig(string name) {
                this.name = name;
            }
            public SmartPig(string name, int weight) {
                this.name = name;
                this.weight = weight;
            }
            public void GetSafePos(int amount, int killnum, out int[] pig) {
                int i, j, flag;
                pig = new int[amount];
                for (i = 0; i < amount; i++) {
                    pig[i] = 1;
                }
                for (i = 0; i < killnum; i++) {
                    flag = 0;
                    for (j = 0; j < amount; j++) {
                        if(pig[j] == 1) {
                            flag++;
                        }
                        if (flag % 2 == 1) {
                            pig[j] = 0;
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            int[] safepos;
            SmartPig pig = new SmartPig(textBox1.Text);
            pig.GetSafePos(Convert.ToInt32(textBox2.Text),Convert.ToInt32(textBox3.Text),out safepos);
            for(int i = 0; i < Convert.ToInt32(textBox2.Text); i++) {
                if (safepos[i] == 1) {
                    textBox4.Text = textBox4.Text + (i+1).ToString()+"\t";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            textBox4.Text = "";
        }
    }
}
