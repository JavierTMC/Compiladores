using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntlrParser.AST;
using EvaluationGrammar;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Net;
using System.Web;

namespace Compiladores
{
    class Program
    {
        static void Main(string[] args)
        {
            #region QTable
            Console.WriteLine("Insert Expression: ");
            string expression = Console.ReadLine();
            Parser parser = new Parser();
            var lollipop = parser.Parse(expression) as BlockStatement;
            Env ambiente = new Env();
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(jaja));
            DataContractJsonSerializer ser2 = new DataContractJsonSerializer(typeof(Dictionary<string, string>));

            foreach (var item in lollipop.children)
            {
                item.Evaluate(ambiente);
            }

            //code
            jaja eljaja = new jaja(Enviroment2.QTable);
            ser.WriteObject(stream, eljaja);
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            string resultstring = sr.ReadToEnd();
            resultstring = resultstring.Substring(0, resultstring.Length - 1) + ", \"env\":{";
                
            foreach (var item in ambiente.variables)
            {
               if(item.Key[0]=='T')
                   resultstring += string.Format("{7}{0}{7}:{1}{7}{2}{7}:{7}{3}{7},{7}{4}{7}:{5}{6}{8}", item.Key, "{", "type", "int", "temp", "true", "}", "\"", ",");
               else
                   resultstring += string.Format("{7}{0}{7}:{1}{7}{2}{7}:{7}{3}{7},{7}{4}{7}:{5}{6}{8}", item.Key, "{", "type", "int", "temp", "false", "}", "\"", ",");
            }

            resultstring = resultstring.Substring(0, resultstring.Length - 1);
            resultstring += "}}";
            Console.WriteLine(resultstring);
            #endregion

            #region JSON
            var resultado= PostJSON(resultstring);
            Console.WriteLine(resultado);
            Console.ReadKey(true);
            #endregion
        }

        private static string PostJSON(string json)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://webcompiler.herokuapp.com/compile");
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            //var result = null;// = string.Empty;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                //return httpResponse.GetResponseStream();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }
            }
            //return result;
        }
    }
}
