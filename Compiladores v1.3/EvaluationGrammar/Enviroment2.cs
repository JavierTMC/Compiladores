using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationGrammar
{
    public static class Enviroment2
    {
        public static int NumeroInstruccion = 0;
        public static List<Quadruple> QTable = new List<Quadruple>();
        private static string temp = string.Empty;

        public static string CreateTempVariable(Env env)
        {
            temp = "T" + QTable.Count.ToString();
            env.DefineVariable(temp);
            env.AssignValue(temp, null);
            return temp;
        }

        public static string ReturnCurrentTempVariable()
        {
            return temp;
        }

    }
}
