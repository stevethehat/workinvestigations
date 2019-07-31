using System;
using System.Text.RegularExpressions;

namespace DblDebug
{
    public class LineProcessor
    {
        public Regex MatchRegex { get; set; }
        public Func<State, string, Match, State> Processor { get; set; }
        public Func<string, Match, OutputLine> Formatter { get; set; }

        public LineProcessor(Regex matchRegex, Func<State, string, Match, State> processor, Func<string, Match, OutputLine> formatter)
        {
            MatchRegex = matchRegex;
            Processor = processor;
            Formatter = formatter;
        }

        public LineProcessor()
        {

        }
    }
}
