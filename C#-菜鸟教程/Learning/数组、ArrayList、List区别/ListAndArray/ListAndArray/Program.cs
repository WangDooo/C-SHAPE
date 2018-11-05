using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListAndArray {
    class Program {
        static void Main(string[] args) {
            List<double> list = new List<double>();
            for (int i = 0; i < 4; i++) {
                double temp =i*0.63;
                list.Add(temp);
            }
            double[] arr = {1.2,2.3,5.6};
            list.AddRange(arr);
            list.Insert(3,999);
            list.Insert(4,999);
            list.Sort();

            foreach(double t in list) {
                Console.WriteLine(t);
            }
            for(int i = 0; i < list.Count(); i++) {
                Console.Write(list[i]);
            }
            Console.WriteLine(list[2]);
        }
    }
}
