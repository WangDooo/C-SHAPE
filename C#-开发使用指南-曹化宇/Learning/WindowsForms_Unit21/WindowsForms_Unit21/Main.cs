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
    public partial class Main : Form {
        public Main() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            TransparentForm transparentForm = new TransparentForm();
            transparentForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e) {
            MaskedTextBoxForm maskedTextBoxForm = new MaskedTextBoxForm();
            maskedTextBoxForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e) {
            GroupBoxForm groupBoxForm = new GroupBoxForm();
            groupBoxForm.ShowDialog();
        }
    }
}
