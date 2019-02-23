using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace 序列化与反序列化 {
    class Program {
        static void Main(string[] args) {
            // 创建Programmer列表 并添加对象
            List<Programmer> list = new List<Programmer>();
            list.Add(new Programmer("Wang",true,"C#"));
            list.Add(new Programmer("Li",false,"C++"));
            list.Add(new Programmer("Ma",true,"Python"));
            // 使用二禁止序列化对象
            string fileName = @"D:\Coding\Github\C-SHAPE\C#-杂学\对象序列化与反序列化\Programmers.zph";
            Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            BinaryFormatter binFormat = new BinaryFormatter(); // 创建二进制序列化器
            binFormat.Serialize(fStream, list);
            // 使用二进制反序列化对象
            list.Clear(); 
            fStream.Position = 0; // 重置流位置
            list = (List<Programmer>)binFormat.Deserialize(fStream);
            foreach(Programmer p in list) {
                Console.WriteLine(p);
            }
            Console.Read();
        }
    }

    [Serializable] // 必须添加序列化特性
    public class Person {
        private string Name;
        private bool Sex;
        public Person(string name, bool sex) {
            this.Name = name;
            this.Sex = sex;
        }
        public override string ToString() {
            return "姓名：" + this.Name + "\t性别：" + (this.Sex ? "男" : "女");
        }
    }

    [Serializable]
    public class Programmer: Person {
        private string Language;
        public Programmer(string name, bool sex, string language): base(name, sex) {
            this.Language = language;
        }
        public override string ToString() {
            return base.ToString() + "\t编程语言：" + this.Language;
        }
    }
}
