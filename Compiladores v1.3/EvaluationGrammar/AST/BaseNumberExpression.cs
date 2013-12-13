using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvaluationGrammar.AST {
    public abstract class BaseNumberExpression : Expression {
        private int value;

        public override EvaluationResult Evaluate(Env env)
        {
            return new EvaluationResult {
                Result = value, Type="int"
            };
        }

        protected void SetValue(int value)
        {
            this.value = value;
        }

        //segunda opcion - 
        public string GetNumber()
        {
            return value.ToString();
        }

    }
}