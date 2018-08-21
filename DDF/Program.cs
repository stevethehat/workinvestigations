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
            DDFFile ddfFile = new DDFFile("structure", "pmfrec");
            //DDFFile ddfFile = new DDFFile("template", "tbaseaddressline");
            var structure = ddfFile.Parse();
            Console.WriteLine(JsonConvert.SerializeObject(structure, Formatting.Indented));

            Console.WriteLine("Hello World!");
        }
    }
}
