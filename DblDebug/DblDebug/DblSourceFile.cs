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
        public IEnumerable<string> BreakLocations
        {
            get => GetBreakLocations();
        }

        private IEnumerable<string> GetBreakLocations()
        {
            return Scopes.Select(s => s.Name);
        }

        private readonly string _fullFileName;
        //private readonly string _sourceDirectory = "/Users/stevelamb/Development/ibcos/investigations/source/";
        private readonly string _sourceDirectory = "c:\\ibcos\\Repositorys\\gold\\source\\";
        public readonly List<Scope> Functions = new List<Scope>();
        public readonly List<Scope> SubRoutines = new List<Scope>();
        private List<string> _lines;



        private const int CONTEXT = 10;

        public DblSourceFile(string sourceDirectory, string fileName)
        {
            FileName = fileName;
            if(default(string) != sourceDirectory)
            {
                _sourceDirectory = sourceDirectory;
            }

            _fullFileName = GetFullFileName(FileName);
            Parse(_fullFileName);
        }

        internal Scope GetScope(string name)
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

        public List<Scope> Scopes { get; private set; } = new List<Scope>();

        private void Parse(string fileName)
        {
            _lines = File.ReadAllLines(fileName).ToList();
            Scope currentScope = default(Scope);
            int lineNumber = 1;
            foreach(string line in _lines)
            {
                try
                {
                    string trimmedLine = line.Trim();
                    Match match = _functionSubroutine.Match(line);

                    if(default(Match) != match && match.Success)
                    {
                        if(default(Scope) != currentScope)
                        {
                            currentScope.Finish(lineNumber);
                        }
                        currentScope = new Scope(this, match, line, lineNumber);
                        Scopes.Add(currentScope);
                        Functions.Add(currentScope);
                    }

                    if(default(Scope) != currentScope)
                    {
                        currentScope.ProcessLine(line, lineNumber);
                    }
                } catch (Exception e)
                {

                }
                lineNumber++;
            }
            if (default(Scope) != currentScope)
            {
                currentScope.Finish(lineNumber);
            }
        }

        internal void SetCode(ConsoleOutput code, int lineNumber)
        {
            code.Lines.Clear();
            code.Lines.Add(OutputLine.Blank);
            code.Lines.Add(new OutputLine($"File: {_fullFileName}", ConsoleColor.Yellow));

            int codeLineNo = lineNumber - 1;

            for(int i = lineNumber - CONTEXT; i < lineNumber + CONTEXT; i++){
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
                } catch (Exception e)
                {

                }
            }

            code.Lines.Add(OutputLine.Blank);
        }
    }

    public enum ScopeType
    {
        Function,
        Subroutine
    }

    enum ScopeState
    {
        Variables,
        Body
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
