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
    public partial class Form2 : Form {
        public Form2() {
            InitializeComponent();
        }

        private void Form2_MouseClick(object sender, MouseEventArgs e) {
            Graphics g = this.CreateGraphics();
            Rectangle rect = new Rectangle(e.X,e.Y,20,20);
            SolidBrush solidBrush = new SolidBrush(Color.Red);
            g.FillRectangle(solidBrush, rect);
        }
    }
}
