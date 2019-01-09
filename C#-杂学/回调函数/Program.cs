using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 回调函数 {
    class Program {
        static void Main(string[] args) {
            CalculateClass cc = new CalculateClass();
            FunctionClass fc = new FunctionClass();

            int result1 = cc.PrintAndCalculate(2,3, fc.GetSum);
            Console.WriteLine("调用了开发人员的加法函数，处理后返回结果：" + result1);
 
            int result2 = cc.PrintAndCalculate(2, 3, fc.GetMulti);
            Console.WriteLine("调用了开发人员的乘法函数，处理后返回结果：" + result2);
 
            Console.ReadKey();
        }
    }

    class FunctionClass {
        public int GetSum(int a, int b) {
            return (a + b);
        }
        public int GetMulti(int a, int b) {
            return (a * b);
        }
    }

    #region 这个类会封装起来，只提供函数接口，相当于系统底层
    class CalculateClass {
        public delegate int SomeCalculateWay(int num1, int num2);
        // 将传入参数在系统底层进行某种处理，具体计算方法由开发者开发，函数仅提供执行计算方法
        public int PrintAndCalculate(int num1, int num2, SomeCalculateWay cal) {
            Console.WriteLine("系统底层处理：" + num1);
            Console.WriteLine("系统底层处理：" + num2);
            return cal(num1, num2); // 调用传入函数的一个引用 
        }
    }
    #endregion
}
