using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2_绘图 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        Bitmap bitmap;

        private void Form1_Load(object sender, EventArgs e) {
            // 全局变量
            int Height = this.pictureBox1.Height;
            int Width = this.pictureBox1.Width;
            //创建位图
            bitmap = new Bitmap(Width, Height);
        }

        private void button2_Click(object sender, EventArgs e) {
            Color c = Color.FromArgb(44,55,66,77);

            Graphics g = Graphics.FromImage(bitmap);// 创建Graphics对象
            Pen redPen = new Pen(Color.Red,2.2f);
            Pen bluePen = new Pen(Color.Blue,6);
            Pen greenPen = new Pen(Color.Green,4);

            Point p1 = new Point(40,30);
            Point p2 = new Point(160,30);
            
            redPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            redPen.Width = 3;

            g.DrawLine(redPen,10,10,10,180);

            bluePen.StartCap = System.Drawing.Drawing2D.LineCap.RoundAnchor;
            bluePen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            
            g.DrawLine(bluePen,p1,p2);
            g.DrawLine(greenPen,30,60,160,160);
            
            this.pictureBox1.Image = bitmap;
        }

        private void button1_Click(object sender, EventArgs e) {
            Graphics g = Graphics.FromImage(bitmap);// 创建Graphics对象
            g.Clear(Color.White);      
            this.pictureBox1.Image = bitmap;
        }

        private void button3_Click(object sender, EventArgs e) {
            Graphics g = Graphics.FromImage(bitmap);// 创建Graphics对象
            HatchBrush bru1 = new HatchBrush(HatchStyle.Shingle, Color.Yellow, Color.Red);
            g.FillRectangle(bru1,100,0,50,50);
            HatchBrush bru2 = new HatchBrush(HatchStyle.Cross, Color.White, Color.Green);
            g.FillRectangle(bru2,100,50,50,50);
            SolidBrush bru3 = new SolidBrush(Color.Red);
            g.FillRectangle(bru3,100,100,50,50);
            LinearGradientBrush bru4 = new LinearGradientBrush(new Rectangle(0,50,50,100), Color.Red, Color.Green,90);
            bru4.WrapMode = WrapMode.TileFlipX;
            g.FillRectangle(bru4,100,150,50,100);
            this.pictureBox1.Image = bitmap;
        }

        private void button4_Click(object sender, EventArgs e) {

        }
    }
}
