using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CockAndLaird {
    public class Laird {
        private string name;
        public Laird(string name) {
            this.name = name;
        }
        public void SayGetUp() {
            MessageBox.Show("Mr."+this.name+":\t该干活了！");
        }
    }
}
