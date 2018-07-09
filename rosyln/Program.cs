using System;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Newtonsoft.Json;


namespace Rosyln
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //string[] files = Directory.GetFiles(".", "*.cs");
            string[] files = Directory.GetFiles("../api", "*.cs", SearchOption.AllDirectories);

            foreach(string fileName in files){
                var code = new StreamReader(fileName).ReadToEnd();
                var test = SyntaxFactory.ParseCompilationUnit(code);
                var root = (CompilationUnitSyntax)test; 

                var classNode = ClassInfo.FindClass(root);

                /*
                var definition = new {
                    fileName = fileName,
                    class = classNode.Definition()
                };
                */
                var definition = new {
                    fileName = fileName,
                    classDefinition = classNode.Definition()
                };

                Console.WriteLine(JsonConvert.SerializeObject(definition));
            }

            Console.WriteLine("Done");
        }
    }
}
