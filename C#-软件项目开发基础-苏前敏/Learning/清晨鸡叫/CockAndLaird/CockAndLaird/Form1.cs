using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CockAndLaird {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            Cock cock = new Cock(textBox1.Text);
            Laird laird = new Laird(textBox2.Text);
            cock.GetUp += new SayGetUpDelegate(laird.SayGetUp);
            cock.Crow();
        }
    }
}
