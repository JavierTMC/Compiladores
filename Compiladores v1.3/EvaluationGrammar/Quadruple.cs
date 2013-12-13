using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace EvaluationGrammar
{
    [DataContract]
    public class Quadruple
    {
        public int Numero { get; set; }
        [DataMember(Name = "operator",Order=0)]
        public string Operador { get; set; }
        [DataMember(Order=2)]
        public object argument1 { get; set; }
        [DataMember(Order=3)]
        public object argument2 { get; set; }
        [DataMember(Order=4)]
        public string result { get; set; }

        public Quadruple(string operador, object argumento1, object argumento2, string resultado)
        {
            Numero = Enviroment2.NumeroInstruccion++;
            Operador = operador;
            argument1 = argumento1;
            argument2 = argumento2;
            result = resultado;
        }
    }
}
