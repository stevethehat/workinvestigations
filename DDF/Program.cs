using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;


namespace DDF
{
    class Program
    {
        static void Main(string[] args)
        {
            ConvertDDF("deprec");
            //ConvertDDF("prerec");
            //ConvertDDF("pmfrec");
            //ConvertDDF("pcdrec");
            //ConvertDDF("pcgrec");
            //ConvertDDF("ctfrec");
            //ConvertDDF("cmfrec");
            //ConvertDDF("ivtrec");
            //ConvertDDF("vatrec");
            //ConvertDDF("pihrec");
            //ConvertDDF("pilrec");
            //ConvertDDF("pohrec");
            //ConvertDDF("polrec");
            //ConvertDDF("powrec");

            Console.WriteLine("Hello World!");
        }

        protected static void ConvertDDF(string ddfType){
            DDFFile ddfFile = new DDFFile("structure", ddfType);
            //DDFFile ddfFile = new DDFFile("template", "tbaseaddressline");
            var structure = ddfFile.Parse();

            string outputFile = Path.Combine("output", $"{ddfType}.json");
            string json = JsonConvert.SerializeObject(structure, Formatting.Indented);
            
            Console.WriteLine(json);

            if(File.Exists(outputFile)){
                File.Delete(outputFile);
            }
            System.IO.File.WriteAllText(outputFile, json);
        }
    }
}
