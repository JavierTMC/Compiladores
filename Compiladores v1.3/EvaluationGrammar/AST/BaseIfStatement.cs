using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvaluationGrammar.AST {
    public class BaseIfStatement : BaseAST {
        private BaseBooleanExpression Condition;
        private BaseAST OnTrue;
        private BaseAST OnFalse;

        protected void SetValues(BaseBooleanExpression condition, BaseAST onTrue, BaseAST onFalse)
        {
            Condition = condition;
            OnTrue = onTrue;
            OnFalse = onFalse;
        }

        public override EvaluationResult Evaluate(Env env)
        {
            string argumento1Temp, argumento2Temp;
            if(Condition.Left.GetType().BaseType == typeof(BaseBinaryExpression))
            {
                Condition.Left.Evaluate(env);
                argumento1Temp = Enviroment2.ReturnCurrentTempVariable();
            }
            else if(Condition.Left.GetType().BaseType == typeof (BaseNumberExpression))
            {
                BaseNumberExpression numberExpression= Condition.Left as BaseNumberExpression;
                argumento1Temp = numberExpression.GetNumber();
            }
            else
            {
                BaseIdentifierExpression identifier = Condition.Left as BaseIdentifierExpression;
                argumento1Temp = identifier.GetIdentifierSymbol();
            }

            //optener argumento 2
            if(Condition.Right.GetType().BaseType == typeof(BaseBinaryExpression))
            {
                Condition.Right.Evaluate(env);
                argumento2Temp = Enviroment2.ReturnCurrentTempVariable();
            }
            else if(Condition.Right.GetType().BaseType == typeof (BaseNumberExpression))
            {
                BaseNumberExpression numberExpression= Condition.Right as BaseNumberExpression;
                argumento2Temp = numberExpression.GetNumber();
            }
            else
            {
                BaseIdentifierExpression identifier = Condition.Right as BaseIdentifierExpression;
                argumento2Temp = identifier.GetIdentifierSymbol();
            }

            //crear ontrue para generar siguiente instruccion
            string Operator = "if" + Condition.RELOP;
            Enviroment2.QTable.Add(new Quadruple(Operator, argumento1Temp, argumento2Temp, (Enviroment2.QTable.Count + 2).ToString()));
            Enviroment2.QTable.Add(new Quadruple("goto", null, null, null));
            int indexOfGoto = Enviroment2.QTable.Count - 1; // guardar la posicion del goto (else,final);
            // generar ontrue
            OnTrue.Evaluate(env);
            if(OnFalse == null)
            Enviroment2.QTable[indexOfGoto].result = (Enviroment2.QTable.Count.ToString());// + 1).ToString()

            if(OnFalse != null)
            {
                Enviroment2.QTable[indexOfGoto].result = (Enviroment2.QTable.Count + 1).ToString();// + 1).ToString()
                Enviroment2.QTable.Add(new Quadruple("goto", null, null, null));
                int indexOfGoto2 = Enviroment2.QTable.Count-1;
                OnFalse.Evaluate(env);
                Enviroment2.QTable[indexOfGoto2].result=Enviroment2.QTable.Count.ToString();
            }

            return null;
        }
    }
}