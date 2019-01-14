using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 内存释放 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        public class Foo {
            public string name;
            public int totalBytesRead = 0;
            public const int BufferSize = 1600;
            public string readType = null;
            public byte[] buffer = new byte[BufferSize];
            public StringBuilder messageBuffer = new StringBuilder();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            Foo foo = new Foo();
            foo.name = Guid.NewGuid().ToString();
            textBox1.Text = foo.name;
            
            GC.Collect();
        }

        private void button2_Click(object sender, EventArgs e) {
            timer1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e) {
            timer1.Enabled = false;
        }
    }
}
