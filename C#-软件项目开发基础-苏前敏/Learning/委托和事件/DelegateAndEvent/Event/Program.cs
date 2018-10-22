using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event {
    class EventUtilClass {
        public delegate void EventUtilEventHandler(object sender, System.EventArgs e); // 定义delegate对象
        public class EventClass {
            public void EventUtil_Function(object sender, System.EventArgs e) {
                Console.WriteLine("using event to print!");
            }
        }
        static void Main(string[] args) {
        }
    }
}
