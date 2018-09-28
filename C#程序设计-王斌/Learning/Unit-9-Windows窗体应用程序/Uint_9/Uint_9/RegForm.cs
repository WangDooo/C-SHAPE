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
    //声明委托 和 事件
    public delegate void TransfDelegate(string name,string pwd);
    public partial class RegForm : Form {
        public RegForm() {
            InitializeComponent();
        }
        // 定义事件
        public event TransfDelegate TransfEvent;

        private void button1_Click(object sender, EventArgs e) {
            string name = textBox1.Text;
            string pwd = textBox2.Text;
            string repwd = textBox3.Text;
            if (string.IsNullOrEmpty(name)) {
                MessageBox.Show("用户名不能为空！");
                return;
            }else if (string.IsNullOrEmpty(textBox2.Text)) {
                MessageBox.Show("密码不能为空！");
                return;
            }else if (!textBox2.Text.Equals(textBox3.Text)) {
                MessageBox.Show("两次密码输入不一致！");
                return;
            }
            // 将用户名密码传递到主窗口中 但这里是新生产一个主窗口
            //WidgetForm widgetForm = new WidgetForm(name, pwd);
            //widgetForm.Show();
            //this.Close();
            TransfEvent(name,pwd);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
