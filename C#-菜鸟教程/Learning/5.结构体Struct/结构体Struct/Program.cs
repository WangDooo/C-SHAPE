using System;
using System.Text;

namespace Structure {
    struct Books {
        public string title;
        public int id;
        public void get(string t, int idd) {
            title = t;
            id = idd;
        }
        public void display() {
            Console.WriteLine("TITLE  "+title);
            Console.WriteLine("IDIDID  "+id);
        }
    };
    
    class Program {
        static void Main(string[] args) {
            Books Book1;
            Book1.title = "qweqew";
            Book1.id = 123;

            Console.WriteLine("title："+Book1.title);
            Console.WriteLine("id："+Book1.id);

            Books Book2 = new Books();
            Book2.get("asdsad as",999);
            Book2.display();
        }
    }
}
