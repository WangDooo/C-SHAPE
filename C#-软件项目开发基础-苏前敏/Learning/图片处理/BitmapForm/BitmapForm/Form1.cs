using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitmapForm {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            int Height = this.pictureBox1.Image.Height;
            int Width = this.pictureBox1.Image.Width;
            Bitmap newbitmap = new Bitmap(Width, Height);
            Bitmap oldbitmap = (Bitmap)this.pictureBox1.Image;
            Color pixel;
            for (int x = 1; x < Width; x++) {
                for(int y = 1; y < Height; y++) {
                    int r,g,b;
                    pixel = oldbitmap.GetPixel(x,y);
                    r = 255 - pixel.R;
                    g = 255 - pixel.G;
                    b = 255 - pixel.B;
                    newbitmap.SetPixel(x,y,Color.FromArgb(r,g,b));
                }
            }
            this.pictureBox2.Image = newbitmap;
        }

        private void button2_Click(object sender, EventArgs e) {
            int Height = this.pictureBox1.Image.Height;
            int Width = this.pictureBox1.Image.Width;
            Bitmap newbitmap = new Bitmap(Width, Height);
            Bitmap oldbitmap = (Bitmap)this.pictureBox1.Image;
            Color pixel1, pixel2;
            for (int x = 1; x < Width-1; x++) {
                for(int y = 1; y < Height-1; y++) {
                    int r =0 ,g = 0 ,b = 0;
                    pixel1 = oldbitmap.GetPixel(x,y);
                    pixel2 = oldbitmap.GetPixel(x+1,y+1);
                    r = Math.Abs(pixel1.R - pixel2.R + 128);
                    g = Math.Abs(pixel1.G - pixel2.G + 128);
                    b = Math.Abs(pixel1.B - pixel2.B + 128);
                    if (r > 255)
                        r = 255;
                    if (g > 255)
                        g = 255;
                    if (b > 255)
                        b = 255;
                    newbitmap.SetPixel(x,y,Color.FromArgb(r,g,b));
                }
            }
            this.pictureBox3.Image = newbitmap;
        }
    }
}
