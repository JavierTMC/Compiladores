using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvaluationGrammar
{
    public class EvaluationResult
    {
        public object Result { get; set; }
        public string Type { get; set; }

        public EvaluationResult() { Type = "variable"; }
    }
}