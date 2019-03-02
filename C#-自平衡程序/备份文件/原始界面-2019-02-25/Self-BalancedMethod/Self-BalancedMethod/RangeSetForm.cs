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
    public partial class RangeSetForm : Form {
        public RangeSetForm() {
            InitializeComponent();
        }

        private void RangeSetForm_Load(object sender, EventArgs e) {
            ch_text.Text = "量程" + String.Format("{0:D2}", Main.CH_SEL_INDEX + 1);
            range_new.Text = Main.CH_RANGE[Main.CH_SEL_INDEX].ToString();
            range_new_big.Text = Main.CH_RANGE_BIG[Main.CH_SEL_INDEX].ToString();
            this.CenterToParent();
        }

        // 这里是通过返回类型告诉Main来确定发送的是设置的量程上限还是下限，数据放入到CH_RANGE数组中，这个数组，Main可见
        private void ok_button_Click(object sender, EventArgs e) {
            Main.CH_RANGE[Main.CH_SEL_INDEX] = Convert.ToSingle(range_new.Text);
            DialogResult = DialogResult.OK;
        }

        private void ok_button_full_Click(object sender, EventArgs e) {
            Main.CH_RANGE_BIG[Main.CH_SEL_INDEX] = Convert.ToSingle(range_new_big.Text);
            this.DialogResult = DialogResult.Yes;
        }

        private void cancel_button_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }
    }
}
