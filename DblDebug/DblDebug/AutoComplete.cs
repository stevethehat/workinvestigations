﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DblDebug
{
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

    class AutoCompleteHandler : IAutoCompleteHandler
    {
        public AutoCompleteHandler(CoreDebug debug)
        {
            _debug = debug;
            _completions = new List<Completion>()
            {
                new Completion(new Regex(@"^([a-z]*) ([a-z]*)"), (m, t) => GetPossibleOptions(GetSubCommands(m, t), m, t)),
                new Completion(new Regex(@"^([a-z]*)"), (m, t) => GetPossibleOptions(GetCommands(), m, t)),
            };
        }

        private IEnumerable<string> GetSubCommands(Match match, string text)
        {
            string command = match.Groups[1].Value;
            return _debug.Commands.MainCommands.Find(c => c.Name == command).SubCommands;
        }

        // characters to start completion from
        public char[] Separators { get; set; } = new char[] { ' ', '.', '/', '(', ',' };
        private readonly CoreDebug _debug;
        private List<Completion> _completions;

        private IEnumerable<string> GetCommands()
        {
            return _debug.Commands.MainCommands.Select(c => c.Name);
        }

        private List<string> GetVariables()
        {
            return new List<string>();
        }

        private IEnumerable<string> GetPossibleOptions(IEnumerable<string> possibleOptions, Match match, string fullText)
        {
            List<string> result = new List<string>();
            string matchText = match.Groups[match.Groups.Count - 1].Value;
            string previousText = fullText.Substring(0, fullText.Length - matchText.Length);
            foreach (string possibleOption in possibleOptions)
            {
                if (true == possibleOption.StartsWith(matchText, StringComparison.CurrentCultureIgnoreCase))
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

            try
            {
                List<string> options = new List<string>();
                foreach (Completion completion in _completions)
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
            }
            catch (Exception e)
            {

            }


            return result.ToArray();
        }
    }
}
