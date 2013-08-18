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
                //Read the original string
                string input = string.Empty;
                Console.Write(">> ");
                input = Console.ReadLine();

                //remove spaces  (trim)
                string inputTrim = input.Trim();

                //split the original input
                List<string> toAdd = new List<string>();
                toAdd = SplitOriginalInput(inputTrim);

                int first = Convert.ToInt32(toAdd[0]);

                for (int i= 1; i < toAdd.Count; i+=2)
                {
                    if (toAdd[i] == "+")
                    {
                        first = Operation(first, Convert.ToInt32(toAdd[i+1]), true);
                    }
                   else if (toAdd[i] == "-")
                    {
                        first = Operation(first, Convert.ToInt32(toAdd[i+1]), false);
                    }
                }
                Console.WriteLine(first);


            } while (true); 
        }

        static List<string> SplitOriginalInput(string input)
        {
            List<string> simbol = new List<string>();
            string aux = string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '+' || input[i] == '-')
                {
                    simbol.Add(aux.Trim());
                    aux = string.Empty;
                    simbol.Add(input[i].ToString());
                }
                else
                {
                    aux += input[i];
                }
            }
            simbol.Add(aux.Trim());
            return simbol;
            
        }//end split Original Input

        static int Operation(int first, int second, bool type)
        {
            int result = 0;

            if (type)
                result = first + second;
            else
                result = first - second;

            return result;
        }// end Opertation
    }
}
