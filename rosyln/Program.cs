using System;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Rosyln
{
    class Parser{
        protected SyntaxNode _node;
        protected string _indent;
        public Parser(SyntaxNode node, string indent){
            _node = node;
            _indent = indent;
        }

        public string GetName(SyntaxNode node){
            string name = null;

            if(GetType(node) == "IdentifierNameSyntax"){
                name = node.ToString();
            } else {
                foreach(SyntaxNode childNode in node.ChildNodes()){
                    name = GetName(childNode);

                    if(name != null){
                        break;
                    }
                }
            }

            return name;
        }
        public void Parse(){
            /*
            switch(GetType(_node)){
                case "NamespaceDeclarationSyntax":
                    Console.WriteLine($"\n namespace = {GetName(_node)}\n");
                    break;
                case "ClassDeclarationSyntax":
                    Console.WriteLine($"\nclass = {GetName(_node)}\n");
                    break;

                default:
                    Console.WriteLine($"\n{GetName(_node)}\n");
                    break;
            }
            */
            if(_node.ToString().IndexOf(" ") == -1){
                Console.WriteLine($"{_indent}{GetType(_node)} {_node.ToString()}");
            } else {
                //Console.WriteLine($"{_indent}{GetType(_node)}");
                Console.WriteLine($"{_indent}{GetType(_node)} {_node.ToString()}");
            }

            foreach(var childNode in _node.ChildNodes()){
                Parser childParser = new Parser(childNode, _indent + "  ");
                childParser.Parse();
            }
        }

        protected string GetType(SyntaxNode node){
            string fullType = node.GetType().ToString();
            return fullType.Substring(fullType.LastIndexOf(".") +1);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var code = new StreamReader("/Users/stevelamb/Development/ibcos/investigations/rosyln/basic.cs").ReadToEnd();
            //CompilationUnitSystax test = SyntaxFactory.ParseCompilationUnit(code);
            var test = SyntaxFactory.ParseCompilationUnit(code);
            var root = (CompilationUnitSyntax)test; 



            /*
            foreach(SyntaxNode node in root.ChildNodes()){
                Parser nodeParser = new Parser(node, "");
                nodeParser.Parse();
            }
            //SyntaxTree tree = SyntaxTree.ParseCompilationUnit(code);
            */
            var classNode = ClassInfo.FindClass(root);

            Console.WriteLine(classNode);
            Console.WriteLine(classNode.Name());
            Console.WriteLine(classNode.Comment());
            Console.WriteLine(classNode.Document());
            
            Console.WriteLine("Done");
        }
    }
}
