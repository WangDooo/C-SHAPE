using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace SerializeAndDeserialize {
    class Program {
        static void Main(string[] args) {
            string p = "123";
            SerializeNow();
            DeSerializeNow();
            Console.WriteLine(p);
            Console.Read();
        }

        public static void SerializeNow() {
            ClassToSerialize c = new ClassToSerialize();
            FileStream fileStream = new FileStream("C:\\DDD\\temp\\temp.zph", FileMode.Create);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(fileStream, c);
            fileStream.Close();
        }

        public static void DeSerializeNow() {
            ClassToSerialize c = new ClassToSerialize();
            c.Sex = "FFFF";
            FileStream fileStream = new FileStream("C:\\DDD\\temp\\temp.zph", FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryFormatter b = new BinaryFormatter();
            c = b.Deserialize(fileStream) as ClassToSerialize;
            Console.Write(c.name);
            Console.Write(c.Sex);
            fileStream.Close();
        }
    }

    [Serializable]
    public class ClassToSerialize {
        public int id = 100;
        public string name = "Name";
        [NonSerialized]
        public string Sex = "Male";
    }

    
}
