using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace InfixToPostfix_sdt
{
    class Program
    {
        //Lista de tokens
        static LinkedList<Token> LaLista = new LinkedList<Token>();
        static LinkedListNode<Token> CurrentToken = null;

        static void Main(string[] args)
        {
            Console.WriteLine("Conversion de Infix a Postfix (SDT) ");
            while (true)
            {
                string ElString = string.Empty;
                //Introducir datos
                Console.Write("\nOperación Infix: ");
                ElString = Console.ReadLine();

                Tokenizer(ElString);
                Console.WriteLine("Conversion de Infix a Postfix -> {0}", infixToPostfix());
                Console.Write("\n");
                LaLista.Clear();
            }
        }

        // expr -> term term_rest
        // term_rest -> + expr || - expr || epsilon
        // term - > num
        // num -> 0 .. 9

        static string infixToPostfix()
        {
            return expr();
        }

        static string expr()
        {
            string t = term();
            return term_rest(ref t);
        }

        static string term()
        {
            Token t = match(TipoToken.NUM);
            return t.Valor.ToString();
        }

        static string term_rest(ref string e)
        {
            Token t = match(TipoToken.OP);
            if (t == null)
                return e;
            else if (t.Valor == '+')
            {
                e += ' ' + term() + " +";
                return term_rest(ref e);
            }
            else if (t.Valor == '-')
            {
                e += ' ' + term() + " -";
                return term_rest(ref e);
            }
            else
                return "Error in the conversion";
        }

        static Token match(TipoToken tipo)
        {
            Token t = GetNextToken().Value;
            if (tipo == t.Tipo)
            {
                return t;
            }
            return null;
        }

        static bool Tokenizer(string ElString)
        {
            LaLista.AddLast(new Token(TipoToken.NULL, ' '));

            for (int i = 0; i <= ElString.Length - 1; i++)
            {
                char ElChar = ElString[i];

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
            LaLista.AddLast(new Token(TipoToken.NULL, ' '));
            CurrentToken = LaLista.First;

            return true;
        }

        static LinkedListNode<Token> GetNextToken()
        {
            CurrentToken = CurrentToken.Next;
            return CurrentToken;
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
        NUM, OP, NULL
    }
}