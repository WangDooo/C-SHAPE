using System;

namespace ErrorHandlingApplication {
    class DivNumbers {
        int result;
        DivNumbers() {
            result = 0;
        }
        public void division(int num1, int num2) {
            try {
                result = num1 / num2;
            }
            catch(DivideByZeroException e) {
                Console.WriteLine("Exception caught: {0}",e);
            } finally {
                Console.WriteLine("Result"+result);
            }
        }

        static void Main(string[] args) {
            DivNumbers d = new DivNumbers();
            d.division(12,0);
        }     
    }
}
