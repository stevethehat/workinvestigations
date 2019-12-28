using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace GoldRepl
{
    class Rule
    {
        public Regex Regex { get; private set; }
        public ConsoleColor Color { get; private set; }
        public Rule(Regex regex, ConsoleColor color)
        {
            Regex = regex;
            Color = color;
        }
    }

    public class SyntaxHighlight
    {
        private static Dictionary<string, ConsoleColor> _colors = new Dictionary<string, ConsoleColor>()
        {
            { "keyword",    ConsoleColor.Blue },
            { "operator",   ConsoleColor.Red },
            { "brace",      ConsoleColor.DarkGray },
            { "defclass",   ConsoleColor.White },
            { "string",     ConsoleColor.Magenta },
            { "string2",    ConsoleColor.DarkMagenta },
            { "comment",    ConsoleColor.DarkGreen },
            { "self",       ConsoleColor.White },
            { "number",     ConsoleColor.DarkYellow },
        };

        private static List<string> _keywords = new List<string>() {
            "and", "assert", "break", "class", "continue", "def",
            "del", "elif", "else", "except", "exec", "finally",
            "for", "from", "global", "if", "import", "in",
            "is", "lambda", "not", "or", "pass", "print",
            "raise", "return", "try", "while", "yield",
            "None", "True", "False"
        };

        private static List<string> _operators = new List<string>()
        {
            "=",
            // Comparison
            "==", "!=", "<", "<=", ">", ">=",
            // Arithmetic
            "\\+", "-", "\\*", "/", "//", "\\%", "\\*\\*",
            // In-place
            "\\+=", "-=", "\\*=", "/=", "\\%=",
            // Bitwise
            "\\^", "\\|", "\\&", "\\~", ">>", "<<",
        };

        private static List<string> _braces = new List<string>()
        {
            "\\{", "\\}", "\\(", "\\)", "\\[", "\\]",
        };

        private readonly List<Rule> _rules = new List<Rule>();

        public SyntaxHighlight()
        {
            //_rules.AddRange(_keywords.Select(k => new Rule(new Regex($"\b{k}\b"), _colors["keyword"])));
            _rules.AddRange(_operators.Select(o => new Rule(new Regex($"^(\\s?{o}\\s?)"), _colors["operator"])));
            //_rules.AddRange(_braces.Select(b => new Rule(new Regex($"{b}"), _colors["brace"])));

            //_rules.Add(new Rule(new Regex("^\\bself\\b"), _colors["self"]));

            // def and class
            //_rules.Add(new Rule(new Regex("^\\bdef\\b\\s*(\\w+)"), _colors["defclass"]));
            //_rules.Add(new Rule(new Regex("^\\bclass\\b\\s*(\\w+)"), _colors["defclass"]));

            // comment
            // From '#' until a newline
            //_rules.Add(new Rule(new Regex("^#[^\\n]*"), _colors["comment"]));

            // numbers
            _rules.Add(new Rule(new Regex("^\\b[+-]?[0-9]+[lL]?\\b?"), _colors["number"]));

            //_rules.Add(new Rule(new Regex("^\\b[+-]?[0-9]+[lL]?\\b"), _colors["number"]));
            //_rules.Add(new Rule(new Regex("^\\b[+-]?0[xX][0-9A-Fa-f]+[lL]?\\b"), _colors["number"]));
            //_rules.Add(new Rule(new Regex("^\\b[+-]?[0-9]+(?:\\.[0-9]+)?(?:[eE][+-]?[0-9]+)?\\b"), _colors["number"]));

            _rules.Add(new Rule(new Regex("^(\\w+\\s?)"), ConsoleColor.Cyan));
            /*
              88
              rules += [
  89             # 'self'
  90             (r'\bself\b', 0, STYLES['self']),
  91 
  92             # Double-quoted string, possibly containing escape sequences
  93             (r'"[^"\\]*(\\.[^"\\]*)*"', 0, STYLES['string']),
  94             # Single-quoted string, possibly containing escape sequences
  95             (r"'[^'\\]*(\\.[^'\\]*)*'", 0, STYLES['string']),
  96 
  97             # 'def' followed by an identifier
  98             (r'\bdef\b\s*(\w+)', 1, STYLES['defclass']),
  99             # 'class' followed by an identifier
 100             (r'\bclass\b\s*(\w+)', 1, STYLES['defclass']),
 101 
 102             # From '#' until a newline
 103             (r'#[^\n]*', 0, STYLES['comment']),
 104 
 105             # Numeric literals
 106             (r'\b[+-]?[0-9]+[lL]?\b', 0, STYLES['numbers']),
 107             (r'\b[+-]?0[xX][0-9A-Fa-f]+[lL]?\b', 0, STYLES['numbers']),
 108             (r'\b[+-]?[0-9]+(?:\.[0-9]+)?(?:[eE][+-]?[0-9]+)?\b', 0, STYLES['numbers']),
 109         ]
            */
        }

        public void WriteToConsole(string code)
        {
            var currentCode = code.Trim();
            while (false == string.IsNullOrWhiteSpace(currentCode))
            {
                foreach (Rule rule in _rules)
                {
                    MatchCollection matchCollection = rule.Regex.Matches(currentCode);
                    var matches = matchCollection.Count;
                    if (matches > 0)
                    {
                        var firstMatch = matchCollection[0];
                        Console.ForegroundColor = rule.Color;
                        Console.Write(firstMatch.Value);
                        currentCode = currentCode.Substring(firstMatch.Value.Length).TrimStart();
                    }
                }
            }
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
