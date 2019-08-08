using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DblDebug
{
    public class Scope
    {
        public int DefinitionLineNumber { get; }
        public int EndLineNumber { get; protected set; }
        public ScopeType Type { get; protected set; }
        public DblSourceFile Parent { get; }
        public string Name { get; protected set; }


        public Scope(DblSourceFile parent, Match match, string line, int lineNumber)
        {
            Parent = parent;
            DefinitionLineNumber = lineNumber;
        }

    }
    public class RoutineScope: Scope
    {
        public RoutineScope(DblSourceFile parent, Match match, string line, int lineNumber)
            : base(parent, match, line, lineNumber)
        {
            base.Name = match.Groups[2].Value;

            base.Type = ("function" == match.Groups[1].Value)
                ? ScopeType.Function
                : ScopeType.Subroutine;
        }

        public RoutineScope()
            : base(default(DblSourceFile), default(Match), "", (int) 1)
        {
            base.Name = "unknown";
        }

        public LabelScope GetLabelScopeFromLine(int lineNumber)
        {
            return Labels.Where(l => l.DefinitionLineNumber <= lineNumber && l.EndLineNumber >= lineNumber).FirstOrDefault();
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
            header.Line.Add(new OutputChunk(" End: "));
            header.Line.Add(new OutputChunk(EndLineNumber, ConsoleColor.Yellow));

            output.Lines.Add(header);

            output.Lines.Add(OutputLine.Blank);
            output.Lines.Add(new OutputLine("Variables", ConsoleColor.Yellow));
            //output.Lines.Add(new OutputLine(string.Join(", ", Variables.Select(v => $"{{:c}}{v.Name}{{:w}} '{v.Type}'"))));
            output.Lines.Add(new OutputLine(string.Join(", ", Variables.Select(v => $"{v.Name} '{v.Type}'"))));

            output.Lines.Add(OutputLine.Blank);
            output.Lines.Add(new OutputLine("Labels", ConsoleColor.Yellow));
            output.Lines.Add(new OutputLine(string.Join(", ", Labels.Select(l => l.Name).OrderBy(n => n))));
            output.Lines.Add(OutputLine.Blank);
            output.Lines.Add(new OutputLine("Functions", ConsoleColor.Yellow));
            output.Lines.Add(new OutputLine(string.Join(", ", Parent.Functions.Select(f => f.Name).OrderBy(n => n))));
            output.Lines.Add(OutputLine.Blank);
        }

        public List<Variable> Variables = new List<Variable>();
        public List<LabelScope> Labels = new List<LabelScope>();

        private static Regex _procStart = new Regex(@"^\s*\.?proc");
        private static Regex _label = new Regex(@"^\s*([a-zA-Z0-9_]+)\s*,");
        // ^\s*(opt|req)?\s*(in|out|inout)?\s*([a-zA-Z0-9_]+)\s*,\s*([a-zA-Z0-9_]+)
        //private static Regex _variable = new Regex(@"^\s*([a-zA-Z0-9_]+)\s*,\s*([a-zA-Z0-9_]+)");
        private static Regex _variable = new Regex(@"^\s*(opt|req)?\s*(in|out|inout)?\s*([a-zA-Z0-9_]+)\s*,\s*([a-zA-Z0-9_]+)");
        // .include 'skdp_passed' repository, group='skdp_passed'
        private static Regex _groups = new Regex(@"^\s*\.include\s'([a-zA-Z0-9_]+)'\s+repository\s*,\s*group\s*=\s*'([a-zA-Z0-9_]+)'");
        private ScopeState _state;
        private LabelScope _currentLabel = default(LabelScope);

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
                    if(default(LabelScope) != _currentLabel)
                    {
                        _currentLabel.Finish(lineNumber);
                    }
                    _currentLabel = new LabelScope(Parent, match, line, lineNumber);
                    Labels.Add(_currentLabel);
                }
            }
        }

        internal void Finish(int lineNumber)
        {
            EndLineNumber = lineNumber;
            Variables = Variables.OrderBy(v => v.Name).ToList();
            Labels = Labels.OrderBy(l => l.Name).ToList();
            if(default(LabelScope) != _currentLabel)
            {
                _currentLabel.Finish(lineNumber);
            }
        }
    }

    public class LabelScope : Scope
    {
        public LabelScope(DblSourceFile parent, Match match, string line, int lineNumber)
            : base(parent, match, line, lineNumber)
        {
            base.Name = match.Groups[1].Value;
            base.Type = ScopeType.Label;
        }

        internal void Finish(int lineNumber)
        {
            EndLineNumber = lineNumber;
        }
    }

    public enum ScopeType
    {
        Function,
        Subroutine,
        Label
    }

    enum ScopeState
    {
        Variables,
        Body
    }

}
