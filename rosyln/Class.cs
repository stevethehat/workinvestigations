using System.Linq;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Rosyln{
    public class Class{
        protected readonly ClassDeclarationSyntax _classNode;
        public Class(SyntaxNode classNode){
            _classNode = (ClassDeclarationSyntax)classNode;
        }

        public static Class FindClass(SyntaxNode root){
            ClassDeclarationSyntax classNode = root.DescendantNodes().OfType<ClassDeclarationSyntax>().FirstOrDefault();
            return new Class(classNode);
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
    
