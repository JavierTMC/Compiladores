﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvaluationGrammar.AST
{
    public abstract class BaseIdentifierExpression : Expression
    {
        private string variable;

        protected void SetValue(string variable)
        {
            this.variable = variable;
        }

        public override EvaluationResult Evaluate(Env env)
        {
            return new EvaluationResult {
                Result = env.GetValue(variable)
            };
        }

        public string GetIdentifierSymbol()
        {
            return variable;
        }

        public void AssignValue(Env env, EvaluationResult evaluationResult)
        {
            env.AssignValue(variable, evaluationResult.Result.ToString());
        }
    }
}