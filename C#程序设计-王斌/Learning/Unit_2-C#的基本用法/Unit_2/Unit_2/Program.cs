using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("千位" + 1234 / 1000);
            //Console.WriteLine("百位" + 1234 / 100 % 10);
            //Console.WriteLine("十位" + 1234 / 10 % 10);
            //Console.WriteLine("个位" + 1234 % 10);
            //Console.WriteLine("10是偶数：" + (10 % 2 == 0));
            //Console.WriteLine("2的立方：" +(2<<2)); // 位运算
            //Console.WriteLine("10是：" + (10 % 2 == 0?"偶":"奇")); // 三元运算符
            //Console.WriteLine("输入一个整数");
            //int num = int.Parse(Console.ReadLine());
            //String msg = num + "不是偶数";
            //if (num % 2 == 0) {
            //    msg = num + "是偶数";
            //}
            //Console.WriteLine(msg);
            //for (int i = 1; i <= 9; i++) {
            //    for (int j = 1; j <= i; j++) {
            //        Console.Write(i + "*" + j + "=" + i * j + "\t");
            //    }
            //    Console.WriteLine();
            //}

            int count = 1;
            login:
                Console.WriteLine("Username:");
                string username = Console.ReadLine();
                Console.WriteLine("Password:");
                string password = Console.ReadLine();
                if (username=="123" && password == "123"){
                    Console.WriteLine("RIGHT!");
                }else{
                    count++;
                    if (count>3){
                        Console.WriteLine("WRONG TIME MORE THAN 3");
                    }else{
                        Console.WriteLine("WRONG!");
                        goto login;
                    }
                }
        }
    }
}
