using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interprete
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                
            //original string
            string input = string.Empty;
            Console.Write(">> ");
            input = Console.ReadLine();

            //separators to split the original string
            char[]  separators = new char[] { '+','-'};
            string[] arraySub = input.Split(separators[1]);
            string[] arraySum = input.Split(separators[0]);

            if (arraySub.Length < 2)
                //sumar Numeros
                Console.WriteLine(SumarNumeros(arraySum));
            else
                //restar Numeros
                Console.WriteLine(RestarNumeros(arraySub));

            Console.WriteLine("           ");
            } while (true);
        }
        static int SumarNumeros(string[] array)
        {
            int result = 0;
            int a = Convert.ToInt32(array[0]);
            int b = Convert.ToInt32(array[1]);
            return result = a+b;
        }
        static int RestarNumeros(string[] array)
        {
            int result = 0;
            int a = Convert.ToInt32(array[0]);
            int b = Convert.ToInt32(array[1]);
            return result = a-b;
        }
    }
}
