using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassAndInterfaceForm {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        public interface Fly {
            int FlyHeight {
                get;set;
            }
            void SayFlyCondition();
        }
        public abstract class Animal {
            protected string name;
            protected int age;
            public Animal(string name) {
                this.name = name; 
            }
        }
        public class Eagle: Animal, Fly {
            int _FlyHeight;
            public Eagle(string name) : base(name){}
            public int FlyHeight {
                get {
                    return this._FlyHeight;
                }
                set {
                    this._FlyHeight = value;
                }
            }
            public void SayFlyCondition() {
                MessageBox.Show("I am eagle!");
            }
        }
        public abstract class Airplane {
            protected string name;
            protected string manufacturer;
            public abstract void show();
            public Airplane(string manufacturer) {
                this.manufacturer = manufacturer;
            }
            public Airplane(string name,string manufacturer) {
                this.name = name;
                this.manufacturer = manufacturer;
            }
        }
        public class Helicopter : Airplane, Fly {
            public Helicopter(string manufacturer) : base(manufacturer){}
            public Helicopter(string name, string manufactuer) : base(name,manufactuer){}
            public override void show() {
                MessageBox.Show("My manufacturer is "+this.manufacturer+"My flyheight is "+this.FlyHeight);
            }
            int _FlyHeight;
            public int FlyHeight {
                get {
                    return this._FlyHeight;
                }
                set {
                    this._FlyHeight = value;
                }
            }
            public void SayFlyCondition() {
                MessageBox.Show("I am Helicopter!");
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            Helicopter h = new Helicopter(textBox2.Text);
            h.FlyHeight = Convert.ToInt32(textBox4.Text);
            h.show();
        }

        private void button2_Click(object sender, EventArgs e) {
            Eagle eagle = new Eagle(textBox1.Text);
            Helicopter h = new Helicopter(textBox2.Text);
            eagle.SayFlyCondition();
            h.SayFlyCondition();
        }
    }
}
