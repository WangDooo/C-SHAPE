using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CockAndLaird {
    public delegate void SayGetUpDelegate();
    public class Cock {
        public event SayGetUpDelegate GetUp;
        protected string name;
        public Cock(string name) {
            this.name = name;
        }
        public void Crow() {
            MessageBox.Show("Mr."+this.name+ ":\t鸡叫声");
            GetUp();
        }
    }
}
