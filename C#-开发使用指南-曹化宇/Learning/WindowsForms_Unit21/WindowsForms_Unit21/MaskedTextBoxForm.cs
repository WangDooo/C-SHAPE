﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms_Unit21 {
    public partial class MaskedTextBoxForm : Form {
        public MaskedTextBoxForm() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            //   textBox1.Text = maskedTextBox1.Text;
            textBox1.Text = CCheckData.IsDate(maskedTextBox1.Text).ToString();
        }
    }
}
