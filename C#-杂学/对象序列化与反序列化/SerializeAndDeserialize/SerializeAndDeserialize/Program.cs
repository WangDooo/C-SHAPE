using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;

namespace SerializeAndDeserialize {
    class Program {
        static void Main(string[] args) {
            string p = "\n123";
            //SerializeNow();
            //DeSerializeNow();
            //SerializeSoap();
            //DeSerializeSoap();
            XMLSerialize();
            XMLDeserialize();
            Console.WriteLine(p);
            Console.Read();
        }

        public static void XMLSerialize() {
            Person c = new Person("WangDoo");
            c.Courses = new Course[2];
            c.Courses[0] = new Course("English", "土木");
            c.Courses[1] = new Course("数学", "土木MAth");
            XmlSerializer xs = new XmlSerializer(typeof(Person));
            Stream stream = new FileStream("C:\\DDD\\temp\\tempxml.qqq",FileMode.Create, FileAccess.Write, FileShare.Read);
            xs.Serialize(stream, c);
            stream.Close();
        }

        public static void XMLDeserialize() {
            XmlSerializer xs = new XmlSerializer(typeof(Person));
            Stream stream = new FileStream("C:\\DDD\\temp\\tempxml.qqq",FileMode.Open, FileAccess.Read, FileShare.Read);
            Person p = xs.Deserialize(stream) as Person;
            Console.WriteLine(p.Name);
            Console.WriteLine(p.Age.ToString());
            Console.WriteLine(p.Courses[0].Name);
            Console.WriteLine(p.Courses[0].Description);
            Console.WriteLine(p.Courses[1].Name);
            Console.WriteLine(p.Courses[1].Description);
            stream.Close();
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

        public static void SerializeSoap() {
            ClassToSerialize c = new ClassToSerialize();
            FileStream fileStream = new FileStream("C:\\DDD\\temp\\temp.xml", FileMode.Create);
            SoapFormatter b = new SoapFormatter();
            b.Serialize(fileStream, c);
            fileStream.Close();
        }

        public static void DeSerializeSoap() {
            ClassToSerialize c = new ClassToSerialize();
            c.Sex = "FFFF";
            FileStream fileStream = new FileStream("C:\\DDD\\temp\\temp.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
            SoapFormatter b = new SoapFormatter();
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

    [Serializable]
    public class Person {
        private string name;
        public string Name {
            get {
                return name;
            }
            set {
                name = value;
            }
        }
        public string Sex;
        public int Age = 3;
        public Course[] Courses;
        public Person() {

        }
        public Person(string Name) {
            name = Name;
            Sex = "男";
        }
    }
    [Serializable]
    public class Course {
        public string Name;
        [XmlIgnore]
        public string Description;
        public Course() {

        }
        public Course(string name, string description) {
            Name = name;
            Description = description;
        }
    }

    
}
