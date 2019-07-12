﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace GoldRepl
{
    public class Repl
    {
        private readonly ScriptEngine _python;
        private readonly ScriptScope _scope;

        public Repl()
        {
            _python = Python.CreateEngine();
            _scope = _python.CreateScope();

            _scope.ImportModule("clr");
            _python.Execute("import clr");
            _python.Execute("clr.AddReference(\"goldrepl\")", _scope);
            ReadLine.HistoryEnabled = true;
            ReadLine.AutoCompletionHandler = new AutoCompletionHandler(_scope);

            //_python.Execute("clr.AddReference(\"GoldApiServer.DataLayer\")", _scope);
            //_python.Execute("from Net.Ibcos.GoldAPIServer.DataLayer.Models import *", _scope);
        }

        public void InitData(string dataFolder = "~/gold/data")
        {
            //Gold.Gold gold = new Gold.Gold(dataFolder);

            //_scope.SetVariable("gold", gold);
            _python.Execute("from GoldRepl import *", _scope);

            Isams isams = new Isams();
            _scope.SetVariable("isams", isams);

        }

        internal void Execute(string scriptFile)
        {
            ScriptSource source = _python.CreateScriptSourceFromFile(scriptFile);
            RunCode(source);
        }

        public void RunInteractive()
        {
            string code = "";
            while (code != "q\n")
            {
                if (false == string.IsNullOrEmpty(code))
                {
                    ScriptSource source = _python.CreateScriptSourceFromString(code);
                    RunCode(source);
                }
                code = GetCode();
            }
        }

        public void Init(string fullPath)
        {
            ScriptSource source = _python.CreateScriptSourceFromFile(fullPath);
            RunCode(source);
        }

        protected dynamic RunCode(ScriptSource source)
        {
            dynamic result = new {};
            try
            {
                result = source.Execute(_scope);
            }
            catch (IronPython.Runtime.UnboundNameException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        protected string GetCode()
        {
            string result = "";
            bool codeComplete = false;
            int indent = 0;

            while(false == codeComplete)
            {
                Console.Write("".PadLeft(indent, '>'));
                if(indent > 0)
                {
                    Console.Write(" ");
                }

                string line = GetCodeLine();
                result = $"{result}{"".PadLeft(indent * 2, ' ')}{line}{Environment.NewLine}";

                if (line.EndsWith(":", StringComparison.InvariantCulture))
                {
                    indent++;
                }
                else
                {
                    if(true == string.IsNullOrEmpty(line) && indent > 0)
                    {
                        indent--;
                    }
                    codeComplete = 0 == indent;
                }

            }
            return result;
        }

        protected string GetCodeLine()
        {
            return ReadLine.Read(">");

            string result = "";
            ConsoleKeyInfo key = new ConsoleKeyInfo();

            while (ConsoleKey.Enter != key.Key)
            {
                bool echoChar = true;

                switch (key.Key)
                {
                    case ConsoleKey.Tab:
                        string completion = TabComplete(result);
                        result += completion;
                        Console.Write(completion);
                        echoChar = false;
                        break;

                    case ConsoleKey.Backspace:
                        Console.Write($"\r{"".PadLeft(result.Length, ' ')}");
                        result = result.Substring(0, result.Length - 1);
                        Console.Write($"\r{result}");
                        echoChar = false;
                        break;

                    default:
                        if ('\0' != key.KeyChar)
                        {
                            result = result + key.KeyChar;
                        }
                        break;

                }
                /*
                if (ConsoleKey.Tab == key.Key)
                {
                    string completion = TabComplete(result);
                    result += completion;
                    Console.Write(completion);
                    echoChar = false;
                }
                if(ConsoleKey.Backspace == key.Key)
                {
                    result = result.Substring(0, result.Length - 1);
                    Console.Write($"\r{result}");
                    echoChar = false;
                }

                if ('\0' != key.KeyChar)
                {
                    result = result + key.KeyChar;
                }
                */

                if (echoChar)
                {
                    Console.Write(key.KeyChar);
                }

                key = Console.ReadKey(true);
            }

            Console.Write(key.KeyChar);

            return result;
        }

        protected string TabComplete(string line)
        {
            string result = string.Empty;
            Regex regex = new Regex(@"([a-zA-Z0-9_]*)\.([a-zA-Z0-9_]*)$");
            Match match = regex.Match(line);

            if(default(Match) != match)
            {
                string variable = match.Groups[1].Value;
                string soFar = match.Groups[2].Value;
            }

            return result;
        }
    }

    class AutoCompletionHandler : IAutoCompleteHandler
    {
        public AutoCompletionHandler(ScriptScope scope)
        {
            _scope = scope;
        }
        // characters to start completion from
        public char[] Separators { get; set; } = new char[] { ' ', '.', '/' };
        public ScriptScope _scope { get; }
        private readonly List<string> _keywords = new List<string>() { "print", "dir", "len", "def" };

        // text - The current text entered in the console
        // index - The index of the terminal cursor within {text}
        public string[] GetSuggestions(string text, int index)
        {
            List<string> result = new List<string>();

            var variables = _scope.GetVariableNames().ToList();
            List<string> options = new List<string>();
            options.AddRange(_keywords);
            options.AddRange(variables);
            foreach(string option in options)
            {
                if(true == option.StartsWith(text))
                {
                    result.Add(option);
                }
            }

            return result.ToArray();


            if (text.StartsWith("git "))
                return new string[] { "init", "clone", "pull", "push" };
            else
                return null;
        }
    }
}
