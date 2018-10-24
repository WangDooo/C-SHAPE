using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsForm {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e) {
            Graphics g = this.CreateGraphics();
            Rectangle rect = new Rectangle(e.X,e.Y,20,20);
            Pen pen = new Pen(Color.Red);
            g.DrawRectangle(pen, rect);
        }
    }
}
