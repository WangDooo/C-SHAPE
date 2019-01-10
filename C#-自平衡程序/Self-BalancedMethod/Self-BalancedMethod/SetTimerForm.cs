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
    public delegate void SetTimerDelegate(String value); // 定义一个设置采集频率的委托
    public partial class SetTimerForm : Form {
        
        public SetTimerForm() {
            InitializeComponent();
        }

        public event SetTimerDelegate SetTimerEvent;

        private void button1_Click(object sender, EventArgs e) {
            // 触发事件
            SetTimerEvent(textBox1.Text);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
