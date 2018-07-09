using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
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
            // "C:\\Users\\lambbste\\Documents\\Development\\ibcos\\priceupdates\\priceupdates"
            string[] files = Directory.GetFiles("../../priceupdates/priceupdates", "*.cs", SearchOption.AllDirectories);
            //string[] files = Directory.GetFiles("C:\\Users\\lambbste\\Documents\\Development\\priceupdates\\priceupdates", "*.cs", SearchOption.AllDirectories);
            //string[] files = Directory.GetFiles("C:\\Users\\lambbste\\Documents\\Development\\workinvestigations\\rosyln", "*.cs", SearchOption.AllDirectories);

            foreach (string fileName in files){
                var code = new StreamReader(fileName).ReadToEnd();
                var test = SyntaxFactory.ParseCompilationUnit(code);
                if(test != null){
                    var root = (CompilationUnitSyntax)test; 

                    List<ClassInfo> classes = ClassInfo.FindClasses(root);
                    foreach(ClassInfo classNode in classes){
                        var definition = new {
                            fileName = fileName,
                            classDefinition = classNode.Definition()
                        };

                        Console.WriteLine(JsonConvert.SerializeObject(definition)); 
                    }
                }
            }

            Console.WriteLine("Done");
        }
    }
}
