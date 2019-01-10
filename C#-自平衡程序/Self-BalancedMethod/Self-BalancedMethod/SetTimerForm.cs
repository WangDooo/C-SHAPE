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

        // 只允许键入数字
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) {
            if( e.KeyChar != '\b') { // 这是允许输入退格键    
                if ((e.KeyChar<'0') || (e.KeyChar>'9')) {  //这是允许输入0-9数字  
                    e.Handled = true;  
                }  
            }
        }
    }
}
