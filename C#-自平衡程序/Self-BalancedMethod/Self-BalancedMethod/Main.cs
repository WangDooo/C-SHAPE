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
using System.Windows.Forms.DataVisualization.Charting;

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



        //-------新建文件、保存项目信息到txt--------------------------------------------------------------------------
        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e) {
            CreateNewTxt();
        }

        private void toolStripButtonNew_Click(object sender, EventArgs e) {
            CreateNewTxt();
        }

        void CreateNewTxt() {
            CreateNewFolderForm createNewFolderForm = new CreateNewFolderForm();
            createNewFolderForm.ShowDialog();
            InitProjectInfo();
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件存放路径";
            string strPath; //文件夹完整的路径名
            if (dialog.ShowDialog() == DialogResult.OK) {
                try {
                    strPath = dialog.SelectedPath;
                    string subPath = strPath + "\\" + ShareClass.ProjectNumber + "\\";
                    string txtProjectInfo =  ShareClass.ProjectNumber + "-" + ShareClass.PileNumber + "-项目信息";
                    if (System.IO.Directory.Exists(subPath) == false){
                        System.IO.Directory.CreateDirectory(subPath);
                        CreateTxtProjectInfo(subPath, txtProjectInfo);
                    } else {
                        CreateTxtProjectInfo(subPath, txtProjectInfo); 
                    }
    
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }

            // 创建txt文件，写入相关信息
            void CreateTxtProjectInfo(string subPath, string txtProjectInfo) {
                FileStream fs = new FileStream(subPath+"\\"+txtProjectInfo+".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs); 
                sw.WriteLine("项目编号," + ShareClass.ProjectNumber);//开始写入
                sw.WriteLine("测试时间," + ShareClass.TestYear + "." + ShareClass.TestMonth + "." + ShareClass.TestDay);
                sw.WriteLine("工地名称," + ShareClass.SiteName);
                sw.WriteLine("试桩桩号," + ShareClass.PileNumber);
                sw.WriteLine("桩长," + ShareClass.PileLength);
                sw.WriteLine("桩径," + ShareClass.PileDiameter);
                sw.Flush(); //清空缓冲区
                sw.Close(); //关闭流
                fs.Close();
            }
        }
        //------------------------------------------------------------------------------------------


        //-------初始化项目信息---------------------------------------------------------------------
        private void InitProjectInfo() {
            txtProjectNumber.Text = ShareClass.ProjectNumber;
            txtTestTime.Text = ShareClass.TestYear + "年" + ShareClass.TestMonth + "月" + ShareClass.TestDay + "日";
            txtSiteName.Text = ShareClass.SiteName;
            txtPileNumber.Text = ShareClass.PileNumber;
            txtPileLength.Text = ShareClass.PileLength;
            txtPileDiameter.Text = ShareClass.PileDiameter;
        }
        //------------------------------------------------------------------------------------------

        //--------撤销功能--------------------------------------------------------------------------
        private void toolStripButtonUndo_Click(object sender, EventArgs e) {
            Undo();
        }

        private void 撤销UCtrlZToolStripMenuItem_Click(object sender, EventArgs e) {
            Undo();
        }

        void Undo() {
            SendKeys.Send("^(z)");
        }

        //------------------------------------------------------------------------------------------


        //--------定时器实时读取txt数据，写入datatable，显示成Q-s曲线-------------------------------
        List<double> Q = new List<double>();
        List<double> s = new List<double>();
        List<double> lgt = new List<double>();
        List<double> lgQ = new List<double>();
        private void timer3_Tick(object sender, EventArgs e) {
            Q.Clear(); // 每Tick都清空原来的List，待改进，可直接添加采集到的数据
            s.Clear();
            lgt.Clear();
            lgQ.Clear();
            string testpath = @"C:\Users\123\Desktop\test\testdata.txt";
            DataTable dt = GetTxt(testpath);
            dataGridView1.DataSource = dt;
            for(int i=0; i<dt.Rows.Count; i++) {
                Q.Add(Convert.ToDouble(dt.Rows[i][2]));  // List Q
                s.Add(Convert.ToDouble(dt.Rows[i][3]));  // List s
                if (Convert.ToDouble(dt.Rows[i][1]) <= 1) { // List lgt
                    lgt.Add(0);
                } else {
                    lgt.Add(Math.Log10(Convert.ToDouble(dt.Rows[i][1])));
                }
                if (Convert.ToDouble(dt.Rows[i][2]) <= 1) { // List lgQ
                    lgQ.Add(0);
                } else {
                    lgQ.Add(Math.Log10(Convert.ToDouble(dt.Rows[i][2])));
                }   
            }
            DrawQsLine(Q,s); // 绘制Q-s
            DrawslgtLine(lgt,s); // 绘制s-lgt
            DrawslgQLine(lgQ,s);// 绘制s-lgQ
        }

        // 获取Txt 返回datatable
        public DataTable GetTxt(string pths) {
            StreamReader sr = new StreamReader(pths);
            DataTable dt = new DataTable();
            dt.Columns.Add("序号", typeof(string)); // 添加表头
            dt.Columns.Add("时间t(s)", typeof(string));
            dt.Columns.Add("荷载Q(kN)", typeof(string));
            dt.Columns.Add("位移s(mm)", typeof(string));
            string txt = sr.ReadToEnd().Replace("\r\n", "-"); // 把换行标志替换成"-"
            string[] nodes = txt.Split('-');
            foreach (string node in nodes) { //每行
                string[] strs = node.Split(','); // 每行根据","分割
                DataRow dr = dt.NewRow();
                for(int i = 0; i < strs.Length; i++) {
                    dr[i] = strs[i];
                }
                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }

        private void toolStripButton1_Click(object sender, EventArgs e) {
            
             //MessageBox.Show(Math.Log10(Convert.ToDouble(dt.Rows[i][2])))
            
        }

        // 使用Chart 进行Q-s曲线显示
        void DrawQsLine(List<double> Q , List<double> s) {
            chartQs.Series.Clear(); //清除默认的series
            Series Qs = new Series("Q-s");  
            Qs.ChartType = SeriesChartType.Line;  // 设置chart的类型 Column柱状 Spline曲线
            Qs.IsValueShownAsLabel = true; // 是否把值当做标签展示（默认false）
            Qs.Color = Color.Red; // series的颜色
            Qs.BorderWidth = 1; //线条粗细
            Qs.MarkerBorderColor = Color.Red; //标记点边框颜色
            Qs.MarkerBorderWidth = 3; //标记点边框大小
            Qs.MarkerColor = Color.Red; //标记点中心颜色
            Qs.MarkerSize = 3; //标记点大小
            Qs.MarkerStyle = MarkerStyle.Circle; //标记点类型
            chartQs.Titles.Clear(); // 设置标题
            chartQs.Titles.Add("Q-s曲线");
            chartQs.Titles[0].ForeColor = Color.RoyalBlue;
            chartQs.Titles[0].Font = new Font("Microsoft Sans Serif", 16F); 
            chartQs.ChartAreas[0].AxisX.MajorGrid.Interval = 0.5; // 设置网格间隔
            chartQs.ChartAreas[0].AxisX.MajorGrid.Enabled = false; // 显示网格 X
            chartQs.ChartAreas[0].AxisY.MajorGrid.Enabled = false; // 显示网格 Y
            chartQs.ChartAreas[0].Axes[0].MajorTickMark.Enabled = true; // x轴上突出的小点
            chartQs.ChartAreas[0].Axes[1].MajorTickMark.Enabled = true; // y轴上突出的小点
            chartQs.ChartAreas[0].AxisX.IsMarginVisible = false; // X是否过原点 false过
            chartQs.ChartAreas[0].AxisY.IsMarginVisible = false; // Y是否过原点 false过
            chartQs.ChartAreas[0].BackColor = System.Drawing.Color.Transparent; //设置区域内背景透明

            chartQs.ChartAreas[0].AxisX.Title = "荷载Q(kN)";
            chartQs.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
            chartQs.ChartAreas[0].AxisY.Title = "位移s(mm)";
            chartQs.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
            // chartQs.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal; // Y轴字体水平
            chartQs.ChartAreas[0].AxisY.IsReversed = true; // Y轴反向

            chartQs.ChartAreas[0].AxisX.Minimum = 0;  // 设置显示范围
            //chartQs.ChartAreas[0].AxisX.Maximum = 10;
            chartQs.ChartAreas[0].AxisY.Minimum = 0;
            chartQs.ChartAreas[0].AxisY.Maximum = Math.Ceiling(Convert.ToDouble(s[s.Count-1])); // 向上取整

            Qs.ToolTip = "Q：#VALX \n s：#VALY"; // #VALX #VALY 鼠标悬停显示

            for(int i = 0; i < Q.Count; i++) {
                 Qs.Points.AddXY(Q[i], s[i]); //给系列上的点进行赋值，分别对应横坐标和纵坐标的值
            } 

            chartQs.Series.Add(Qs);  //把series添加到chart上
        }

        // 使用Chart 进行s-lgt曲线显示
        void DrawslgtLine(List<double> lgt , List<double> s) { 
            chartslgt.Series.Clear(); //清除默认的series
            Series slgt = new Series("s-lgt");  
            slgt.ChartType = SeriesChartType.Line;
            slgt.IsValueShownAsLabel = true; // 是否把值当做标签展示（默认false）
            slgt.Color = Color.Red; // series的颜色
            slgt.BorderWidth = 1; //线条粗细
            slgt.MarkerBorderColor = Color.Red; //标记点边框颜色
            slgt.MarkerBorderWidth = 3; //标记点边框大小
            slgt.MarkerColor = Color.Red; //标记点中心颜色
            slgt.MarkerSize = 3; //标记点大小
            slgt.MarkerStyle = MarkerStyle.Circle; //标记点类型
            chartslgt.Titles.Clear(); // 设置标题
            chartslgt.Titles.Add("s-lgt曲线");
            chartslgt.Titles[0].ForeColor = Color.RoyalBlue;
            chartslgt.Titles[0].Font = new Font("Microsoft Sans Serif", 16F); 
            chartslgt.ChartAreas[0].AxisX.MajorGrid.Interval = 0.5; // 设置网格间隔
            chartslgt.ChartAreas[0].AxisX.MajorGrid.Enabled = false; // 显示网格 X
            chartslgt.ChartAreas[0].AxisY.MajorGrid.Enabled = false; // 显示网格 Y
            chartslgt.ChartAreas[0].Axes[0].MajorTickMark.Enabled = true; // x轴上突出的小点
            chartslgt.ChartAreas[0].Axes[1].MajorTickMark.Enabled = true; // y轴上突出的小点
            chartslgt.ChartAreas[0].AxisX.IsMarginVisible = false; // X是否过原点 false过
            chartslgt.ChartAreas[0].AxisY.IsMarginVisible = false; // Y是否过原点 false过
            chartslgt.ChartAreas[0].BackColor = System.Drawing.Color.Transparent; //设置区域内背景透明

            chartslgt.ChartAreas[0].AxisX.Title = "lgt";
            chartslgt.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
            chartslgt.ChartAreas[0].AxisY.Title = "位移s(mm)";
            chartslgt.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
            chartslgt.ChartAreas[0].AxisY.IsReversed = true; // Y轴反向

            chartslgt.ChartAreas[0].AxisX.Minimum = 0;  // 设置显示范围
            chartslgt.ChartAreas[0].AxisX.Maximum = Math.Ceiling(Convert.ToDouble(lgt[lgt.Count-1]));
            chartslgt.ChartAreas[0].AxisY.Minimum = 0;
            chartslgt.ChartAreas[0].AxisY.Maximum = Math.Ceiling(Convert.ToDouble(s[s.Count-1]));

            slgt.ToolTip = "lgt：#VALX \n s：#VALY"; // #VALX #VALY 鼠标悬停显示

            for(int i = 0; i < s.Count; i++) {
                slgt.Points.AddXY(lgt[i], s[i]); //给系列上的点进行赋值，分别对应横坐标和纵坐标的值
            }

            chartslgt.Series.Add(slgt);
        }

        // 使用Chart 进行s-lgQ曲线显示
        void DrawslgQLine(List<double> lgQ , List<double> s) { 
            chartslgQ.Series.Clear(); //清除默认的series
            Series slgQ = new Series("s-lgQ");  
            slgQ.ChartType = SeriesChartType.Line;
            slgQ.IsValueShownAsLabel = true; // 是否把值当做标签展示（默认false）
            slgQ.Color = Color.Red; // series的颜色
            slgQ.BorderWidth = 1; //线条粗细
            slgQ.MarkerBorderColor = Color.Red; //标记点边框颜色
            slgQ.MarkerBorderWidth = 3; //标记点边框大小
            slgQ.MarkerColor = Color.Red; //标记点中心颜色
            slgQ.MarkerSize = 3; //标记点大小
            slgQ.MarkerStyle = MarkerStyle.Circle; //标记点类型
            chartslgQ.Titles.Clear(); // 设置标题
            chartslgQ.Titles.Add("s-lgQ曲线");
            chartslgQ.Titles[0].ForeColor = Color.RoyalBlue;
            chartslgQ.Titles[0].Font = new Font("Microsoft Sans Serif", 16F); 
            chartslgQ.ChartAreas[0].AxisX.MajorGrid.Interval = 0.5; // 设置网格间隔
            chartslgQ.ChartAreas[0].AxisX.MajorGrid.Enabled = false; // 显示网格 X
            chartslgQ.ChartAreas[0].AxisY.MajorGrid.Enabled = false; // 显示网格 Y
            chartslgQ.ChartAreas[0].Axes[0].MajorTickMark.Enabled = true; // x轴上突出的小点
            chartslgQ.ChartAreas[0].Axes[1].MajorTickMark.Enabled = true; // y轴上突出的小点
            chartslgQ.ChartAreas[0].AxisX.IsMarginVisible = false; // X是否过原点 false过
            chartslgQ.ChartAreas[0].AxisY.IsMarginVisible = false; // Y是否过原点 false过
            chartslgQ.ChartAreas[0].BackColor = System.Drawing.Color.Transparent; //设置区域内背景透明

            chartslgQ.ChartAreas[0].AxisX.Title = "lgQ";
            chartslgQ.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
            chartslgQ.ChartAreas[0].AxisY.Title = "位移s(mm)";
            chartslgQ.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
            chartslgQ.ChartAreas[0].AxisY.IsReversed = true; // Y轴反向

            chartslgQ.ChartAreas[0].AxisX.Minimum = 0;  // 设置显示范围
            chartslgQ.ChartAreas[0].AxisX.Maximum = Math.Ceiling(Convert.ToDouble(lgQ[lgQ.Count-1]));
            chartslgQ.ChartAreas[0].AxisY.Minimum = 0;
            chartslgQ.ChartAreas[0].AxisY.Maximum = Math.Ceiling(Convert.ToDouble(s[s.Count-1]));

            slgQ.ToolTip = "lgQ：#VALX \n s：#VALY"; // #VALX #VALY 鼠标悬停显示

            for(int i = 0; i < s.Count; i++) {
                slgQ.Points.AddXY(lgQ[i], s[i]); //给系列上的点进行赋值，分别对应横坐标和纵坐标的值
            }

            chartslgQ.Series.Add(slgQ);
        }
        //------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------


        //------------------------------------------------------------------------------------------


        //------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------


    }
}
