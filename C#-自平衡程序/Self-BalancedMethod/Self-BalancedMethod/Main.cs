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
        List<double> Qzph = new List<double>();
        List<double> szph = new List<double>();
        // 每1s读取一次txt中的数据，进行图像绘制
        private void timer3_Tick(object sender, EventArgs e) {
            Q.Clear(); // 每Tick都清空原来的List，待改进，可直接添加采集到的数据
            s.Clear();
            lgt.Clear();
            lgQ.Clear();
            string testpath = @"C:\Users\123\Desktop\test\testdata.txt";
            DataTable dt = GetTxt(testpath); // 将TxT内容写入datatable
            dataGridView1.DataSource = dt;
            for(int i=0; i<dt.Rows.Count; i++) {
                Q.Add(Convert.ToDouble(dt.Rows[i][2]));  // List Q
                s.Add(Convert.ToDouble(dt.Rows[i][3]));  // List s
                if (Convert.ToDouble(dt.Rows[i][1]) <= 1) { // List lgt
                    lgt.Add(0);
                } else {
                    lgt.Add(Math.Round(Math.Log10(Convert.ToDouble(dt.Rows[i][1])),2)); // 保留两位小数
                }
                if (Convert.ToDouble(dt.Rows[i][2]) <= 1) { // List lgQ
                    lgQ.Add(0);
                } else {
                    lgQ.Add(Math.Round(Math.Log10(Convert.ToDouble(dt.Rows[i][2])),2)); // 保留两位小数
                }   
            }
            DrawQsLine(Q,s); // 绘制Q-s
            DrawslgtLine(lgt,s); // 绘制s-lgt
            DrawslgQLine(lgQ,s);// 绘制s-lgQ
            
            if(ShareClass.ParmBool == true) {
                Qzph.Clear();
                szph.Clear();
                for(int i=0; i<dt.Rows.Count; i++) {
                    double QuWGamma1 = (Convert.ToDouble(dt.Rows[i][4])-ShareClass.ParmW)/ShareClass.ParmGamma1;
                    double QzphValue = QuWGamma1 + Convert.ToDouble(dt.Rows[i][5]);
                    double detals = (QuWGamma1 + 2*Convert.ToDouble(dt.Rows[i][5]))*ShareClass.ParmLu/(2*ShareClass.ParmEp*ShareClass.ParmAp);
                    double szqhValue = Convert.ToDouble(dt.Rows[i][6]) + detals;
                    Qzph.Add(Math.Round(QzphValue,2));
                    szph.Add(Math.Round(szqhValue,2));
                }
                DrawQszphLine(Qzph,szph);
            }
        }

        // 获取Txt 返回datatable
        public DataTable GetTxt(string pths) {
            StreamReader sr = new StreamReader(pths);
            DataTable dt = new DataTable();
            dt.Columns.Add("序号", typeof(string)); // 添加表头
            dt.Columns.Add("时间t(s)", typeof(string));
            dt.Columns.Add("荷载Q(kN)", typeof(string));
            dt.Columns.Add("位移s(mm)", typeof(string));
            dt.Columns.Add("Qu(kN)", typeof(string));
            dt.Columns.Add("Qd(kN)", typeof(string));
            dt.Columns.Add("sd(mm)", typeof(string));
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

        // 使用Chart 进行Q-s等效曲线显示
        void DrawQszphLine(List<double> Qzph , List<double> szph) { 
            chartQszph.Series.Clear(); //清除默认的series
            Series Qszph = new Series("Q-s等效曲线");  
            Qszph.ChartType = SeriesChartType.Line;
            Qszph.IsValueShownAsLabel = true; // 是否把值当做标签展示（默认false）
            Qszph.Color = Color.Red; // series的颜色
            Qszph.BorderWidth = 1; //线条粗细
            Qszph.MarkerBorderColor = Color.Red; //标记点边框颜色
            Qszph.MarkerBorderWidth = 3; //标记点边框大小
            Qszph.MarkerColor = Color.Red; //标记点中心颜色
            Qszph.MarkerSize = 3; //标记点大小
            Qszph.MarkerStyle = MarkerStyle.Circle; //标记点类型
            chartQszph.Titles.Clear(); // 设置标题
            chartQszph.Titles.Add("Q-s等效曲线 ");
            chartQszph.Titles[0].ForeColor = Color.RoyalBlue;
            chartQszph.Titles[0].Font = new Font("Microsoft Sans Serif", 16F); 
            chartQszph.ChartAreas[0].AxisX.MajorGrid.Interval = 0.5; // 设置网格间隔
            chartQszph.ChartAreas[0].AxisX.MajorGrid.Enabled = false; // 显示网格 X
            chartQszph.ChartAreas[0].AxisY.MajorGrid.Enabled = false; // 显示网格 Y
            chartQszph.ChartAreas[0].Axes[0].MajorTickMark.Enabled = true; // x轴上突出的小点
            chartQszph.ChartAreas[0].Axes[1].MajorTickMark.Enabled = true; // y轴上突出的小点
            chartQszph.ChartAreas[0].AxisX.IsMarginVisible = false; // X是否过原点 false过
            chartQszph.ChartAreas[0].AxisY.IsMarginVisible = false; // Y是否过原点 false过
            chartQszph.ChartAreas[0].BackColor = System.Drawing.Color.Transparent; //设置区域内背景透明

            chartQszph.ChartAreas[0].AxisX.Title = "等效荷载Q(kN)";
            chartQszph.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
            chartQszph.ChartAreas[0].AxisY.Title = "等效位移s(mm)";
            chartQszph.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
            chartQszph.ChartAreas[0].AxisY.IsReversed = true; // Y轴反向

            chartQszph.ChartAreas[0].AxisX.Minimum = 0;  // 设置显示范围
            //chartQszph.ChartAreas[0].AxisX.Maximum = 10;
            chartQszph.ChartAreas[0].AxisY.Minimum = 0;
            chartQszph.ChartAreas[0].AxisY.Maximum = Math.Ceiling(Convert.ToDouble(szph[szph.Count-1]));

            Qszph.ToolTip = "Q：#VALX \n s：#VALY"; // #VALX #VALY 鼠标悬停显示

            for(int i = 0; i < szph.Count; i++) {
                Qszph.Points.AddXY(Qzph[i], szph[i]); //给系列上的点进行赋值，分别对应横坐标和纵坐标的值
            }

            chartQszph.Series.Add(Qszph);
        }

        // chartQs 获取图中点的坐标
        private void chartQs_MouseMove(object sender, MouseEventArgs e) {
            HitTestResult myTestResult=  chartQs.HitTest(e.X,e.Y);
            if (myTestResult.ChartElementType == ChartElementType.DataPoint)  {  
                this.Cursor = Cursors.Cross;  
                int i = myTestResult.PointIndex;  
                DataPoint dp = myTestResult.Series.Points[i];           
                double doubleXValue= (dp.XValue);  
                double doubleYValue = dp.YValues[0]; 
                toolStripStatusLabelCoordinate.Text = " Q : "+doubleXValue.ToString()+"  s : "+doubleYValue.ToString();           
            } else {  
               this.Cursor = Cursors.Default;  
            }
        }

        // chartsglt 获取图中点的坐标
        private void chartslgt_MouseMove(object sender, MouseEventArgs e) {
            HitTestResult myTestResult=  chartslgt.HitTest(e.X,e.Y);
            if (myTestResult.ChartElementType == ChartElementType.DataPoint)  {  
                this.Cursor = Cursors.Cross;  
                int i = myTestResult.PointIndex;  
                DataPoint dp = myTestResult.Series.Points[i];           
                double doubleXValue= (dp.XValue);  
                double doubleYValue = dp.YValues[0]; 
                toolStripStatusLabelCoordinate.Text = " lgt : "+doubleXValue.ToString()+"  s : "+doubleYValue.ToString();           
            } else {  
               this.Cursor = Cursors.Default;  
            }
        }

        // chartslgQ 获取图中点的坐标
        private void chartslgQ_MouseMove(object sender, MouseEventArgs e) {
            HitTestResult myTestResult=  chartslgQ.HitTest(e.X,e.Y);
            if (myTestResult.ChartElementType == ChartElementType.DataPoint)  {  
                this.Cursor = Cursors.Cross;  
                int i = myTestResult.PointIndex;  
                DataPoint dp = myTestResult.Series.Points[i];           
                double doubleXValue= (dp.XValue);  
                double doubleYValue = dp.YValues[0]; 
                toolStripStatusLabelCoordinate.Text = " lgQ : "+doubleXValue.ToString()+"  s : "+doubleYValue.ToString();           
            } else {  
               this.Cursor = Cursors.Default;  
            }
        }

        // chartQszph 获取图中点的坐标
        private void chartQszph_MouseMove(object sender, MouseEventArgs e) {
            HitTestResult myTestResult=  chartQszph.HitTest(e.X,e.Y);
            if (myTestResult.ChartElementType == ChartElementType.DataPoint)  {  
                this.Cursor = Cursors.Cross;  
                int i = myTestResult.PointIndex;  
                DataPoint dp = myTestResult.Series.Points[i];           
                double doubleXValue= (dp.XValue);  
                double doubleYValue = dp.YValues[0]; 
                toolStripStatusLabelCoordinate.Text = " Q : "+doubleXValue.ToString()+"  s : "+doubleYValue.ToString();           
            } else {  
               this.Cursor = Cursors.Default;  
            }
        }
        
        //------------------------------------------------------------------------------------------

        //------Qs等效曲线判断是否已经输入相关参数--------------------------------------------------
        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e) {
            if((tabControl2.SelectedIndex == 4)&&(ShareClass.ParmBool==false)){
                MessageBox.Show("请先设置等效参数");
                EquivalentParametersForm equivalentParametersForm = new EquivalentParametersForm();
                equivalentParametersForm.ShowDialog();
            }
        }
        //------------------------------------------------------------------------------------------

        //--------软件升级 链接至百度云盘--------------------------------------------------------------------------
        private void 升级测试软件UToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show("提取码 : hvaz ");
            System.Diagnostics.Process.Start("https://pan.baidu.com/s/15H-Lf4Xn8-bD-nLhSJGJ6w");
        }
        //------------------------------------------------------------------------------------------

        //--------每十分钟备份一次TxT----------------------------------------------------------------------------------
        private void timerBackupTxT_Tick(object sender, EventArgs e) {

        }

        // 备份txt
        void BackupTxt(){
            String dir = "C:\\Users\\123\\Desktop\\test\\"; 
            String bakDir = dir + "backup\\";  // 创建备份文件夹，
            if (Directory.Exists(bakDir) == false){
                Directory.CreateDirectory(bakDir);
            }
            string[] files = Directory.GetFiles(dir);
            if (files.Length != 0) {
                foreach (string file in files) {
                    FileInfo fileinfo = new FileInfo(file);
                    try{
                        string fileName =  DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")+".txt"; // 按时间命名
                        File.Copy(file,Path.Combine(bakDir,fileName)); //备份文件
                    } catch (Exception ex) {
                        MessageBox.Show(ex.Message);
                    } 
                }
            }
        }
        //------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------

        //--------测试专用--------------------------------------------------------------------------
        private void toolStripButton1_Click(object sender, EventArgs e) {
            string testpath = @"C:\Users\123\Desktop\test\testdata.txt";
            DataTable dt = GetTxt(testpath);
            // TestWriteTxt(testpath,dt); 
            BackupTxt();

            // 用于模拟数据采集
            void TestWriteTxt(string path, DataTable testdt){ 
                FileStream fs = new FileStream(path,  FileMode.Append, FileAccess.Write, FileShare.Read);
                StreamWriter sw = new StreamWriter(fs);
                string strtemp = "\r\n";
                for (int i = 0; i < 7; i++) { // 这个7是暂时设定的
                    double temp = Convert.ToDouble(testdt.Rows[testdt.Rows.Count - 1][i]);
                    temp = temp * 1.05;
                    if (i != 6) { // 这个6是暂时设定的
                        strtemp = strtemp + temp.ToString("f2") + ",";
                    } else {
                        strtemp = strtemp + temp.ToString("f2");
                    }
                }
                sw.Write(strtemp);
                sw.Flush(); //清空缓冲区
                sw.Close(); //关闭流
                fs.Close();
            }
        }
        //------------------------------------------------------------------------------------------

    }
}
