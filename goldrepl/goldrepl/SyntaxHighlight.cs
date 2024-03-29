﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using Microsoft.Scripting.Hosting;

namespace GoldRepl
{
    class Rule
    {
        public Regex Regex { get; private set; }
        public ConsoleColor Color { get; private set; }
        public TokenType TokenType { get; private set; }
        public Func<List<Token>, bool> Test { get; private set; }
        public Rule(string regex, TokenType tokenType, Func<List<Token>, bool> test = default(Func<List<Token>, bool>))
        {
            Regex = new Regex(regex);
            TokenType = tokenType;
            Color = TokenColors.Colors[TokenType];
            if(default(Func<List<Token>, bool>) != test)
            {
                Test = test;
            }
            else
            {
                Test = l => true;
            }
        }
    }

    public enum TokenType
    {
        Keyword, Operator, Brace, DefClass, String, String2, Comment, Self, Number, Variable, Method
    }

    public class TokenColors
    {
        public static Dictionary<TokenType, ConsoleColor> Colors = new Dictionary<TokenType, ConsoleColor>()
        {
            { TokenType.Keyword,    ConsoleColor.Blue },
            { TokenType.Operator,   ConsoleColor.Red },
            { TokenType.Brace,      ConsoleColor.DarkGray },
            { TokenType.DefClass,   ConsoleColor.White },
            { TokenType.String,     ConsoleColor.Magenta },
            { TokenType.String2,    ConsoleColor.DarkMagenta },
            { TokenType.Comment,    ConsoleColor.DarkGreen },
            { TokenType.Self,       ConsoleColor.White },
            { TokenType.Number,     ConsoleColor.DarkYellow },
            { TokenType.Variable,   ConsoleColor.Cyan },
            { TokenType.Method,     ConsoleColor.Yellow },
        };
    }

    public class Token
    {
        public static ScriptScope Scope { get; set; }
        public static List<Token> CurrentTokens { get; set; }
        public TokenType TokenType { get; private set; }
        public string Value { get; private set; }
        public Type Type { get; private set; }
        public ConsoleColor Color { get; }

        public Token(TokenType tokenType, string value)
        {
            TokenType = tokenType;
            Value = value;
            Color = TokenColors.Colors[TokenType];

            if(TokenType == TokenType.Variable)
            {
                try
                {
                    var test = Scope.GetVariable(value.Trim());
                    Type = test.GetType();
                    if(true == Type.IsGenericType)
                    {
                        var gType = Type.GetGenericTypeDefinition();
                        var gArg = Type.GetGenericArguments();
                    }
                } catch (Exception e)
                {

                }
                //Console.WriteLine
            }

            if (TokenType == TokenType.Method)
            {
                try
                {
                    var current = new List<Token>();
                    current.AddRange(Token.CurrentTokens);
                    current.Reverse();
                    var test1 = current.Skip(1);
                    var test = Scope.GetVariable(value.Trim());
                    Type = test.GetType();
                    if (true == Type.IsGenericType)
                    {
                        var gType = Type.GetGenericTypeDefinition();
                        var gArg = Type.GetGenericArguments();
                    }
                }
                catch (Exception e)
                {

                }
                //Console.WriteLine
            }

        }
    }

    public class SyntaxHighlight
    {
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
            "=", ":", "\\.",
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
            //"\\{", "\\}",
            "\\(", "\\)",
            "\\[", "\\]",
        };

        private readonly List<Rule> _rules = new List<Rule>();

