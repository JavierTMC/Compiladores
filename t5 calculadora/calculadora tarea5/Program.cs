using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace calculadora_tarea5
{
    class Program
    {

        static LinkedList<Token> LaLista = new LinkedList<Token>();
        static LinkedListNode<Token> CurrentToken = null;


        static void Main(string[] args)
        {

            string input;
            Console.WriteLine("Calculadora");
            Console.Write("Operacion Infix: ");
            input = Console.ReadLine();

            Tokenizer(input);
            string postfix = expr();
            Console.WriteLine("\n conversion Postfix = {0}", postfix);
            LaLista.Clear();

            #region AST
            Tokenizer(postfix.Replace(" ", string.Empty));
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
                else if (ElToken.Tipo == TipoToken.OPEN_PARENS || ElToken.Tipo == TipoToken.CLOSING_PARENS)
                    continue;
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

                if (ElChar == '+' || ElChar == '-' || ElChar == '*' || ElChar == '/')
                    LaLista.AddLast(new Token(TipoToken.OP, ElChar));
                else if (ElChar == '(')
                    LaLista.AddLast(new Token(TipoToken.OPEN_PARENS, ElChar));
                else if (ElChar == ')')
                    LaLista.AddLast(new Token(TipoToken.CLOSING_PARENS, ElChar));
                else if (Regex.IsMatch(ElChar.ToString(), @"[0-9]"))
                    LaLista.AddLast(new Token(TipoToken.NUM, ElChar));
                else return false;
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

        static LinkedListNode<Token> FallBack()
        {
            CurrentToken = CurrentToken.Previous;
            return CurrentToken;
        }

        //expr -> term term_rest
        //term -> factor factor_rest
        //term_rest -> +term || - term || epsilon
        //factor -> num || (expr)
        // factor_rest -> * term || / term || epsilon
        //num -> 0..9

        static string expr()
        {
            string e = term();
            return term_rest(ref e);
        }

        static string term()
        {
            string e2 = factor();
            string fr = factor_rest(ref e2);
            if (fr == "epsilon")
            {
                FallBack();
                return e2; 
            }
            else return fr;
            
        }

        static string term_rest(ref string e)
        {
            Token t = Match(TipoToken.OP, false);

            if (t == null)
            {
                FallBack();
                return e;
            }
            else if (t.Valor == '+')
            {
                return e += ' ' + term() + " +";
            }
            else if (t.Valor == '-')
            {
                return e += ' ' + term() + " -";
            }
            else return "Error";
        }

        static string factor()
        {
            Token token = Match(TipoToken.NUM,true);

            if (token == null)
            { FallBack(); return ""; }
            if (token.Tipo == TipoToken.NUM)
                return token.Valor.ToString();
            else if (token.Tipo == TipoToken.OPEN_PARENS)
            {
                string parentesis = expr();
                //FallBack();
                Token t2 = GetNextToken().Value;
                if (t2.Tipo == TipoToken.CLOSING_PARENS)
                {
                    //FallBack();
                    return parentesis;
                }
                else return "Error";
            }
            else return "Error";

        }

        static string factor_rest(ref string e2)
        {
            Token token = Match(TipoToken.OP, false);

            if (token == null)
            {
                FallBack();
                return e2;
            }
            else if (token.Valor == '*')
            {
                e2 += ' ' + factor() + " *";
                return factor_rest(ref e2);
            }
            else if (token.Valor == '/')
            {
                e2 += ' ' + factor() + " /";
                return factor_rest(ref e2); ;
            }
            else return "epsilon";
        }

        static Token Match(TipoToken tipo, bool parentesis)
        {
            Token token = GetNextToken().Value;
            if (token.Tipo == tipo && parentesis == false)
                return token;
            else if (parentesis == true && (token.Tipo == TipoToken.OPEN_PARENS || token.Tipo == TipoToken.NUM))
                return token;
            else
                return null;
        }
    }

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
        NUM, OP, NULL, OPEN_PARENS, CLOSING_PARENS
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
            else if (base.token.Valor == '*')
            {
                return MultOperation(Left, Right);
            }
            else if (base.token.Valor == '/')
            {
                return DivOperation(Left, Right);
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
        public int MultOperation(Expression l, Expression r)
        {
            return l.eval() * r.eval();
        }
        public int DivOperation(Expression l, Expression r)
        {
            return l.eval() / r.eval();
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
