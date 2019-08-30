using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DblDebug
{
    public class DblSourceFile
    {
        public readonly string FileName;
        public readonly string FullFileName;
        public IEnumerable<string> BreakLocations
        {
            get => GetBreakLocations();
        }

        private IEnumerable<string> GetBreakLocations()
        {
            return Scopes.Select(s => s.Name);
        }

        //private readonly string _fullFileName;
        private readonly string _sourceDirectory = "/Users/stevelamb/Development/ibcos/investigations/source/";
        //private readonly string _sourceDirectory = "c:\\ibcos\\Repositorys\\gold\\source\\";
        public readonly List<RoutineScope> Functions = new List<RoutineScope>();
        public readonly List<RoutineScope> SubRoutines = new List<RoutineScope>();
        private List<string> _lines;



        private const int CONTEXT = 10;

        public DblSourceFile(string sourceDirectory, string fileName)
        {
            FileName = fileName;
            if(default(string) != sourceDirectory)
            {
                _sourceDirectory = sourceDirectory;
            }

            FullFileName = GetFullFileName(FileName);
            Parse(FullFileName);
        }

        internal async Task<bool> Peek(CoreDebug debug, string commandText)
        {
            Regex check = new Regex(@":peek\s+([a-zA-z0-9_]+)");
            Match match = check.Match(commandText);

            if(default(Match) != match && true == match.Success)
            {
                RoutineScope routine = GetScope(match.Groups[1].Value);

                if(default(RoutineScope) != routine)
                {
                    SetCode(debug.Outputs.Code, routine.DefinitionLineNumber, debug.Settings.Get("autoviewlines"));
                }
                else
                {
                    // get current routine from line number then check its labels
                    routine = GetScopeFromLine(debug.State.CurrentLineNo);

                    if (default(RoutineScope) != routine)
                    {
                        LabelScope label = routine.Labels.Where(l => l.Name == match.Groups[1].Value).FirstOrDefault();
                        string labels = string.Join(", ", routine.Labels.Select(l => l.Name).OrderBy(l => l));
                        if(default(LabelScope) != label){
                            SetCode(debug.Outputs.Code, label.DefinitionLineNumber, debug.Settings.Get("autoviewlines"));
                        }

                    }
                }

            }

            return await Task.FromResult<bool>(true);
        }

        public RoutineScope GetScopeFromLine(int lineNumber)
        {
            return Scopes
                .Where(s => s.BodyLineNumber <= lineNumber && s.EndLineNumber >= lineNumber)
                .FirstOrDefault();
        }

        internal RoutineScope GetScope(string name)
        {
            return Scopes.Find(s => s.Name.ToLower() == name);
        }

        private string GetFullFileName(string fileName)
        {
            string result = default(string);

            var sourceDirectories = Directory.GetDirectories(_sourceDirectory);
            foreach(string directory in sourceDirectories)
            {
                string fullFileName = Path.Combine(_sourceDirectory, directory, fileName);

                if(true == File.Exists(fullFileName))
                {
                    result = fullFileName;
                    break;
                }
            }

            return result;
        }

        //private 
        private static Regex _functionSubroutine = new Regex(@"^\s*\.?(function|subroutine)\s+([a-zA-Z0-9_]+)");

        public List<RoutineScope> Scopes { get; private set; } = new List<RoutineScope>();

        private void Parse(string fileName)
        {
            _lines = File.ReadAllLines(fileName).ToList();
            RoutineScope currentScope = default(RoutineScope);
            int lineNumber = 1;
            foreach(string line in _lines)
            {
                try
                {
                    string trimmedLine = line.Trim();
                    Match match = _functionSubroutine.Match(line);

                    if(default(Match) != match && match.Success)
                    {
                        if(default(RoutineScope) != currentScope)
                        {
                            currentScope.Finish(lineNumber);
                        }
                        currentScope = new RoutineScope(this, match, line, lineNumber);
                        Scopes.Add(currentScope);
                        Functions.Add(currentScope);
                    }

                    if(default(RoutineScope) != currentScope)
                    {
                        currentScope.ProcessLine(line, lineNumber);
                    }
                } catch (Exception e)
                {

                }
                lineNumber++;
            }
            if (default(RoutineScope) != currentScope)
            {
                currentScope.Finish(lineNumber);
            }
        }

        internal void SetCode(ConsoleOutput code, int lineNumber, string numLines)
        {
            int showLines = 20;
            if(Int32.TryParse(numLines, out int parsedLines))
            {
                showLines = parsedLines;
            }

            code.Lines.Clear();
            if (0 != showLines)
            {
                code.Lines.Add(OutputLine.Blank);
                code.Lines.Add(new OutputLine($"File: {FullFileName}", ConsoleColor.Yellow));

                int codeLineNo = lineNumber - 1;

                for (int i = lineNumber - showLines; i < lineNumber + showLines; i++)
                {
                    try
                    {
                        if (codeLineNo == i)
                        {
                            code.Lines.Add(new OutputLine($"{i + 1,6:d}> {_lines[i].Trim(new[] { '\n', '\r' })}", ConsoleColor.White, ConsoleColor.Red));
                        }
                        else
                        {
                            code.Lines.Add(new OutputLine($"{i + 1,6:d}> {_lines[i].Trim(new[] { '\n', '\r' })}"));
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }

                code.Lines.Add(OutputLine.Blank);
            }
        }
    }


    public class Variable
    {
        public string Name { get; private set; }
        public string Type { get; private set; }

        public Variable(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }

    
}
