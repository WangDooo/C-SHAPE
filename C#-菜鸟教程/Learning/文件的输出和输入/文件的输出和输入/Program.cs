using System;
using System.IO;

namespace FileIOApplication {
    ////  FileStream 类的用法 
    //class Program {
    //    static void Main(string[] args) {
    //        FileStream F = new FileStream("test.dat",FileMode.OpenOrCreate,FileAccess.ReadWrite);
    //        for (int i = 1; i <= 20; i++) {
    //            F.WriteByte((byte)i);
    //        }
    //        F.Position = 0;
    //        for (int i = 0; i <= 20; i++) {
    //            Console.Write(F.ReadByte()+" ");
    //        }
    //        F.Close();
    //        Console.ReadKey();
    //    }
    //}

    // 文本文件的读写,StreamReader 和 StreamWriter 类用于文本文件的数据读写。
    
    //class Program {
    //    static void Main(string[] args) {
    //        try {
    //            using (StreamReader sr = new StreamReader("jamaica.txt")) {
    //                string line;
    //                while((line = sr.ReadLine()) != null) {
    //                    Console.WriteLine(line);
    //                }
    //            }
    //        } catch (Exception e) {
    //            Console.WriteLine("The file could not be read:");
    //            Console.WriteLine(e.Message);
    //        }
    //    }
    //}

    // 使用 StreamWriter 类向文件写入文本数据
    //class Program {
    //    static void Main(string[] args) {
    //        string[] names = new string[]{ "WangDoo", "HUBU123"};
    //        using (StreamWriter sw = new StreamWriter("names.txt")) {
    //            foreach(string s in names) {
    //                sw.WriteLine(s);
    //            }
    //        }

    //        string line;
    //        using (StreamReader sr = new StreamReader("names.txt")) {  
    //            while ((line = sr.ReadLine()) != null) {
    //                Console.WriteLine(line);
    //            }
    //        }
    //    }
    //}

    // C# 允许您使用各种目录和文件相关的类来操作目录和文件，比如 DirectoryInfo 类和 FileInfo 类。
    class Program {
        static void Main(string[] args) {
            DirectoryInfo mydir = new DirectoryInfo("./");

            FileInfo[] f = mydir.GetFiles();
            foreach(FileInfo file in f) {
                Console.WriteLine("File name: {0} Size: {1}",file.Name, file.Length);
            }
        }
    }

}
