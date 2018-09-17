using System;

namespace ArrayApplication {
    //class Program {
    //    static void Main(string[] args) {
    //        double[] balance = new double[10];
    //        balance[1] = 12.3;
    //        Console.WriteLine(balance[1]);
    //        double[] b2 = { 15156.3,61,25,656,99,9,9,6,6};
    //        for (int i = 0; i < 5; i++) {
    //            Console.WriteLine(b2[i]);
    //        }
    //        int [] marks = new int[]{ 44,13,123,133};
    //        int [] socre  = marks;
    //        // 二维数组
    //        string [,] names;
    //        int [ , , ] m;
    //        int [,] a = new int [3,4]{ 
    //            { 0,0,0,0},
    //            { 1,2,3,4},
    //            { 9,9,9,8}
    //            }; // 三行4列
    //        Console.WriteLine("a[2,3]="+a[2,3]);// a[2,3] 第三行第四列 c#也是从0开始计数的
    //    }
    //}

    class MyArray{
        double getAverage(int[] arr, int size) {
            int i;
            double avg;
            int sum = 0;
            for (i = 0; i < size; ++i) {
                sum += arr[i];
            }
            avg = (double)sum/size;
            return avg;
        }
        static void Main(string[] args) {
            MyArray app = new MyArray();
            int[] b1 = new int[]{ 1,2,3,4,5 };
            double avg;
            avg = app.getAverage(b1, b1.Length);
            Console.WriteLine("平均值：{0}", avg);
            Console.ReadLine();
        }
    }
}
