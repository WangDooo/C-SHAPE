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

        public bool trans;
        public Keys[] keys;
        public int level;
        public int startLevel;

        // 主界面Load事件代码
        private void Initiate() {
            try {
                XmlDocument doc = new XmlDocument();
                doc.Load("D:\\Coding\\Github\\C-SHAPE\\C#-课程案例精编-杨恒\\俄罗斯方块游戏\\Tetris\\setting.ini");
                XmlNodeList nodes = doc.DocumentElement.ChildNodes;
                this.startLevel = Convert.ToInt32(nodes[0].InnerText);
                this.level = this.startLevel;
                this.trans = Convert.ToBoolean(nodes[1].InnerText);
                keys = new Keys[5];
                for (int i = 0; i < nodes[2].ChildNodes.Count; i++) {
                    KeysConverter kc = new KeysConverter();
                    this.keys[i] = (Keys)(kc.ConvertFromString(nodes[2].ChildNodes[i].InnerText));
                }
            } catch {
                this.trans = false;
                keys = new Keys[5];
                keys[0] = Keys.Left;
                keys[1] = Keys.Right;
                keys[2] = Keys.Down;
                keys[3] = Keys.NumPad8;
                keys[4] = Keys.NumPad9;
                this.level = 1;
                this.startLevel = 1;
            }
            this.timer1.Interval = 500-50*(level-1);
            this.label4.Text = "级别： "+this.startLevel;
            if (trans) {
                this.TransparencyKey = Color.Black;
            }
        }

        // 游戏设置保存
        private void SaveSetting() {
            try {
                XmlDocument doc = new XmlDocument();
                XmlDeclaration xmlDec = doc.CreateXmlDeclaration("1.0","gb2312",null);

                XmlElement setting = doc.CreateElement("SETTING");
                doc.AppendChild(setting);

                XmlElement level = doc.CreateElement("LEVEL");
                level.InnerText = this.startLevel.ToString();
                setting.AppendChild(level);

                XmlElement trans = doc.CreateElement("TRANSPARENT");
                trans.InnerText = this.trans.ToString();
                setting.AppendChild(trans);

                XmlElement keys = doc.CreateElement("KEYS");
                setting.AppendChild(keys);
                foreach(Keys k in this.keys) {
                    KeysConverter kc = new KeysConverter();
                    XmlElement x = doc.CreateElement("SUBKEYS");
                    x.InnerText = kc.ConvertToString(k);
                    keys.AppendChild(x);
                }

                XmlElement root = doc.DocumentElement;
                doc.InsertBefore(xmlDec,root);
                doc.Save("D:\\Coding\\Github\\C-SHAPE\\C#-课程案例精编-杨恒\\俄罗斯方块游戏\\Tetris\\setting.ini");
            }
            catch(Exception xe) {
                MessageBox.Show(xe.Message);
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e) {
            this.SaveSetting();
        }

    }
}
