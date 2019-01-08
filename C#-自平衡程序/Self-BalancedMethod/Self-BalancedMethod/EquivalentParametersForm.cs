using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Self_BalancedMethod {
    public partial class EquivalentParametersForm : Form {
        public EquivalentParametersForm() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            try{
                ShareClass.ParmW = Convert.ToDouble(txtParmW.Text);
                ShareClass.ParmGamma1 = Convert.ToDouble(txtParmGamma1.Text);
                ShareClass.ParmLu = Convert.ToDouble(txtParmLu.Text);
                ShareClass.ParmEp = Convert.ToDouble(txtParmEp.Text);
                ShareClass.ParmAp = Convert.ToDouble(txtParmAp.Text);
                ShareClass.ParmBool = true;
                this.Close();
            } 
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
