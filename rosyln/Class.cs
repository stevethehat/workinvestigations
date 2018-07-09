using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;


namespace Rosyln{
    public class BaseInfo{
        public string Name;

        /*
        public BaseInfo(SyntaxNode node){
            Name = node.Identifier.Text;
        }
        */
    }

    public class MethodInfo: BaseInfo{
        public MethodInfo(MethodDeclarationSyntax node){
            Name = node.Identifier.Text;
        }
    }

    public class FieldInfo: BaseInfo{
        public FieldInfo(VariableDeclaratorSyntax node){
            Name = node.Identifier.Text;
        }
    }

    public class ClassInfo{
        protected readonly ClassDeclarationSyntax _classNode;
        public ClassInfo(SyntaxNode classNode){
            _classNode = (ClassDeclarationSyntax)classNode;
        }

        public static ClassInfo FindClass(SyntaxNode root){
            ClassDeclarationSyntax classNode = root.DescendantNodes().OfType<ClassDeclarationSyntax>().FirstOrDefault();
            return new ClassInfo(classNode);
        }

        public string Document(){
            var methods = Methods();
            var fields = Fields();
            var definition = new {
                name = Name(),
                methods = methods,
                fields = fields
            };

            return JsonConvert.SerializeObject(definition);
        }

        protected List<MethodInfo> Methods(){
            List<MethodInfo> result = new List<MethodInfo>();
            foreach(var method in _classNode.DescendantNodes().OfType<MethodDeclarationSyntax>()){
                result.Add(new MethodInfo(method));
            }

            return result;
        }

        protected List<FieldInfo> Fields(){
            List<FieldInfo> result = new List<FieldInfo>();
            foreach(var fields in _classNode.DescendantNodes().OfType<FieldDeclarationSyntax>()){
                foreach(var field in fields.Declaration.Variables){
                    result.Add(new FieldInfo(field));
                }
            }

            return result;
        }

        public string Name(){
            return _classNode.Identifier.Text;
        }

        public string Comment(){
            SyntaxTriviaList trivia = _classNode.GetLeadingTrivia();
            var commentNode = _classNode.DescendantNodes().OfType<DocumentationCommentTriviaSyntax>().FirstOrDefault();
            if(commentNode != null){
                return commentNode.GetText().ToString();
            } else {
                return "";
            }
        }
    }
}
    
