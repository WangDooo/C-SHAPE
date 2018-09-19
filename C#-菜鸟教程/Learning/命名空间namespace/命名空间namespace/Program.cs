//using System;

//// 在一个命名空间中声明的类的名称与另一个命名空间中声明的相同的类的名称不冲突。
//namespace first_space {
//    class abc {
//        public void func() {
//            Console.WriteLine("FIrst space");
//        }
//    }
//}

//namespace second_space {
//    class abc {
//        public void func() {
//            Console.WriteLine("Second space");
//        }
//    }
//}

//namespace TestClass{
//    class Program {
//        static void Main(string[] args) {
//            first_space.abc fc = new first_space.abc();
//            second_space.abc sc = new second_space.abc();
//            fc.func();
//            sc.func();
//        }
//    }
//}


// using 关键字

using System;
using first_space;
using second_space;

namespace first_space {
    class abc {
        public void func() {
            Console.WriteLine("FIrst space");
        }
    }
}

namespace second_space {
    class abc2 {
        public void func() {
            Console.WriteLine("Second space2222");
        }
    }
}

namespace TestClass {
    class Program {
        static void Main(string[] args) {
            abc fc = new abc();
            abc2 sc = new abc2();
            fc.func();
            sc.func();
        }
    }
}