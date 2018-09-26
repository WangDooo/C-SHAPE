using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_8 {
    // 静态
    //class Test {
    //    public static void SayHello() {
    //        Console.WriteLine("Hellooooooo");
    //    }
    //}
    
    //class Program {
    //    public delegate void MyDelegate();
    //    static void Main(string[] args) {
    //        MyDelegate myDelegate = new MyDelegate(Test.SayHello);
    //        myDelegate();
    //    }
    //}

    
    // 实例 类的实例调用方法 new 类名().方法名
    //class Test {
    //    public void SayHello() {
    //        Console.WriteLine("Hellooooooo");
    //    }
    //}

    //class Program {
    //    public delegate void MyDelegate();
    //    static void Main(string[] args) {
    //        MyDelegate myDelegate = new MyDelegate(new Test().SayHello);
    //        myDelegate();
    //    }
    //}

    class Book : IComparable<Book>{
        public Book(string name, double price) {
            Name = name;
            Price = price; 
        }
        public string Name{get; set;}
        public double Price{get; set;}
        // 实现比较器的比较方法
        public int CompareTo(Book other){
            return (int)(this.Price - other.Price);
        }
        // 重写ToString()方法，返回图书名称和价格
        public override string ToString() {
            return Name+":"+Price;
        }
        // 图书信息排序
        public static void BookSort(Book[] books) {
            Array.Sort(books);
        }
    }
    
    class Program {
        public delegate void BookDelegate(Book[] books);
        static void Main(string[] args) {
            
        }
    }
}
