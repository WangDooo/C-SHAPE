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
using System.Runtime.InteropServices;

namespace Self_BalancedMethod {
    public partial class Main : Form {
        //--------结构体定义------------------------------------------------------------------------
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1), Serializable]
        public struct Modbus_memory//结构体定义法，严重不同于c语言
        {
            public ushort slave_addr;
            public ushort slave_baud;
            public ushort slave_rtu;//用来存是否RTU
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public long[] range;// = new long[12];//先下限，后上限
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public float[] i_data;//= new float[12];
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public float[] pa_data;//= new float[12];


        }
        public static Modbus_memory modbus_mem = new Modbus_memory();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1), Serializable]
        public struct slave_memory{  
            public ushort start;
	        public ushort addr;//
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public float[] range_low;			//
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public float[] range_top;			//
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public float[] range_zero;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public float[] ch;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]//
            public float[] I;	//
            public byte relay_status;	//
            public byte empty;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] UID;		//
            public byte YYYY;
            public byte MM;
            public byte DD;
            public byte hh;
            public byte mm;
            public byte ss;
            public byte end;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1), Serializable]
        public struct slave_memory_all{
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] head;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public slave_memory[] slave_data;
        }
        public static slave_memory_all slave_mem = new slave_memory_all();
        //------------------------------------------------------------------------------------------

        //-------初始定义的一些变量-----------------------------------------------------------------
        public static uint Is_connected = 0; // 网络连接状态
        public static float[] CH_RANGE = new float[12]; // 量程
        public static float[] CH_RANGE_BIG = new float[12]; // 量程（大端）
        public byte[] Realy_status = new byte[8]; // 继电器状态
        mysocket sock = new mysocket();
        public static Main mfs;
        string msg_not_connect = "未与主机建立连接";
        
        //------------------------------------------------------------------------------------------

        //--------初始化界面Load--------------------------------------------------------------
        private void Main_Load(object sender, EventArgs e) {
            // Timer的初始设置
            timerNetdata.Interval = 10;
            timerSystemTime.Interval = 1000;
            timerDrawLine.Interval = 10000;
            timerBackupTxT.Interval = 600000; // 10min
            //界面listview外观设置
            listView_ch.CheckBoxes = false;
            listView_ch.Columns.Add("通道", 60, HorizontalAlignment.Left);
            listView_ch.Columns.Add("量程", 80, HorizontalAlignment.Right);
            listView_ch.Columns.Add("电流", 80, HorizontalAlignment.Right);
            listView_ch.Columns.Add("位移", 80, HorizontalAlignment.Right);
            listView_ch.GridLines = true;
            listView_ch.BeginUpdate();
            for (int i = 0; i < 4; i++){
                CH_RANGE[i] = 0; 
                CH_RANGE_BIG[i] = 100;
                ListViewItem vi = new ListViewItem();
                vi.ImageIndex = i + 1;
                vi.Text = "CH" + Convert.ToString(i + 1);
                vi.SubItems.Add(String.Format("{0}~{1} mm", CH_RANGE[i], CH_RANGE_BIG[i]));
                vi.SubItems.Add(i + " mA");
                vi.SubItems.Add(0 + " mm");
                listView_ch.Items.Add(vi);
            }
            listView_ch.EndUpdate();
            listView_ch.FullRowSelect = true;
            //结构体需要分配空间，然后初始化一些数据
            modbus_mem.range = new long[12];
            modbus_mem.i_data = new float[12];
            modbus_mem.pa_data = new float[12];
            for (int i = 0; i < 4; i++){
                modbus_mem.range[i] = 0x0000001000003020;
                modbus_mem.i_data[i] = (float)(10.0 * (i + 1));
                modbus_mem.pa_data[i] = (float)(20.0 * (i + 1));
            }
            for (int i = 0; i < 4; i++){
                CH_RANGE[i] = (int)((modbus_mem.range[i] >> 32) & 0xffffffff);
                CH_RANGE_BIG[i] = (int)((modbus_mem.range[i] >> 0) & 0xffffffff);
                listView_ch.Items[i].SubItems[1].Text = String.Format("{0}~{1} mm", CH_RANGE[i], CH_RANGE_BIG[i]);
                listView_ch.Items[i].SubItems[2].Text = Convert.ToString(modbus_mem.i_data[i]) + " mA";
                listView_ch.Items[i].SubItems[3].Text = Convert.ToString(modbus_mem.pa_data[i]) + " mm";
            }
            // 判断testpath是否存在
            if (File.Exists(testpath) == false){
                MessageBox.Show("未找到测试文件，请选择测试文件位置");
                OpenFileDialog fileName = new OpenFileDialog(); //创建一个对话框
                if(fileName.ShowDialog() == DialogResult.OK){   
                    testpath = fileName.FileName.ToString();
                }
            }
        }
        //------------------------------------------------------------------------------------------

        public Main() { //界面启动后第一个执行的。
            InitializeComponent();
            mfs = this;
            Control.CheckForIllegalCrossThreadCalls = false; // 允许跨线程调用此类控件
        }
        
        // 按字节排列的数据搬运给结构体
        public static object BytesToStruct(byte[] bytes, Type strcutType)
        {
            int size =  Marshal.SizeOf(strcutType);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(bytes, 0, buffer, size);
                return Marshal.PtrToStructure(buffer, strcutType);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }    
        //习惯于c语言中的地址搬运函数
        public void memcpy_byte(byte[] dec,int off,byte[] src,int offset,int len){
            for (int i = 0; i < len; i++)
                dec[off+i] = src[offset + i];
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
        // 10ms定时，主要用来网络定时周期性数据读取
        private void timer2_Tick(object sender, EventArgs e) {  
            if (Is_connected == 1){
                sock.ReadData(); // 定时的读网口操作
                // GC.Collect(); 这个没用，内存还是涨
                SetConnect_text("已连接");
                toolStripButtonConnect.Text=("断开");
            }
            else{
                SetConnect_text("已断开");
                toolStripButtonConnect.Text=("连接");
            }
        }
        // 设置tcp连接状态字符串
        public void SetConnect_text(String str){ 
            toolStripStatusLabelConnect.Text = str;
        }
        // 读取连接状态
        public uint GetConnect_state(){
            return Is_connected;
        }
        // 连接主机Button
        private void toolStripButtonConnect_Click(object sender, EventArgs e) {
            //判别当前是断开状态，那么下面进行连接服务器（ARM主控板）
            if (GetConnect_state() != 1){
                sock.Connect();//网络连接命令，
            }
            else{
                sock.DisConnect();
            }
        }


        # region 新建文件、保存项目信息到txt
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
        # endregion
        
        # region 初始化项目信息
        private void InitProjectInfo() {
            txtProjectNumber.Text = ShareClass.ProjectNumber;
            txtTestTime.Text = ShareClass.TestYear + "年" + ShareClass.TestMonth + "月" + ShareClass.TestDay + "日";
            txtSiteName.Text = ShareClass.SiteName;
            txtPileNumber.Text = ShareClass.PileNumber;
            txtPileLength.Text = ShareClass.PileLength;
            txtPileDiameter.Text = ShareClass.PileDiameter;
        }
        # endregion

        # region 简易实现撤销功能
        private void toolStripButtonUndo_Click(object sender, EventArgs e) {
            Undo();
        }

        private void 撤销UCtrlZToolStripMenuItem_Click(object sender, EventArgs e) {
            Undo();
        }

        void Undo() {
            SendKeys.Send("^(z)");
        }
        # endregion

        # region 定时器实时读取txt数据，写入datatable，显示成Q-s曲线
        List<double> Q = new List<double>();
        List<double> s = new List<double>();
        List<double> lgt = new List<double>();
        List<double> lgQ = new List<double>();
        List<double> Qzph = new List<double>();
        List<double> szph = new List<double>();
        string testpath = @"C:\Users\123\Desktop\test\testdata.txt";
        // 每1s读取一次txt中的数据，进行图像绘制
        private void timer3_Tick(object sender, EventArgs e) {
            Q.Clear(); // 每Tick都清空原来的List，待改进，可直接添加采集到的数据
            s.Clear();
            lgt.Clear();
            lgQ.Clear();
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
        
        # endregion

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

        //------------------------------------------------------------------------------------------

        //--------测试专用--------------------------------------------------------------------------
        private void toolStripButton1_Click(object sender, EventArgs e) {
            string testpath = @"C:\Users\123\Desktop\test\testdata.txt";
            DataTable dt = GetTxt(testpath);
            // TestWriteTxt(testpath,dt);  // 模拟数据采集
            // BackupTxt(); // 备份文件

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

        # region 所有接收到的数据都在这里处理，按命令处理，参考通讯协议文档
        //-------注意：这里都是接收到的数据，不是发送命令的地方-------------------------------------
        public void process_receive(byte[] data, int len)
        {
            //uint id = 0;
            // uint total, index;
            if (data[5] == 't') { // 同步时间，使得下位机的时间和电脑同步
                if (data[6] == 1){
                    //把接收到的数据发送到界面的最下面那个文本框内，net_msg_text
                    string str="";
                    for (int z = 0; z < data.Length; z++) {
                        str += data[z].ToString("X2"); // 转化为大写的16进制
                    }
                    net_msg_text.Text = str;
                    MessageBox.Show("时间同步已完成！");
                }
            }
            else if (data[5] == 'r') { // 读取量程的返回数据，
                string str = "";
                for (int z = 0; z < data.Length; z++) {
                    str += data[z].ToString("X2");
                } 
                net_msg_text.Text = str;
                for(int i = 0; i < 4; i++){
                    CH_RANGE[i] = BitConverter.ToSingle(data, 6+4*i); // 量程的小端
                    CH_RANGE_BIG[i] = BitConverter.ToSingle(data, 6 + 4 * i+32); // 量程的大端
                    listView_ch.Items[i].SubItems[1].Text = String.Format("{0}~{1} mm", CH_RANGE[i], CH_RANGE_BIG[i]); // 显示在界面的主机动态数据
                }
            }
            else if (data[5] == 'R') { // 设置量程 返回包
                string str = "";
                for (int z = 0; z < data.Length; z++) {
                    str += data[z].ToString("X2");
                }  
                net_msg_text.Text = str;
            }
            else if (data[5] == 'h') { // 设置远端机是否在线的返回包
                string str = "";
                for (int z = 0; z < data.Length; z++) {
                    str += data[z].ToString("X2");
                }
                net_msg_text.Text = str;     
            }
            else if (data[5] == 'w') { // 设置有线传输还是无线433的返回包
                string str = "";
                for (int z = 0; z < data.Length; z++) {
                    str += data[z].ToString("X2");
                }  
                net_msg_text.Text = str;
            }
            else if (data[5] == 'a') { // 下载4~20mA输入的a系数的返回包
                string str = "";
                for (int z = 0; z < data.Length; z++) {
                    str += data[z].ToString("X2");
                } 
                net_msg_text.Text = str;
            }
            else if (data[5] == 'k') { // 下载4~20mA输入的k系数的返回包
                string str = "";
                for (int z = 0; z < data.Length; z++) {
                    str += data[z].ToString("X2");
                }
                net_msg_text.Text = str;
            }
            else if (data[5] == 'p') { // 设置报警上下限阈值的返回包
                string str = "";
                for (int z = 0; z < data.Length; z++) {
                    str += data[z].ToString("X2");
                }   
                net_msg_text.Text = str;
            }
            else if (data[5] == 'g') { // 发送4G数据的返回包
                string str = "";
                for (int z = 0; z < data.Length; z++) {
                    str += data[z].ToString("X2");
                }      
                net_msg_text.Text = str;
            }
            else if (data[5] == 'e') { // 设置继电器动作命令的返回包
                string str = "";
                for (int z = 0; z < data.Length; z++) {
                    str += data[z].ToString("X2");
                }   
                net_msg_text.Text = str;
            }
            else if (data[5] == 'D') { //收到3个远端机的数据了
                slave_mem = (slave_memory_all)BytesToStruct(data, slave_mem.GetType()); // 把远端机的数据放到结构体中
                string str = "";
                for (int z = 0; z < data.Length; z++) {
                    str += data[z].ToString("X2");
                }    
                net_msg_text.Text = str;
                for (int i = 0; i < 8; i++) { // 8个通道的远端数据显示在界面上
                        listView_slave.Items[i].SubItems[1].Text = slave_mem.slave_data[0].ch[i].ToString("F5") + " mm";
                        listView_slave.Items[i].SubItems[2].Text = slave_mem.slave_data[1].ch[i].ToString("F5") + " mm";
                        listView_slave.Items[i].SubItems[3].Text = slave_mem.slave_data[2].ch[i].ToString("F5") + " mm";
                }
            }
            else if (data[5] == 'd') { // 获取到实时电流数据
                string str = "";
                for (int z = 0; z < data.Length; z++) {
                    str += data[z].ToString("X2");
                }
                net_msg_text.Text = str;
                float aaa;
                for (int i = 0; i < 4; i++) { // 4个通道的电流数据显示
                    aaa = BitConverter.ToSingle(data, 6+34+4*i); // 这些数字都是要根据ARM发过来的结构体中的所在位置算地址便宜的。即主机动态数据所在结构体的位置偏移。
                    modbus_mem.i_data[i] = aaa;
                    listView_ch.Items[i].SubItems[2].Text = aaa.ToString("F5") + " mA";
                    aaa = BitConverter.ToSingle(data, 6 + 2 + 4 * i);
                    listView_ch.Items[i].SubItems[3].Text = aaa.ToString("F5") + " mm";      
                }
                str = "GPS:  ";
                UInt32 latitude = BitConverter.ToUInt32(data,222+6);//纬度
                byte[] nshemi=new byte[2] ;
                nshemi[0]= data[222 + 4 + 6];//北纬南纬
                UInt32 longitude = BitConverter.ToUInt32(data, 222+5+6);//经度
                byte[] ewhemi=new byte[2];
                ewhemi[0]= data[222 + 9 + 6];//东经西经
                float lat = (float)latitude / 100000;//小数点后面的进制为60进位，未转化为分秒。
                float lon = (float)longitude / 100000;
                string str0 = Encoding.ASCII.GetString(nshemi).Substring(0, 1);
                string str1 = ":";
                string str2 = lat.ToString();
                string str3 = Encoding.ASCII.GetString(ewhemi).Substring(0, 1);
                string str4 = ":";
                string str5 = lon.ToString();
                str += str0 + str1+str2+"   "+str3+str4+str5;
                gps_text.Text = str;
                for (int j = 0; j < 8; j++) { // 8个继电器的状态显示，
                    Realy_status[j] = data[j+6+66];
                    if (j == 0){
                        if (Realy_status[j] == 0)
                            btn1.Text = "OFF";
                        else
                            btn1.Text = "ON";
                    }
                    else if (j == 1){
                        if (Realy_status[j] == 0)
                            btn2.Text = "OFF";
                        else
                            btn2.Text = "ON";
                    }
                    else if (j == 2){
                        if (Realy_status[j] == 0)
                            btn3.Text = "OFF";
                        else
                            btn3.Text = "ON";
                    }
                    else if (j == 3){
                        if (Realy_status[j] == 0)
                            btn4.Text = "OFF";
                        else
                            btn4.Text = "ON";
                    }
                    else if (j == 4){
                        if (Realy_status[j] == 0)
                            btn5.Text = "OFF";
                        else
                            btn5.Text = "ON";
                    }
                    else if (j == 5){
                        if (Realy_status[j] == 0)
                            btn6.Text = "OFF";
                        else
                            btn6.Text = "ON";
                    }
                    else if (j == 6){
                        if (Realy_status[j] == 0)
                            btn7.Text = "OFF";
                        else
                            btn7.Text = "ON";
                    }
                    else if (j == 7){
                        if (Realy_status[j] == 0)
                            btn8.Text = "OFF";
                        else
                            btn8.Text = "ON";
                    }
                }
            }
            else if (data[5] == 'i') { // 设置4~20mA输出大小的返回包
                string str = "";
                for (int z = 0; z < data.Length; z++) {
                    str += data[z].ToString("X2");
                }  
                net_msg_text.Text = str;           
            }
            else if (data[5] == 'j') { // 下载4~20mA输出的标定a和k系数的返回包
                string str = "";
                for (int z = 0; z < data.Length; z++) {
                    str += data[z].ToString("X2");
                } 
                net_msg_text.Text = str;
            }
            else if (data[5] == 'y') { // 需要发送到远端机的数据的返回包
                string str = "";
                for (int z = 0; z < data.Length; z++) {
                    str += data[z].ToString("X2");
                }
                net_msg_text.Text = str;
                if (data[6] == 0x01){
                    if (data[7] == 0) 
                        checkBox1.Checked = false;
                    else
                        checkBox1.Checked = true;
                    if (data[8] == 0)
                        checkBox2.Checked = false;
                    else
                        checkBox2.Checked = true;
                    if (data[9] == 0)
                        checkBox3.Checked = false;
                    else
                        checkBox3.Checked = true;
                }
            }
        }
        //------------------------------------------------------------------------------------------
        # endregion

        
        //-------发送命令函数 和 crc值处理-----------------------------------------------------------
        //最主要的发送命令函数，data是数据区，type是类型，total是总共有几个包需要发送，index是当前是第几个包，len是发送长度
        public uint Get_Para_Client(byte[] data, byte type, uint total, uint index, int len)
        {
            uint length;
            // UInt16 l = 0;
            length = (uint)len;
            unsafe{
                byte[] ll = new byte[2];
                byte[] p = new byte[1024];

                p[0] = 0x7e; p[1] = 0x7e; p[2] = 0x7e;

                p[5] = (byte)type;//ascll 

                p[6] = (byte)total; 
                p[7] = (byte)index;
                memcpy_byte(p, 8, data, 0, (int)length);
                ll = BitConverter.GetBytes((UInt16)length + 9);
                p[3] = ll[0]; p[4] = ll[1];//len 
                p[length + 8] = get_crc_frame(p, (int)(length + 8));

                sock.SendData(p, (int)length + 9);

                //显示每条发出去的数据
                string str = "";
                for (int z = 0; z < ((int)length + 9); z++) {
                    str += p[z].ToString("X2");
                } 
                m_send_msg.Text = str;
            }
            return 1;
        }

        //计算通讯协议的crc值
        public byte get_crc_frame(byte[] data, int len){
            byte result = 0;
            int ll = len;
            for (int i = 3; i < ll; i++){
                result += data[i];
            }
            return result;
        }
        //计算tcp包的crc位
        public byte Cal_CRC(byte[] data, uint len){
            uint i;
            byte crc_r = 0;
            for (i = 0; i < len; i++)
                crc_r += data[i];
            return crc_r;
        }
        //------------------------------------------------------------------------------------------   
            
            
        //--------判断继电器开关状态----------------------------------------------------------------

        //------------------------------------------------------------------------------------------


        # region 同步时间函数
        //--------同步时间的响应，把arm的时间同步的和电脑一致---------------------------------------
        private void btnTimeSame_Click(object sender, EventArgs e) {
            SynchronizeTime();
        }
        void SynchronizeTime() {
            if (GetConnect_state() == 1){
                byte[] datas = new byte[100];
                UInt16 year = (UInt16)(System.DateTime.Now.Year-2000);
                datas[0] = (byte)(year & 0xff);
                datas[1] = (byte)System.DateTime.Now.Month;
                datas[2] = (byte)System.DateTime.Now.Day;
                datas[3] = (byte)System.DateTime.Now.Hour;
                datas[4] = (byte)System.DateTime.Now.Minute;
                datas[5] = (byte)System.DateTime.Now.Second;
                Get_Para_Client(datas, (byte)'t', 1, 0, 6);
            } else {
                MessageBox.Show(msg_not_connect);
            }
        } 
        //------------------------------------------------------------------------------------------
        #endregion

        # region 继电器测试
        //---------继电器的开关---------------------------------------------------------------------
        void TurnONorOFFRealy(byte n) {
            if (GetConnect_state() == 1) {
                byte[] datas = new byte[100];
                datas[0] = n;
                if (Realy_status[datas[0]] == 0)
                    datas[1] = 1;
                else
                    datas[1] = 0;
                Get_Para_Client(datas, (byte)'e', 1, 0, 2);
                // 获取电流数据 为了实时更新 开关状态 后续应该会在Timer中实时获取电流状态
                byte[] datas2 = new byte[100];
                Get_Para_Client(datas2, (byte)'d', 1, 0, 0);
            } else {
                MessageBox.Show(msg_not_connect);
            }  
        }

        private void btnR1_Click(object sender, EventArgs e) {
            TurnONorOFFRealy(0);
        }

        private void btnR2_Click(object sender, EventArgs e) {
            TurnONorOFFRealy(1);
        }

        private void btnR3_Click(object sender, EventArgs e) {
            TurnONorOFFRealy(2);
        }

        private void btnR4_Click(object sender, EventArgs e) {
            TurnONorOFFRealy(3);
        }

        private void btnR5_Click(object sender, EventArgs e) {
            TurnONorOFFRealy(4);
        }

        private void btnR6_Click(object sender, EventArgs e) {
            TurnONorOFFRealy(5);
        }

        private void btnR7_Click(object sender, EventArgs e) {
            TurnONorOFFRealy(6);
        }

        private void btnR8_Click(object sender, EventArgs e) {
            TurnONorOFFRealy(7);
        }
        //------------------------------------------------------------------------------------------
        # endregion

        //-------设置采样间隔-----------------------------------------------------------------------
        private void setTimerForm_SetTimerEvent(string str){
            try {
                int interval = (int)(Convert.ToDouble(str) * 1000);
                // timerSystemTime.Interval = interval; // 暂时用系统时间做测试
                timerNetdata.Interval = interval;
                MessageBox.Show("已设置为"+interval.ToString()+"ms");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }         
        }

        private void 修改采样间隔GToolStripMenuItem_Click(object sender, EventArgs e) {
            SetTimerForm setTimerForm = new SetTimerForm();
            setTimerForm.SetTimerEvent += setTimerForm_SetTimerEvent;
            setTimerForm.ShowDialog();
        }
        //------------------------------------------------------------------------------------------

        //-------开始采集-----------------------------------------------------------------------------------
        private void btnTestBegin_Click(object sender, EventArgs e) {
            timerDrawLine.Enabled = true;
        }
        //------------------------------------------------------------------------------------------
    }
}
