using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event {
    class EventUtilClass {
        public delegate void EventUtilEventHandler(object sender, System.EventArgs e); // 1. 定义delegate对象
        public class EventClass {
            public void EventUtil_Function(object sender, System.EventArgs e) { // 3. 定义处理事件的方法 与delegate对象有相同的参数
                Console.WriteLine("using event to print!");
            }
        }

        // 4. 用event关键词 定义事件对象
        private event EventUtilEventHandler utilEvent;
        private EventClass eventClass;

        // 构造函数
        public EventUtilClass() {
            eventClass = new EventClass();
            this.utilEvent += new EventUtilEventHandler(eventClass.EventUtil_Function);
        }
        static void Main(string[] args) {
        }
    }
}
