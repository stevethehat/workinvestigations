using System;
using Mono.Terminal;

namespace isamexplore
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creates a line editor, and sets the name of the editing session to
            // "foo".  This is used to save the history of input entered
            LineEditor le = new LineEditor ("foo") {
                HeuristicsMode = "csharp"
            };

            // Configures auto-completion, in this example, the result
            // is always to offer the numbers as completions
            le.AutoCompleteEvent += delegate (string text, int pos){
                string prefix = "";
                var completions = new string [] { 
                    "One", "Two", "Three", "Four", "Five", 
                    "Six", "Seven", "Eight", "Nine", "Ten" };
                return new Mono.Terminal.LineEditor.Completion (prefix, completions);
            };

            string s;

            // Prompts the user for input
            while ((s = le.Edit ("shell> ", "")) != "q"){
                Console.WriteLine ("Your Input: [{0}]", s);
            }
        }
    }
}
