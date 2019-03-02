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
    public partial class CreateNewFolderForm : Form {
        public CreateNewFolderForm() {
            InitializeComponent();
            button1.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) {        
            try{
                ShareClass.ProjectNumber = txtProjectNumber.Text;
                ShareClass.TestYear = txtTestYear.Text;
                ShareClass.TestMonth = txtTestMonth.Text;
                ShareClass.TestDay = txtTestDay.Text;
                ShareClass.SiteName = txtSiteName.Text;
                ShareClass.PileNumber = txtPileNumber.Text;
                ShareClass.PileLength = txtPileLength.Text;
                ShareClass.PileDiameter = txtPileDiameter.Text;
                this.Close();
            } 
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
