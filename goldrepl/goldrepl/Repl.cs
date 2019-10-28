﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Microsoft.CSharp;

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
            _scope.SetVariable("scope", _scope);

        }

        internal void Execute(string scriptFile)
        {
            ScriptSource source = _python.CreateScriptSourceFromFile(scriptFile);
            RunCode(source, "");
        }

        public void RunInteractive()
        {
            string code = "";
            while (code != "q\n")
            {
                if (false == string.IsNullOrEmpty(code))
                {
                    ScriptSource source = _python.CreateScriptSourceFromString(code);
                    RunCode(source, code);
                }
                code = GetCode();
            }
        }

        public void Init(string fullPath)
        {
            ScriptSource source = _python.CreateScriptSourceFromFile(fullPath);
            RunCode(source, "");
        }

        protected dynamic RunCode(ScriptSource source, string code)
        {
            dynamic result = new {};
            try
            {
                result = source.Execute(_scope);

                if(null != result)
                {
                    source = _python.CreateScriptSourceFromString("print " + code);
                    source.Execute(_scope);

                }
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
            return ReadLine.Read("");
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

    class Completion
    {
        public Regex Regex { get; set; }
        public Func<Match, string, IEnumerable<string>> GetOptions { get; set; }

        public Completion(Regex regex, Func<Match, string, IEnumerable<string>> getOptions)
        {
            Regex = regex;
            GetOptions = getOptions;
        }
    }

    class AutoCompletionHandler : IAutoCompleteHandler
    {
        public AutoCompletionHandler(ScriptScope scope)
        {
            _scope = scope;
            _regexes = new List<Completion>()
            {
                new Completion(new Regex(@"([a-zA-Z0-9_]*)\.([a-zA-Z0-9_]*)$"),
                    (m, t) => FilterOptions(GetParameters(m), m)),

                new Completion(new Regex(@"([a-zA-Z0-9_]*)\.([a-zA-Z0-9_]*)\s=\s([a-zA-Z0-9_]*)$"),
                    (m, t) => FilterOptions(GetVariablesOfType(ParameterType(m)), m)),

                /*
                new Completion(new Regex(@"([a-zA-Z0-9_]*)\s=\s([a-zA-Z0-9_]*)$"),
                    (m, t) => FilterOptions(GetVariablesOfType(m), m)),
                */

                new Completion(new Regex(@"([a-zA-Z0-9_]*)$"),
                    (m, t) => FilterOptions(GetVariables(), m)),
            };
        }

        // characters to start completion from
        public char[] Separators { get; set; } = new char[] { ' ', '.', '/', '(', ',' };
        public ScriptScope _scope { get; }
        private readonly List<string> _keywords = new List<string>() { "print", "dir", "len", "def" };


        private List<Completion> _regexes;

        private List<string> GetVariables()
        {
            return _scope.GetVariableNames().ToList() ;
        }

        private List<string> GetVariablesOfType(Match m)
        {
            string variableName = m.Groups[1].Value;
            object variable = _scope.GetVariable(variableName);



            return _scope.GetVariableNames().ToList();
        }

        private IEnumerable<string> GetVariablesOfType(Type type)
        {
            IEnumerable<string> result = _scope.GetVariableNames().Where(v => {
                bool correctType = true;
                var variable = _scope.GetVariable(v);
                if (null != variable) {
                    var variableType = variable.GetType();
                }

                //if (_scope)
                //_scope.GetVariable(v).GetType == type
                return correctType;
            });
            //object variable = _scope.GetVariable(variableName);



            return result;
        }


        private Type ParameterType(Match m)
        {
            Type result = default(Type);
            string variableName = m.Groups[1].Value;
            string parameterName = m.Groups[2].Value;

            var variable = _scope.GetVariable(variableName);
            if (null != variable)
            {
                Type variableType = (Type)variable.GetType();

                PropertyInfo propertyInfo = variableType.GetProperty(parameterName);

                result = propertyInfo.PropertyType;
            }
            return result;
        }

        private List<string> GetParameters(Match m)
        {
            List<string> result = new List<string>();
            string variableName = m.Groups[1].Value;
            string param = m.Groups[2].Value;

            var variable = _scope.GetVariable(variableName);
            if(null != variable) {
                Type variableType = (Type)variable.GetType();

                MethodInfo[] methodInfo = variableType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                result.AddRange(methodInfo.Select(mi => mi.Name).ToList());

                PropertyInfo[] propertyInfo = variableType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                result.AddRange(propertyInfo.Select(mi => mi.Name).ToList());
            }

            return result;
        }

        private List<string> GetParametersOfType(Match m)
        {
            List<string> result = new List<string>();
            string variableName = m.Groups[1].Value;
            string param = m.Groups[2].Value;

            var variable = _scope.GetVariable(variableName);
            if (null != variable)
            {
                Type variableType = (Type)variable.GetType();

                MethodInfo[] methodInfo = variableType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                result.AddRange(methodInfo.Select(mi => mi.Name).ToList());

                PropertyInfo[] propertyInfo = variableType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                result.AddRange(propertyInfo.Select(mi => mi.Name).ToList());
            }

            return result;
        }

        private IEnumerable<string> FilterOptions(IEnumerable<string> possibleOptions, Match match)
        {
            List<string> result = new List<string>();
            string matchText = match.Groups[match.Groups.Count - 1].Value;

            return possibleOptions.Where(po => po.StartsWith(matchText, StringComparison.CurrentCultureIgnoreCase));
        }

        // text - The current text entered in the console
        // index - The index of the terminal cursor within {text}
        public string[] GetSuggestions(string text, int index)
        {
            List<string> result = new List<string>();

            List<string> options = new List<string>();
            foreach (Completion completion in _regexes)
            {
                Match match = completion.Regex.Match(text);
                if (default(Match) != match)
                {
                    if(true == match.Success)
                    {
                        result.AddRange(completion.GetOptions(match, text));
                        break;
                    }
                }
            }

            //options.AddRange(_keywords);
            //options.AddRange(variables);
            /*
            foreach(string option in options)
            {
                if(true == option.StartsWith(text))
                {
                    result.Add(option);
                }
            }
            */
            return result.ToArray();


            if (text.StartsWith("git "))
                return new string[] { "init", "clone", "pull", "push" };
            else
                return null;
        }
    }
}
