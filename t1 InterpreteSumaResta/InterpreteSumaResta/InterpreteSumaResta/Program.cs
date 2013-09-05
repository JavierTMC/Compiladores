using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace InterpreteSumaResta
{
    class Program
    {

        //Lista de tokens
        static LinkedList<Token> LaLista = new LinkedList<Token>();
        static LinkedListNode<Token> CurrentToken = null;
        static int ElIntG=0;

        static void Main(string[] args)
        {
            string ElString = string.Empty;

            //Introducir datos
            Console.WriteLine("Por favor introduzca la operación a calular :3\nUsted puede sumar y restar.");

            Console.Write("\nOperación: ");
            ElString = Console.ReadLine();
            if (Tokenizer(ElString))
            {
                Generar(0);
                Console.WriteLine("\n El resultado  es : {0}",ElIntG);
            }

            else
            {
                Console.WriteLine("Intentalo de nuevo :'(");
            }

            Console.ReadKey(true);
        }

        static bool Tokenizer(string ElString)
        {
            LaLista.AddLast(new Token(TipoToken.NULL,' '));

            for (int i = 0; i <= ElString.Length-1; i++)
            {
                char ElChar=ElString[i];

                if (ElChar == '+' || ElChar == '-')
                {
                    LaLista.AddLast(new Token(TipoToken.OP, ElChar));
                }

                else if (Regex.IsMatch(ElChar.ToString(), @"[0-9]"))
                {
                    LaLista.AddLast(new Token(TipoToken.NUM, ElChar));
                }

                else
                {
                    return false;
                }
            
            }
            LaLista.AddLast(new Token(TipoToken.NULL,' '));
            CurrentToken = LaLista.First;

            return true;
        }

        static LinkedListNode<Token> GetNextToken()
        {
            CurrentToken = CurrentToken.Next;
            return CurrentToken;
        }

        static int SumOperation(int n1, int n2)
        {
            return n1 + n2;
        }

        static int DifOperation(int n1, int n2)
        {
            return n1 - n2;
        }

        static int Generar(int ElInt)
        {
            LinkedListNode<Token> ElToken = GetNextToken();

            if (ElToken.Value.Tipo == TipoToken.NUM)
            {
                int num1 = int.Parse(ElToken.Value.Valor.ToString());
                LinkedListNode<Token> t1 = GetNextToken();

               if(t1.Value.Tipo==TipoToken.OP)
                {                  
                LinkedListNode<Token> t2 = GetNextToken();
                if (t2.Value.Tipo == TipoToken.NUM)
                {
                    int num2 = int.Parse(t2.Value.Valor.ToString());
                    int r1 = 0;
                    if (t1.Value.Valor == '+')
                    {
                        //llamar al metodo sumar y asignar el resultado a r1
                        r1 = SumOperation(num1, num2);
                    }

                    if (t1.Value.Valor == '-')
                    {
                        // llamar al metodo restar y asignar a r1
                        r1 = DifOperation(num1, num2);
                    }
                    ElIntG = r1;
                    Generar(r1);
                }
                }
               return ElInt;
            }
            else if (ElToken.Value.Tipo == TipoToken.OP)
            {
                LinkedListNode<Token> t3 = GetNextToken();
                if (t3.Value.Tipo == TipoToken.NUM)
                {
                    int n3 = int.Parse(t3.Value.Valor.ToString());
                    int r2 = 0;

                    if (ElToken.Value.Valor == '+')
                    {
                        //lamar metodo sumop, parametros(ElInt,n3) y agregar resultado a r2
                        r2 = SumOperation(ElInt, n3);
                    }
                    else if (ElToken.Value.Valor == '-')
                    {
                        //llamar metodo summenos, parametros(ElInt,n3) y agregar resultado a r2
                        r2 = DifOperation(ElInt, n3);
                    }
                    ElIntG = r2;
                    Generar(r2);
                }
                return ElInt;
            }
            else if(ElToken.Value.Tipo == TipoToken.NULL)
            {
                ElIntG = ElInt;
                return ElInt;
            }

            return ElIntG;
        }
    }//finaliza clase programa

    class Token
    {
        public TipoToken Tipo { get; set; }
        public char Valor { get; set; }

        public Token(TipoToken tipo, char valor)
        {
            Tipo = tipo;
            Valor = valor;
        }
    }

    enum TipoToken
    {
        NUM, OP,NULL
    }
}