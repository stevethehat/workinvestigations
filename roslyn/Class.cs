using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Rosyln{
    public class BaseInfo{
        public string Name;
        public string Comment;
        protected SyntaxNode _node;
        protected void GetComment(){
            StringBuilder comment = new StringBuilder();
            SyntaxTriviaList trivia = _node.GetLeadingTrivia();
            var commentNodes = _node.DescendantNodes().OfType<DocumentationCommentTriviaSyntax>();
            foreach(var commentNode in trivia){
                //if(commentNode.GetType() != WhitespaceTrivia){
                comment.AppendLine(commentNode.ToString());
                //commentNode.GetStructure()
                //}
            }

            Comment = comment.ToString();
        }
    }

    public class MethodInfo: BaseInfo{
        public MethodInfo(MethodDeclarationSyntax node){
            _node = node;
            Name = node.Identifier.Text;
            GetComment();
        }
    }

    public class FieldInfo: BaseInfo{
        public FieldInfo(VariableDeclaratorSyntax node){
            _node = node;
            Name = node.Identifier.Text;
            GetComment();
        }
    }

    public class ClassInfo: BaseInfo{
        protected readonly ClassDeclarationSyntax _classNode;
        public ClassInfo(SyntaxNode classNode){
            _classNode = (ClassDeclarationSyntax)classNode;
            _node = classNode;
            Name = _classNode.Identifier.Text;
            GetComment();
        }

        public static List<ClassInfo> FindClasses(SyntaxNode root){
            List<ClassInfo> result = new List<ClassInfo>();
            foreach(ClassDeclarationSyntax classNode in root.DescendantNodes().OfType<ClassDeclarationSyntax>()){
                result.Add(new ClassInfo(classNode));
            }
            return result;
        }

        public object Definition(){
            var methods = Methods();
            var fields = Fields();
            var definition = new {
                name = Name,
                methods = methods,
                fields = fields,
                all = All()
            };

            return definition;
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

        protected List<BaseInfo> All(){
            List<BaseInfo> result = new List<BaseInfo>();
            foreach(var field in _classNode.DescendantNodes().OfType<BaseTypeDeclarationSyntax>()){
                result.Add(new BaseInfo());
            }

            return result;
        }

    }
}
    
