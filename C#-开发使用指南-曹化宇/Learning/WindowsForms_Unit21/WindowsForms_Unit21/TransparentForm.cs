using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms_Unit21 {
    public partial class TransparentForm : Form {
        private int x, y;

        public TransparentForm() {
            InitializeComponent();
        }

        private void TransparentForm_MouseDown(object sender, MouseEventArgs e) {
            x = e.X;
            y = e.Y;
        }

        private void TransparentForm_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                int xOffset = e.X - x;
                int yOffset = e.Y - y;
                this.Left += xOffset;
                this.Top += yOffset;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void TransparentForm_Load(object sender, EventArgs e) {
            this.Text="";
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TransparencyKey = this.BackColor;
        }
    }
}
