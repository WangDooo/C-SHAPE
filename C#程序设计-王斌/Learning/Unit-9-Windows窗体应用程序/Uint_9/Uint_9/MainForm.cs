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
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e) {
            //NewForm newForm = new NewForm();
            //newForm.Show();
            DialogResult dr = MessageBox.Show("是否打开新窗体？","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if(dr == DialogResult.Yes) {
                MessageForm messageForm = new MessageForm();
                messageForm.Show();
            }else if(dr == DialogResult.No) {
                this.Close();
            }
        }
    }
}
