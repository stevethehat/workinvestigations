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
            string ddfType = "ctfrec";
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


            Console.WriteLine("Hello World!");
        }
    }
}
