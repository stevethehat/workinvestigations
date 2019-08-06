using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DblDebug
{
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

            OutputLine header = new OutputLine();
            header.Line.Add(new OutputChunk("Name: "));
            header.Line.Add(new OutputChunk(Name, ConsoleColor.Yellow));
            header.Line.Add(new OutputChunk(" Definition: "));
            header.Line.Add(new OutputChunk(DefinitionLineNumber, ConsoleColor.Yellow));
            header.Line.Add(new OutputChunk(" Body: "));
            header.Line.Add(new OutputChunk(BodyLineNumber, ConsoleColor.Yellow));
            header.Line.Add(new OutputChunk("End: "));
            header.Line.Add(new OutputChunk(EndLineNumber, ConsoleColor.Yellow));

            output.Lines.Add(header);

            output.Lines.Add(new OutputLine($"Name: {Name} Definition: {DefinitionLineNumber} Body: {BodyLineNumber} End: {EndLineNumber}"));
            output.Lines.Add(OutputLine.Blank);
            output.Lines.Add(new OutputLine("Variables", ConsoleColor.Yellow));
            //output.Lines.Add(new OutputLine(string.Join(", ", Variables.Select(v => $"{{:c}}{v.Name}{{:w}} '{v.Type}'"))));
            output.Lines.Add(new OutputLine(string.Join(", ", Variables.Select(v => $"{v.Name} '{v.Type}'"))));

            output.Lines.Add(OutputLine.Blank);
            output.Lines.Add(new OutputLine("Labels", ConsoleColor.Yellow));
            output.Lines.Add(new OutputLine(string.Join(", ", Labels)));
            output.Lines.Add(OutputLine.Blank);
            output.Lines.Add(new OutputLine("Functions", ConsoleColor.Yellow));
            output.Lines.Add(new OutputLine(string.Join(", ", Parent.Functions.Select(f => f.Name))));
            output.Lines.Add(OutputLine.Blank);
        }

        public DblSourceFile Parent { get; }
        public ScopeType Type { get; }
        public string Name { get; }
        public int DefinitionLineNumber { get; }
        public int EndLineNumber { get; private set; }

        public List<Variable> Variables = new List<Variable>();
        public List<string> Labels = new List<string>();

        private static Regex _procStart = new Regex(@"^\s*\.?proc");
        private static Regex _label = new Regex(@"^\s*([a-zA-Z0-9_]+)\s*,");
        // ^\s*(opt|req)?\s*(in|out|inout)?\s*([a-zA-Z0-9_]+)\s*,\s*([a-zA-Z0-9_]+)
        //private static Regex _variable = new Regex(@"^\s*([a-zA-Z0-9_]+)\s*,\s*([a-zA-Z0-9_]+)");
        private static Regex _variable = new Regex(@"^\s*(opt|req)?\s*(in|out|inout)?\s*([a-zA-Z0-9_]+)\s*,\s*([a-zA-Z0-9_]+)");
        // .include 'skdp_passed' repository, group='skdp_passed'
        private static Regex _groups = new Regex(@"^\s*\.include\s'([a-zA-Z0-9_]+)'\s+repository\s*,\s*group\s*=\s*'([a-zA-Z0-9_]+)'");
        private ScopeState _state;

        public int BodyLineNumber { get; private set; }

        internal void ProcessLine(string line, int lineNumber)
        {
            Match match = default(Match);

            if (ScopeState.Variables == _state)
            {
                match = _procStart.Match(line);
                if (default(Match) != match && true == match.Success)
                {
                    _state = ScopeState.Body;
                    BodyLineNumber = lineNumber;
                }
                match = _variable.Match(line);
                if (default(Match) != match && true == match.Success)
                {
                    Variables.Add(new Variable(match.Groups[3].Value, match.Groups[4].Value));
                }
                match = _groups.Match(line);
                if (default(Match) != match && true == match.Success)
                {
                    Variables.Add(new Variable(match.Groups[2].Value, match.Groups[1].Value));
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
            Variables = Variables.OrderBy(v => v.Name).ToList();
            Labels = Labels.OrderBy(l => l).ToList();
        }
    }
}
