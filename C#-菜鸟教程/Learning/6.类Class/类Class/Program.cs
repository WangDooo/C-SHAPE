using System;

namespace LineApplication {
    class Line2 {
        public double length;
        public Line2() {
            Console.WriteLine("对象已建立");
        }
    }
    
    // 构造函数的名称与类的名称完全相同，它没有任何返回类型。

    class Program {
        static void Main(string[] args) {
            Line2 line = new Line2();
            line.length = 1;
            Console.WriteLine(line.length);
        }
    }
}