        public SyntaxHighlight()
        {
            _rules.AddRange(_keywords.Select(k => new Rule($"^(\\s*{k})", TokenType.Keyword)));
            _rules.AddRange(_operators.Select(o => new Rule($"^(\\s?{o}\\s?)", TokenType.Operator)));
            _rules.AddRange(_braces.Select(b => new Rule($"^{b}", TokenType.Brace)));

            //_rules.Add(new Rule(new Regex("^\\bself\\b"), _colors["self"]));

            // Double-quoted string, possibly containing escape sequences
            _rules.Add(new Rule("^\\s*\"[^\"\\\\]*(\\\\.[^\"\\\\]*)*", TokenType.String));
            // Single-quoted string, possibly containing escape sequences
            //(r"'[^'\\]*(\\.[^'\\]*)*'", 0, STYLES['string']),


            // def and class
            //_rules.Add(new Rule(new Regex("^\\bdef\\b\\s*(\\w+)"), _colors["defclass"]));
            _rules.Add(new Rule("^\\b(def\\s+)", TokenType.DefClass));
            //_rules.Add(new Rule(new Regex("^\\bclass\\b\\s*(\\w+)"), _colors["defclass"]));

            // comment
            // From '#' until a newline
            _rules.Add(new Rule("^\\s*#[^\\n]*", TokenType.Comment));

            // numbers
            _rules.Add(new Rule("^\\b[+-]?[0-9]+[lL]?\\b?", TokenType.Number));

            //_rules.Add(new Rule(new Regex("^\\b[+-]?[0-9]+[lL]?\\b"), _colors["number"]));
            //_rules.Add(new Rule(new Regex("^\\b[+-]?0[xX][0-9A-Fa-f]+[lL]?\\b"), _colors["number"]));
            //_rules.Add(new Rule(new Regex("^\\b[+-]?[0-9]+(?:\\.[0-9]+)?(?:[eE][+-]?[0-9]+)?\\b"), _colors["number"]));

            _rules.Add(new Rule("^(\\w+)", TokenType.Method, l =>
                {
                    bool result = false;
                    Token token = l.LastOrDefault();
                    if(default(Token) != token && TokenType.Operator == token.TokenType && "." == token.Value)
                    {
                        result = true;
                    }
                    return result;
                }
            ));
            _rules.Add(new Rule("^(\\s*\\w+\\s*)", TokenType.Variable));
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

        public List<Token> Tokenize(string code)
        {
            //List<Token> result = new List<Token>();
            Token.CurrentTokens = new List<Token>();
            var currentCode = code.Trim();
            while (false == string.IsNullOrWhiteSpace(currentCode))
            {
                bool ruleMatch = false;
                foreach (Rule rule in _rules)
                {
                    MatchCollection matchCollection = rule.Regex.Matches(currentCode);
                    var matches = matchCollection.Count;
                    if (matches > 0)
                    {
                        var firstMatch = matchCollection[0];
                        if(true == rule.Test(Token.CurrentTokens))
                        {
                            currentCode = currentCode.Substring(firstMatch.Value.Length);

                            ruleMatch = true;
                            Token.CurrentTokens.Add(new Token(rule.TokenType, firstMatch.Value));
                            break;
                        }
                    }
                }

                if (false == ruleMatch)
                {
                    ConsoleUtils.Write($"Cant match {currentCode}", ConsoleColor.White);
                    currentCode = "";
                }
            }

            return Token.CurrentTokens;
        }

        public void WriteToConsole(List<Token> tokens)
        {
            foreach (var token in tokens)
            {
                ConsoleUtils.Write(token.Value, token.Color);
            }
        }

        public void WriteToConsole(string code)
        {
            var currentCode = code.Trim();
            while (false == string.IsNullOrWhiteSpace(currentCode))
            {
                bool ruleMatch = false;
                foreach (Rule rule in _rules)
                {
                    MatchCollection matchCollection = rule.Regex.Matches(currentCode);
                    var matches = matchCollection.Count;
                    if (matches > 0)
                    {
                        var firstMatch = matchCollection[0];
                        ConsoleUtils.Write(firstMatch.Value, rule.Color);
                        //Console.ForegroundColor = rule.Color;
                        //Console.Write(firstMatch.Value);
                        currentCode = currentCode.Substring(firstMatch.Value.Length);
                        //currentCode = currentCode.Substring(firstMatch.Value.Length).TrimStart();

                        ruleMatch = true;
                        break;
                    }
                }

                if (false == ruleMatch)
                {
                    ConsoleUtils.Write($"Cant match {currentCode}", ConsoleColor.White);
                    //Console.ResetColor();
                    //Console.WriteLine($"Cant match {currentCode}");
                    currentCode = "";
                }
                
            }
            Console.ResetColor();
            //Console.WriteLine();
        }
    }
}
