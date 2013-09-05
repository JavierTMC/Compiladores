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

        static void Main(string[] args)
        {
            Console.WriteLine("Calculador Postfix");

            string input = string.Empty;
            //Introducir datos
            Console.Write("\nOperación: ");
            input = Console.ReadLine();

            Tokenizer(input);

            string postfix = expr().Replace(" ", string.Empty);
            LaLista.Clear();

            Tokenizer(postfix);

            #region ConstruirAST
            Expression AST = null;

            Stack<Expression> LaPila = new Stack<Expression>();

            foreach (Token ElToken in LaLista)
            {
                //El token es numero
                if (ElToken.Tipo == TipoToken.NUM)
                {
                    LaPila.Push(new UnaryExpression(ElToken));
                }

                //El token es operador
                else if (ElToken.Tipo == TipoToken.OP)
                {
                    Expression Pop1 = LaPila.Pop();
                    Expression Pop2 = LaPila.Pop();
                    Expression NuevoArbol = new BinaryExpression(ElToken, Pop2, Pop1);
                    LaPila.Push(NuevoArbol);
                }
            }
            AST = LaPila.Pop();
            Console.WriteLine("\nEl resultado es: " + AST.eval());
            #endregion

            Console.ReadKey(true);
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

        #region Postfix
        static string expr()
        {
            string t = term();
            return term_rest(ref t);
        }

        static string term()
        {
            Token t = Match(TipoToken.NUM);
            return t.Valor.ToString();
        }

        static string term_rest(ref string e)
        {
            Token t = Match(TipoToken.OP);
            if (t == null)
            {
                return e;
            }
            else if (t.Valor == '+')
            {
                e += ' ' + term() + "+";
                return term_rest(ref e);
            }
            else if (t.Valor == '-')
            {
                e += ' ' + term() + "-";
                return term_rest(ref e);
            }
            else
                return "Error";
        }

        static Token Match(TipoToken tipo)
        {
            Token t = GetNextToken().Value;
            if (tipo == t.Tipo)
            {
                return t;
            }
            return null;
        }
        #endregion
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

    abstract class Expression
    {
        public Token token;

        public virtual int eval() { return 0; }
    }

    class BinaryExpression : Expression
    {
        public Expression Left { get; set; }
        public Expression Right { get; set; }

        public override int eval()
        {
            if (base.token.Valor == '+')
            {
                return SumOperation(Left, Right);
            }
            else if (base.token.Valor == '-')
            {
                return DiffOperation(Left, Right);
            }
            else
                return base.eval();
        }

        public BinaryExpression(Token t, Expression left, Expression right)
        {
            base.token = t;
            Left = left;
            Right = right;
        }

        #region Operaciones
        public int SumOperation(Expression l, Expression r)
        {
            return l.eval() + r.eval();
        }

        public int DiffOperation(Expression l, Expression r)
        {
            return l.eval() - r.eval();
        }
        #endregion
    }

    class UnaryExpression : Expression
    {
        //Constructor
        public UnaryExpression(Token T)
        {
            base.token = T;
        }

        //metodo override
        public override int eval()
        {
            return ConstantExpression();
        }

        //metodo
        int ConstantExpression()
        {
            return int.Parse(base.token.Valor.ToString());
        }
    }
}