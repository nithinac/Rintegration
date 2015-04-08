using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDotNet;
using System.IO;

namespace RIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            var envPath = Environment.GetEnvironmentVariable("PATH");
            var rBinPath = System.Environment.Is64BitProcess ? @"C:\Program Files\R\R-3.1.2\bin\x64" : @"C:\Program Files\R\R-3.1.2\bin\i386";
            Environment.SetEnvironmentVariable("PATH", envPath + Path.PathSeparator + rBinPath);
            StringWriter builder = new StringWriter();
            var temp = Console.Out;
            Console.SetOut(builder);
            using (var engine = REngine.CreateInstance("RDotNet"))
            {
                engine.Initialize();

                String content = File.ReadAllText(@"C:/Users/Nithin/Desktop/aprioriStub.R");
                content = content.Replace("filePathStub", "C:/Users/Nithin/Desktop/test.csv");
                content = content.Replace("headerStub", "FALSE");
                content = content.Replace("supportStub", "0.1");
                content = content.Replace("confStub", "0.1");
                content = content.Replace("sideStub", "rhs");
                content = content.Replace("valueStub", "V3=High");
                content = content.Replace("sortStub", "confidence");
                content = content.Replace("nStub", "3");
                File.WriteAllText(@"C:/Users/Nithin/Desktop/apriori.R", content);

                //using (var fs = File.OpenRead(@"C:\R-scripts\r-test.R"))
                //{
                //    engine.Evaluate(fs);
                //}
                engine.Evaluate(@"source('C:/Users/Nithin/Desktop/apriori.R')");
                Console.SetOut(temp);
                string output = builder.ToString();
                output = output.Substring(output.IndexOf("lhs"));
                Console.WriteLine(output);
            }
            Console.Read();
        }
    }
}
