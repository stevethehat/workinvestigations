﻿using Microsoft.Scripting.Hosting;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GoldRepl
{
    class Completion
    {
        public Regex Regex { get; set; }
        public Func<Match, string, List<string>> GetOptions { get; set; }

        public Completion(Regex regex, Func<Match, string, List<string>> getOptions)
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
                new Completion(new Regex(@"""([\/\w]+)"), (m, t) => GetPathOptions(m , t)),
                new Completion(new Regex(@"([a-zA-Z0-9_]*)\.([a-zA-Z0-9_]*)$"), (m, t) => GetPossibleOptions(GetParameters(m), m, t)),
                new Completion(new Regex(@"([a-zA-Z0-9_]*)$"), (m, t) => GetPossibleOptions(GetVariables(), m, t)),
                
            };
        }

        private List<string> GetPathOptions(Match m, string t)
        {
            Directory.EnumerateDirectories("/");
            List<string> result = new List<string>();
            result.AddRange(Directory.EnumerateDirectories("/"));
            return result;
        }

        // characters to start completion from
        public char[] Separators { get; set; } = new char[] { ' ', '.', '/', '(', ',', '[', '"' };
        public ScriptScope _scope { get; }
        private readonly List<string> _keywords = new List<string>() { "print", "dir", "len", "def" };


        private List<Completion> _regexes;

        private List<string> GetVariables()
        {
            return _scope.GetVariableNames().ToList();
        }

        protected IEnumerable<MethodInfo> GetExtensionMethods(Assembly assembly, Type extendedType)
        {
            List<MethodInfo> extension_methods = new List<MethodInfo>();
            if (typeof(IEnumerable<object>).IsAssignableFrom(extendedType))
            {
                Type t = typeof(Enumerable);

                foreach (MethodInfo mi in t.GetMethods().Distinct())
                {
                    if (mi.IsDefined(typeof(ExtensionAttribute), false))
                    {
                        extension_methods.Add(mi);
                    }
                }

            }

            return extension_methods;
        }

        private List<string> GetParameters(Match match)
        {
            List<string> result = new List<string>();
            string variableName = match.Groups[1].Value;
            string param = match.Groups[2].Value;
            
            try
            {
                var variable = _scope.GetVariable(variableName);

                if (null != variable)
                {
                    Type variableType = (Type)variable.GetType();

                    MethodInfo[] methodInfo = variableType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                    var methods = methodInfo.Where(m => !(m.Name.StartsWith("get_") || m.Name.StartsWith("set_")))
                                            .Select(mi => mi.Name)
                                            .ToList();
                    result.AddRange(methods);

                    result.AddRange(GetExtensionMethods(null, variableType).Select(min => min.Name));

                    PropertyInfo[] propertyInfo = variableType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                    result.AddRange(propertyInfo.Select(mi => mi.Name).ToList().Distinct());
                }
            }
            catch (Exception e)
            {

            }

            return result;
        }

        private List<string> GetPossibleOptions(List<string> possibleOptions, Match match, string fullText)
        {
            List<string> result = new List<string>();
            string matchText = match.Groups[match.Groups.Count - 1].Value;
            string previousText = fullText.Substring(0, fullText.Length - matchText.Length);
            foreach (string possibleOption in possibleOptions.Distinct())
            {
                if (true == possibleOption.StartsWith(matchText))
                {
                    result.Add(possibleOption);
                }
            }

            return result;
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
                    if (true == match.Success)
                    {
                        result.AddRange(completion.GetOptions(match, text));
                        break;
                    }
                }
            }

            return result.ToArray();
        }
    }
}
