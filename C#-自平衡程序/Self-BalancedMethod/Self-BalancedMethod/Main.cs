using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Self_BalancedMethod {
    public partial class Main : Form {

        public static uint Is_connected = 0;
        mysocket sock = new mysocket();
        public static Main mfs;

        public Main() { //界面启动后第一个执行的。
            InitializeComponent();
            mfs = this;
        }
        // 退出程序
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }
        // 显示“关于”
        private void 关于AToolStripMenuItem_Click(object sender, EventArgs e) {
            AboutUsForm aboutUsForm = new AboutUsForm();
            aboutUsForm.ShowDialog();
        }
        // 工具栏的显示和隐藏
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
        // 显示“自平衡等效换算参数”
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
        // 设置连接状态
        public void SetConnect_state(uint ok){
            Is_connected = ok;
        }
        // 右下角显示系统时间
        private void timer1_Tick(object sender, EventArgs e) {
            this.toolStripStatusLabelTime.Text = DateTime.Now.ToString();
        }
        //10毫秒定时，主要用来网络定时周期性数据读取
        private void timer2_Tick(object sender, EventArgs e) {  
            if (Is_connected == 1)
            {
                // client.clientreceive();
                // Receive_Data_client();
                sock.ReadData();//定时的读网口操作
                SetConnect_text(("已连接"));
                toolStripButtonConnect.Text=("断开");
            }
            else
            {
                SetConnect_text(("已断开"));
                toolStripButtonConnect.Text=("连接");
            }
        }
        //设置tcp连接状态字符串
        public void SetConnect_text(String str){ 
            toolStripStatusLabelConnect.Text = str;
        }
        //读取连接状态
        public uint GetConnect_state(){
            return Is_connected;
        }
        // 连接主机Button
        private void toolStripButtonConnect_Click(object sender, EventArgs e) {
            //判别当前是断开状态，那么下面进行连接服务器（ARM主控板）
            if (GetConnect_state() != 1){
                //AsynchronousClient client = new AsynchronousClient();
                // client.StartClient(1);
                sock.Connect();//网络连接命令，
                byte[] data = new byte[10];
                data[0] = 0;
                //Send_Data_Client(data, (byte)'v', 1);
            }
            else{
                sock.DisConnect();
                //client.StartClient(0);
            }
        }



        //-------新建文件--------------------------------------------------------------------------
        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e) {
            CreateNewFolderForm createNewFolderForm = new CreateNewFolderForm();
            createNewFolderForm.ShowDialog();
            lblProjectNumber.Text = ShareClass.ProjectNumber;
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件存放路径";
            string strPath; //文件夹完整的路径名
            if (dialog.ShowDialog() == DialogResult.OK) {
                try {
                    strPath = dialog.SelectedPath;
                    FileStream fs = new FileStream(strPath+"\\"+ShareClass.ProjectNumber+".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    //strPath = ofd.FileName.Replace("\\", "\\\\");
                    //MessageBox.Show(dialog.FileName);
                    //string filePath = strPath.Substring(0, strPath.LastIndexOf("\\") + 1);
                    MessageBox.Show(strPath);
                    
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //------------------------------------------------------------------------------------------
    }
}
