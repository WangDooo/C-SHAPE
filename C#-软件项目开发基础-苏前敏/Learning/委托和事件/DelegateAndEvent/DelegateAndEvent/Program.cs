using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate {
    class Program {
        public delegate void DelegateFun(int x, int y);
        static void Main(string[] args) {
            DelegateFun fun;
            int x = 4;
            int y = 2;
            fun = new DelegateFun(fun1);
            fun(x,y);
            fun = fun2;
            fun(x,y);
            fun = fun3;
            fun(x,y);

            fun = fun1;
            fun += fun2;
            fun += fun3;
            fun -= fun1;
            fun(x,y);
            Console.ReadLine();
        }
        public static void fun1(int x, int y) {
            Console.WriteLine("x+y={0}", x+y);
        }
        public static void fun2(int x, int y) {
            Console.WriteLine("x-y={0}", x-y);
        }
        public static void fun3(int x, int y) {
            Console.WriteLine("x*y={0}", x*y);
        }
    }
}
