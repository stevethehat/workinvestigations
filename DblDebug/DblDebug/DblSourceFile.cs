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
        private readonly List<Scope> _functions = new List<Scope>();
        private readonly List<Scope> _subRoutines = new List<Scope>();
        private List<string> _lines;



        private const int CONTEXT = 10;

        public DblSourceFile(string fileName)
        {
            FileName = fileName;
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

    public class Scope
    {
        public Scope(DblSourceFile parent, Match match, string line, int lineNumber)
        {
            Parent = parent;
            Type = ("function" == match.Groups[1].Value) 
                ? ScopeType.Function
                : ScopeType.Subroutine;
            Name = match.Groups[2].Value;
            DefinitionLineNumber = lineNumber;
        }

        public Scope()
        {
            Name = "unknown";
        }

        public void Info(ConsoleOutput output)
        {
            output.Lines.Add(OutputLine.Blank);
            output.Lines.Add(new OutputLine($"Name: {Name} Definition: {DefinitionLineNumber} Body: {BodyLineNumber} End: {EndLineNumber}"));
            output.Lines.Add(OutputLine.Blank);
            output.Lines.Add(new OutputLine("Variables"));
            output.Lines.Add(new OutputLine(string.Join(", ", Variables)));
            output.Lines.Add(OutputLine.Blank);
            output.Lines.Add(new OutputLine("Labels"));
            output.Lines.Add(new OutputLine(string.Join(", ", Labels)));
        }

        public DblSourceFile Parent { get; }
        public ScopeType Type           { get; }
        public string Name              { get; }
        public int DefinitionLineNumber           { get; }
        public int EndLineNumber { get; private set; }

        public List<string>     Variables  = new List<string>();
        public List<string>     Labels     = new List<string>();

        private static Regex    _procStart = new Regex(@"^\s*\.?proc");
        private static Regex    _label     = new Regex(@"^\s*([a-zA-Z0-9_]+)\s*,");
        private static Regex    _variable  = new Regex(@"^\s*([a-zA-Z0-9_]+)\s*,");
        // .include 'skdp_passed' repository, group='skdp_passed'
        private static Regex    _groups    = new Regex(@"^\s*\.include\s'[a-zA-Z0-9_]+'\s+repository\s*,\s*group\s*=\s*'([a-zA-Z0-9_]+)'");
        private ScopeState      _state;

        public int BodyLineNumber { get; private set; }

        internal void ProcessLine(string line, int lineNumber)
        {
            Match match = default(Match);

            if (ScopeState.Variables == _state)
            {
                match = _procStart.Match(line);
                if(default(Match) != match && true == match.Success)
                {
                    _state = ScopeState.Body;
                    BodyLineNumber = lineNumber;
                }
                match = _variable.Match(line);
                if (default(Match) != match && true == match.Success)
                {
                    Variables.Add(match.Groups[1].Value);
                }
                match = _groups.Match(line);
                if (default(Match) != match && true == match.Success)
                {
                    Variables.Add(match.Groups[1].Value);
                }
            }

            if (ScopeState.Body == _state)
            {
                match = _label.Match(line);
                if (default(Match) != match && true == match.Success)
                {
                    Labels.Add(match.Groups[1].Value);
                }
            }
        }

        internal void Finish(int lineNumber)
        {
            EndLineNumber = lineNumber;
            Variables = Variables.OrderBy(v => v).ToList();
            Labels = Labels.OrderBy(l => l).ToList();
        }
    }
}
