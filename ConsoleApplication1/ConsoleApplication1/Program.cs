using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    delegate double MathAction(double num);
    class DelegateTest
    {
        // Regular method that matches signature:
        static double Double(double input)
        {
            return input * 2; 
        }
        static void Main(string[] args)
        {
            // Instantiate delegate with named method: 
            MathAction ma = Double;

            // Invoke delagte ma:
            double multByTwo = ma(4.5);
            Console.WriteLine("multByTwo : {0}", multByTwo);

            // Instantiate delegate with anonymous method:
            MathAction ma2 = delegate (double input)
            {
                return input * input;
            };

            double square = ma2(5);
            Console.WriteLine("square : {0}", square);

            // Instantiate delegate with lambda expression 
            MathAction ma3 = s => s * s * s;
            double cube = ma3(4.375);

            Console.WriteLine("cube : {0}", cube);
            Console.ReadLine();
        }
    }
}
