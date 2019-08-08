using System;
using System.Text.RegularExpressions;

namespace DblDebug
{
    public class Formatters
    {
        public static OutputLine CodeLine(string line, Match match)
        {
            OutputLine result = new OutputLine();
            result.Line.Add(new OutputChunk(match.Groups[1].Value));
            result.Line.Add(new OutputChunk(match.Groups[2].Value, ConsoleColor.Yellow));
            result.Line.Add(new OutputChunk($"> {match.Groups[3].Value}"));

            return result;
        }

        public static OutputLine LineNumber(CoreDebug debug, string line, Match match)
        {
            int lineNumber      = Convert.ToInt32(match.Groups[2].Value);
            string function     = match.Groups[3].Value;
            OutputLine result   = new OutputLine();

            result.Line.Add(new OutputChunk($"{match.Groups[1].Value} ("));
            result.Line.Add(new OutputChunk(lineNumber, ConsoleColor.Yellow));
            result.Line.Add(new OutputChunk(") in "));
            result.Line.Add(new OutputChunk(function, ConsoleColor.Yellow));
            result.Line.Add(new OutputChunk(" ("));
            result.Line.Add(new OutputChunk(match.Groups[4].Value, ConsoleColor.Yellow));
            result.Line.Add(new OutputChunk(")"));

            if(6 == match.Groups.Count)
            {
                result.Line.Add(new OutputChunk(match.Groups[5].Value));
            }

            RoutineScope scope = debug.State.DblSourceFile.GetScopeFromLine(lineNumber);

            if(default(RoutineScope) != scope && scope.Name.ToLower() != function.ToLower())
            {
                Scope label = scope.GetLabelScopeFromLine(lineNumber);
                if(default(LabelScope) != label)
                {
                    result.Line.Add(new OutputChunk(" "));
                    result.Line.Add(new OutputChunk(label.Name, ConsoleColor.Yellow));
                }
            }


            return result;
        }

        internal static OutputLine Error(string line, Match match)
        {
            return new OutputLine(line, ConsoleColor.Red);
        }
    }
}
