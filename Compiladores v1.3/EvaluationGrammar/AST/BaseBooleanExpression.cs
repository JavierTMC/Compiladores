using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvaluationGrammar.AST {
    public abstract class BaseBooleanExpression : BaseAST {
        public Expression Left, Right;
        public string RELOP;

        protected void SetValues(Expression left, Expression right, string relop)
        {
            Left = left;
            Right = right;
            RELOP = relop;
        }

        public override EvaluationResult Evaluate(Env env)
        {
            int left = (int)Left.Evaluate(env).Result;
            int right = (int)Right.Evaluate(env).Result;
            bool result;
            switch (RELOP) {
                case "==":
                    result = left == right;
                    break;
                case "!=":
                    result = left != right;
                    break;
                case ">":
                    result = left > right;
                    break;
                case "<":
                    result = left < right;
                    break;
                case ">=":
                    result = left >= right;
                    break;
                case "<=":
                    result = left <= right;
                    break;
                default:
                    throw new InvalidOperationException("Unknown operator: " + RELOP);
            }
            return new EvaluationResult {
                Result = result
            };
        }

    }
}