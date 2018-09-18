using System;

namespace StringApplication {
    class Program {
        static void Main(string[] args) {
            // 字符串的连接
            string fname, lname;
            fname = "Wang";
            lname = "DOO";

            string fullname = fname + lname;
            Console.WriteLine("Fullname="+fullname);

            // 使用string构造函数
            char[] letters = { 'W','a','n','g'};
            string namechar = new string(letters);
            Console.WriteLine("NEWCHAR:"+namechar);

            // 方法返回字符串
            string[] sarray = { "Hello", "Wang", "Doo", "Googo"};
            string message = String.Join("-", sarray); // 每个次之间的连接
            Console.WriteLine(message);
            Console.WriteLine(message.Length);

            // 格式化
            DateTime waiting = new DateTime(2018,9,18,10,41,56);
            string chat = String.Format("{0:tt dddd yyyy MMMM HH mm ss}",waiting);
            Console.WriteLine(chat);
        }
    }
}
