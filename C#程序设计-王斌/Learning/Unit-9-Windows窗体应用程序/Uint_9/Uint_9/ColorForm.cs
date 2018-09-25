using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1 {
    public partial class ColorForm : Form {
        public ColorForm() {
            InitializeComponent();
        }

        private void ColorForm_MouseClick(object sender, MouseEventArgs e) {
            this.BackColor = Color.Black;
        }

        private void ColorForm_MouseDoubleClick(object sender, MouseEventArgs e) {
            this.BackColor = Color.Blue;
        }

        private void ColorForm_Load(object sender, EventArgs e) {
            this.BackColor = Color.Red;
        }
    }
}
