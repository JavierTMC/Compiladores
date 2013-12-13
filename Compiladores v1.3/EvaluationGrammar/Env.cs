using EvaluationGrammar.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EvaluationGrammar
{
    [DataContract]
    public class Env
    {
        [DataMember]
        public Dictionary<string, string> variables;
        
        public Env()
        {
            variables = new Dictionary<string, string>();
        }

        public string GetValue(string variable)
        {
            try
            {
                string valueOfVar = variables[variable];
                if (valueOfVar == null)
                {
                    throw new UninitializedVariableUseException(variable);
                }
                return valueOfVar;
            }
            catch (KeyNotFoundException)
            {
                throw new UndefinedVariableException(variable);
            }
        }

        public void DefineVariable(string variable)
        {
            if (variables.ContainsKey(variable))
            {
                throw new VariableRedefinitionException(variable);
            }
            variables[variable] = null;
        }

        public void AssignValue(string variable, string value)
        {
            if (!variables.ContainsKey(variable))
            {
                throw new UndefinedVariableException(variable);
            }
            variables[variable] = value;
        }
    }
}