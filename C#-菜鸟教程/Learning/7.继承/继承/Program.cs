using System;

namespace InheritanceApplication {
    // //基类和派生类
    //class Shape {
    //    // protected：只有该类对象及其子类对象可以访问
    //    protected int width;
    //    protected int height;
    //    public void setWidth(int w) {
    //        width = w;
    //    }
    //    public void setHeight(int h) {
    //        height = h;
    //    }
    //}

    //// 派生类
    //class Rectangle: Shape {
    //    public int getArea() {
    //        return (width*height);
    //    }
    //}
    
    //class RectangleTester {
    //    static void Main(string[] args) {
    //        Rectangle r = new Rectangle();
    //        r.setWidth(3);
    //        r.setHeight(9);
    //        Console.WriteLine(r.getArea());
    //    }
    //}

    // 基类的初始化
    class Rectangle {
        protected double length;
        protected double width;
        // 对父类进行初始化
        public Rectangle (double l, double w) {
            length = l;
            width = w;
        }
        public double getArea() {
            return length*width;
        }
        public void Display() {
            Console.WriteLine("LENGTH"+length);
            Console.WriteLine("WIDHT"+width);
            Console.WriteLine("AREA="+getArea());
        }
    }
    
    class Tabletop: Rectangle {
        private double cost;
        public Tabletop(double l, double w) : base(l, w) { }
        public double getCost() {
            double cost;
            cost = getArea() * 88;
            return cost;
        }
        public void Display() {
            base.Display();
            Console.WriteLine("成本="+getCost());
        }
    }

    class ExecuteRectangle {
        static void Main(string[] args) {
            Tabletop t = new Tabletop(4.5,7.5);
            t.Display();
        }
    }
}
