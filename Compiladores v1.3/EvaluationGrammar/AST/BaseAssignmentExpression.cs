using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EvaluationGrammar;


namespace EvaluationGrammar.AST
{
    public abstract class BaseAssignmentExpression : BaseAST
    {
        private BaseIdentifierExpression identifier;
        private Expression assignment;

        protected void SetValues(BaseIdentifierExpression identifier, Expression assignment)
        {
            this.identifier = identifier;
            this.assignment = assignment;
        }

        public override EvaluationResult Evaluate(Env env)
        {
            //identifier.AssignValue(env, assignment.Evaluate(env));

            if (assignment.GetType().BaseType == typeof(BaseNumberExpression))
            {
                Enviroment2.QTable.Add(new Quadruple("=", assignment.Evaluate(env).Result, null, identifier.GetIdentifierSymbol()));
            }

            else
            {
                Enviroment2.QTable.Add(new Quadruple("=", Enviroment2.ReturnCurrentTempVariable(), null, identifier.GetIdentifierSymbol()));
            }
            return null;
        }

    }
}