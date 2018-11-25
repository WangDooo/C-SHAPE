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
    public partial class Main : Form {
        public Main() {
            InitializeComponent();
        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }
        // 显示“关于”
        private void 关于AToolStripMenuItem_Click(object sender, EventArgs e) {
            AboutUsForm aboutUsForm = new AboutUsForm();
            aboutUsForm.ShowDialog();
        }

        private void 工具栏TToolStripMenuItem_Click(object sender, EventArgs e) {
            if (toolStrip1.Visible) {
                toolStrip1.Visible = false;
                工具栏TToolStripMenuItem.Checked = false;
            } else {
                toolStrip1.Visible = true;
                工具栏TToolStripMenuItem.Checked = true;
            }
        }

        // 状态栏的显示和隐藏
        private void 状态栏SToolStripMenuItem_Click(object sender, EventArgs e) {
            if (statusStrip1.Visible) {
                statusStrip1.Visible = false;
                状态栏SToolStripMenuItem.Checked = false;
            } else {
                statusStrip1.Visible = true;
                状态栏SToolStripMenuItem.Checked = true;
            }
            
        }

        private void 自平衡等效换算参数ZToolStripMenuItem_Click(object sender, EventArgs e) {
            EquivalentParametersForm equivalentParametersForm = new EquivalentParametersForm();
            equivalentParametersForm.ShowDialog();
        }

        // 监听快捷键动作 Ctrl+Z
        private void Main_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.X && e.Control) {
                e.Handled = true;       //将Handled设置为true，指示已经处理过KeyDown事件   
                this.Close();               //执行动作
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            this.toolStripStatusLabelTime.Text = DateTime.Now.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e) {
            //10毫秒定时，主要用来网络定时周期性数据读取

        }
    }
}
