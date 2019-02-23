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
    
    //class Program {
    //    public delegate void BookDelegate(Book[] books);
    //    static void Main(string[] args) {
    //        BookDelegate bookDelegate = new BookDelegate(Book.BookSort); 
    //        Book[] book = new Book[3];
    //        book[0] = new Book("123",45);
    //        book[1] = new Book("12443",35);
    //        book[2] = new Book("123999",945);
    //        bookDelegate(book);
    //        foreach(Book bk in book) {
    //            Console.WriteLine(bk);
    //        }
    //    }
    //}

    class Order {
        public static void BuyFood() {
            Console.WriteLine("FOOD!");
        }
        public static void BuyCake() {
            Console.WriteLine("CAKE");
        }
        public static void BuyFlower() {
            Console.WriteLine("Flower");
        }
    }

    //class Program {
    //    public delegate void OrderDelegate();
    //    static void Main(string[] args) {
    //        // 实例化委托
    //        OrderDelegate orderDelegate = new OrderDelegate(Order.BuyFlower);
    //        // 向委托中注册方法
    //        orderDelegate += Order.BuyFood;
    //        orderDelegate += Order.BuyCake;
    //        // 调用委托
    //        orderDelegate();
    //    }
    //}

    class Program {
        // 定义委托
        public delegate void SayDelegate();
        // 定义事件
        public event SayDelegate SayEvent;
        // 定义委托中调用的方法
        public void SayHello() {
            Console.WriteLine("Hello Event!");
        }
        // 创建触发事情的方法
        public void SayEventTrigger() {
            // 触发事件， 必须与事件是同名的方法
            SayEvent();
        }
        static void Main(string[] args) {
            // 创建program类的实例
            Program program = new Program();
            // 实例化事件，使用委托指向处理方法
            program.SayEvent = new SayDelegate(program.SayHello);
            // 调用触发事件的方法
            program.SayEventTrigger();
        }
    }
}
