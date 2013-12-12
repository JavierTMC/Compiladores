using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Tokens
{
    class Program
    {
        static List<Token> LaLista = new List<Token>();

        static void Main(string[] args)
        {

            string input;
            Console.WriteLine("Input: ");
            input = Console.ReadLine();

            Tokenizer(input.Replace(" ",string.Empty));

            foreach (var token in LaLista)
            {
                if(token.Tipo != TipoToken.NULL)
                Console.WriteLine("Tipo:{0}\tValor:{1}",token.Tipo,token.Valor.ToString());
            }

            Console.ReadKey(true);

        }

        static void Tokenizer(string ElString)
        {

            for (int i = 0; i <= ElString.Length - 1; i++)
            {
                String ElChar = ElString[i].ToString();

                if (ElChar == "+" || ElChar == "-" || ElChar == "*" || ElChar == "/" || ElChar == "=")
                {
                    if(ElString[i+1].ToString() == "=")
                    {
                        LaLista.Add(new Token(TipoToken.RELOP, "=="));
                        i++;
                    }
                    else
                        LaLista.Add(new Token(TipoToken.OP, ElChar));
                }
                else if (ElChar == ">" || ElChar == "<")
                {
                    if (ElString[i + 1].ToString() == "=")
                    {
                        ElChar = ElString[i].ToString() + "=";
                        i++;
                    }
                    LaLista.Add(new Token(TipoToken.RELOP, ElChar));
                }
                else if (ElChar == "!" && ElString[i + 1].ToString() == "=")
                {
                    LaLista.Add(new Token(TipoToken.RELOP, ElChar + ElString[i + 1].ToString()));
                    i++;
                }
                else if (ElChar == "(")
                    LaLista.Add(new Token(TipoToken.OPEN_PARENS, ElChar));
                else if (ElChar == ")")
                    LaLista.Add(new Token(TipoToken.CLOSING_PARENS, ElChar));
                else if (Regex.IsMatch(ElChar.ToString(), @"[0-9]"))
                    LaLista.Add(new Token(TipoToken.NUM, ElChar));
                else if (Regex.IsMatch(ElChar.ToString(), @"[a-z A-Z ? _ ! # $ % ^ & ` ~ ]"))
                {
                    if((ElChar == "w" && ElString.Length >=5) || (ElChar == "i" && ElString.Length >=2 ))
                    {
                        if(handleSpecialToken(ElChar, ElString,i))
                        {
                            if(ElChar == "i")
                            {
                                LaLista.Add(new Token(TipoToken.IF, "if"));
                                i++;
                            }
                            else
                            {
                                LaLista.Add(new Token(TipoToken.WHILE, "while"));
                                i = i+4;
                            }
                        }
                        else
                            LaLista.Add(new Token(TipoToken.SYMBOL, ElChar));
                    }
                    else
                        LaLista.Add(new Token(TipoToken.SYMBOL, ElChar));

                }
            }// end for
        }

        private static bool handleSpecialToken(string letter, string ElString,int position)
        {
            if (letter == "i")
            {
                if (ElString[position + 1].ToString() == "f")
                    return true;
            }
            else

                if (ElString[position + 1].ToString() == "h" && ElString[position + 2].ToString() == "i"
                    && ElString[position + 3].ToString() == "l" && ElString[position + 4].ToString() == "e")
                    return true;
            return false;
        }

       
    class Token
    {
        public TipoToken Tipo { get; set; }
        public string Valor { get; set; }

        public Token(TipoToken tipo, string valor)
        {
            Tipo = tipo;
            Valor = valor;
        }
    }

    enum TipoToken
    {
        NUM, OP, NULL, OPEN_PARENS, CLOSING_PARENS, SYMBOL, RELOP, WHILE, IF
    }
    }
}
