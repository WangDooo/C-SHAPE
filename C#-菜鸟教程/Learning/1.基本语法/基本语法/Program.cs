using System;

namespace RectangleApplication {
    class Rectangle {
        double length;
        double width;
        public void Acceptdeatails() {
            length = 4.5;
            width = 3.5;
        }
        public double GetArea() {
            return length*width;
        }
        public void Display() {
            Console.WriteLine("Length:{0}",length);
            Console.WriteLine("Width:"+width);
            Console.WriteLine("Area:{0}",GetArea());
        }
    }
    
    class ExecuteRectangle {
        static void Main(string[] args) {
            Rectangle r = new Rectangle();
            r.Acceptdeatails();
            r.Display();
            Console.ReadLine();
        }
    }
}
