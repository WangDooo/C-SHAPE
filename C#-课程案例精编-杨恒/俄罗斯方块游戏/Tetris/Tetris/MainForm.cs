using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Tetris {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            this.Initiate();
        }

        private void Initiate() {
            try {
                XmlDocument doc = new XmlDocument();
                doc.Load("");
                XmlNodeList nodes = doc.DocumentElement.ChildNodes;
                // this.startLevel = 
            } catch {
                bool trans = false;
                Keys keys = new Keys[5];
                keys[0] = Keys.Left;


            }
        }
    }
}
