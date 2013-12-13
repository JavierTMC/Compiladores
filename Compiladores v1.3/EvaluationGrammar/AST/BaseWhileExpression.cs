using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationGrammar.AST
{
    public abstract class BaseWhileExpression : BaseAST
    {
        private BaseBooleanExpression Condition;
        private BaseStatementList block;

        protected void SetValues(BaseBooleanExpression Condition, BaseStatementList block)
        {
            this.Condition = Condition;
            this.block = block;
        }

        public override EvaluationResult Evaluate(Env env)
        {

            string argumento1Temp, argumento2Temp;
            argumento1Temp = ObtenerArgumentoTempUno(env);
            argumento2Temp = ObtenerArgumentoTempDos(env);

            string Operator = "if" + Condition.RELOP;
            Enviroment2.QTable.Add(new Quadruple(Operator, argumento1Temp, argumento2Temp, (Enviroment2.QTable.Count + 2).ToString()));
            int indexOfWhile = (Enviroment2.QTable.Count - 1);
            Enviroment2.QTable.Add(new Quadruple("goto", null, null, null));
            int indexOfGoto = (Enviroment2.QTable.Count - 1);

            // evaluar el blockstatement 
            block.Evaluate(env);
            Enviroment2.QTable.Add(new Quadruple("goto", null, null, indexOfWhile.ToString()));
            int indexOfEnd = Enviroment2.QTable.Count;
            Enviroment2.QTable[indexOfGoto].result = indexOfEnd.ToString();

            return null;

        }
        private string ObtenerArgumentoTempDos(Env env)
        {
            string temp = string.Empty;
            //optener argumento 2
            if (Condition.Right.GetType().BaseType == typeof(BaseBinaryExpression))
            {
                Condition.Right.Evaluate(env);
                temp = Enviroment2.ReturnCurrentTempVariable();
            }
            else if (Condition.Right.GetType().BaseType == typeof(BaseNumberExpression))
            {
                BaseNumberExpression numberExpression = Condition.Right as BaseNumberExpression;
                temp = numberExpression.GetNumber();
            }
            else
            {
                BaseIdentifierExpression identifier = Condition.Right as BaseIdentifierExpression;
                temp = identifier.GetIdentifierSymbol();
            }

            return temp;
        }

        private string ObtenerArgumentoTempUno(Env env)
        {
            string temp = string.Empty;
            if (Condition.Left.GetType().BaseType == typeof(BaseBinaryExpression))
            {
                Condition.Left.Evaluate(env);
                temp = Enviroment2.ReturnCurrentTempVariable();
            }
            else if (Condition.Left.GetType().BaseType == typeof(BaseNumberExpression))
            {
                BaseNumberExpression numberExpression = Condition.Left as BaseNumberExpression;
                temp = numberExpression.GetNumber();
            }
            else
            {
                BaseIdentifierExpression identifier = Condition.Left as BaseIdentifierExpression;
                temp = identifier.GetIdentifierSymbol();
            }

            return temp;
        }
    }
}
