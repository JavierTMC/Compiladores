using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace EvaluationGrammar
{
    [DataContract]
    public class jaja
    {
        [DataMember]
        List<Quadruple> code;

        public jaja(List<Quadruple> code)
        {
            this.code = code;
        }
    }
}
