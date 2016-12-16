using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_event
{
    class Program
    {
        // Méthode 1
        delegate void Mydelegate(string s);
        // Méthiode 2
        delegate string MyDelegate2();

        //Méthode avec la class animal (3)
        delegate Animal MyDelegateAnimal();
        delegate void MyDelegatedog(Dog d);
        static void Main(string[] args)
        {
        //Méthode 1
            Mydelegate del = new Mydelegate(Testfunction);
            del += Testfunction; // invoque une 2ème fois la fonction Testfunction.
            //résultat : Hello world apparaitra 2 fois à l'écran sur 2 lignes différentes (writeline). 
            del -= Testfunction; // supprime la 2éme invocation de Testfunction.
            //résultat : Hello world n'apparaitra qu'une fois à l'écran.          
            del("Hello world");

            //Peut aussi s'écrire:
            Mydelegate del2 = Testfunction;
            
            del2("bonjour le monde");

        //Méthode 2
            MyDelegate2 testdel = Testfunction2;
            string result = testdel();
            Console.WriteLine(result);



            // Méthode 3 
            MyDelegateAnimal testanimal = testFunctionA;
        }
        static void tes

        static void Testfunction(string text)
        {
            Console.WriteLine(text); 
        }

        static string Testfunction2()
        {
            return "Bye world";
        }

        static void testFunctionA(Animal a)
        {

        }


        static Dog Testdog()
        { 
            return ; 
        }
    }

    class Animal
    {

    }

    class Dog : Animal
    {

    }
}
