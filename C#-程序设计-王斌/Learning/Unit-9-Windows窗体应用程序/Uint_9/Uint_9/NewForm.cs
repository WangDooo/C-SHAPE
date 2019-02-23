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
    public partial class NewForm : Form {
        public NewForm() {
            InitializeComponent();
        }

        private void NewForm_MouseClick(object sender, MouseEventArgs e) {
            this.CenterToScreen();
        }

        private void NewForm_MouseDoubleClick(object sender, MouseEventArgs e) {
            this.Close();
        }
    }
}
