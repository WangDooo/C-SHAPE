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
    public partial class Form3 : Form {
        public Form3() {
            InitializeComponent();
        }

        Graphics g;
        Point previous = new Point(1,1);
        private void Form3_Load(object sender, EventArgs e) {
            g = pictureBox1.CreateGraphics();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
            Pen pen = new Pen(Color.Blue, 5);
            g.DrawLine(pen,previous.X,previous.Y,e.X,e.Y);
            previous.X = e.X;
            previous.Y = e.Y;
        }
    }
}
