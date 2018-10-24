using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsForm {
    public partial class Form4 : Form {
        public Form4() {
            InitializeComponent();
        }
        Graphics g;
        Point[] snake = new Point[5];
        private void Form4_Load(object sender, EventArgs e) {
            g = this.CreateGraphics();
            for (int i = 0; i < 3; i++) {
                snake[i].X = 20*i+10;
                snake[i].Y = 100;
            }
        }
        
        private void timer1_Tick(object sender, EventArgs e) {
            SolidBrush solidBrush = new SolidBrush(Color.Red);
            for (int i = 0; i < 3; i++) {
                g.FillRectangle(solidBrush,snake[i].X,snake[i].Y,20,20);
                g.DrawRectangle(new Pen(Color.Blue),snake[i].X,snake[i].Y,20,20);
            }
            SolidBrush backBrush = new SolidBrush(this.BackColor);
            int x = snake[0].X;
            int y = snake[0].Y;
            g.FillRectangle(backBrush,x,y,20,21);
            for (int i = 0; i < 2; i++) {
                snake[i].X = snake[i+1].X;
                snake[i].Y = snake[i+1].Y;
            }
            snake[2].X = snake[2].X + 20;
            snake[2].Y = snake[2].Y;
            
            

        }
    }
}
