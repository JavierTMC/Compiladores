using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvaluationGrammar.AST
{
    public abstract class BaseInitializationStatement : BaseAST
    {
        private BaseIdentifierExpression identifier;
        private Expression initialization;

        protected void SetValues(BaseIdentifierExpression identifier, Expression initialization)
        {
            this.identifier = identifier;
            this.initialization = initialization;
        }

        public override EvaluationResult Evaluate(Env env)
        {
            string value = string.Empty;

            if (initialization.GetType().BaseType == typeof(BaseBinaryExpression))
            {
                initialization.Evaluate(env);
                value = Enviroment2.ReturnCurrentTempVariable();
            }

            else if (initialization.GetType().BaseType == typeof(BaseNumberExpression))
            {
                BaseNumberExpression numberExpression = initialization as BaseNumberExpression;
                value = numberExpression.GetNumber();
            }

            else
            {
                BaseIdentifierExpression identifierExpression = initialization as BaseIdentifierExpression;
                value = identifierExpression.GetIdentifierSymbol();
            }

            var symbol = identifier.GetIdentifierSymbol();
            env.DefineVariable(symbol);
            env.AssignValue(symbol, value);
            Enviroment2.QTable.Add(new Quadruple("=", value, null, symbol));
            return null;
        }

    }
}