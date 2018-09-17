using System;

namespace CalculatorApplication {
    //可以使用类的实例从另一个类中调用其他类的公有方法
    //class NumberManipulator {
    //    public int FindMax(int num1, int num2) {
    //        int result;
    //        if (num1>num2)
    //            result = num1;
    //        else
    //            result = num2;
    //        return result;
    //    }
    //}
    //class Program {
    //    static void Main(string[] args) {
    //        int a = 100;
    //        int b = 200;
    //        NumberManipulator n = new NumberManipulator();
    //        int ans;
    //        ans = n.FindMax(a,b);
    //        Console.WriteLine("MAX:"+ans);
    //        Console.ReadLine();
    //    }
    //}
    class NumbermManipulator{
        //public int factorial(int num) {
        //    int result;
        //    if (num == 1) {
        //        return 1;
        //    } else {
        //        result = factorial(num-1)*num;
        //        return result;
        //    }
        //}
        
        //static void Main(string[] args) {
        //    NumbermManipulator n = new NumbermManipulator();
        //    Console.WriteLine("6阶乘："+n.factorial(6));
        //}


        //引用参数是一个对变量的内存位置的引用。当按引用传递参数时，与值参数不同的是，它不会为这些参数创建一个新的存储位置。引用参数表示与提供给方法的实际参数具有相同的内存位置。
        //在 C# 中，使用 ref 关键字声明引用参数。
        public void swap(ref int x, ref int y) {
            int temp;
            temp = x;
            x = y;
            y = temp;
        }

        static void Main(string[] args) {
            NumbermManipulator n = new NumbermManipulator();
            int a = 100;
            int b = 200;
            n.swap(ref a, ref b);
            Console.WriteLine("a"+a+"B"+b);
        }
    }



}
