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

        public static OutputLine LineNumber(string line, Match match)
        {
            OutputLine result = new OutputLine();
            result.Line.Add(new OutputChunk($"{match.Groups[1].Value} ("));
            result.Line.Add(new OutputChunk(match.Groups[2].Value, ConsoleColor.Yellow));
            result.Line.Add(new OutputChunk(") in "));
            result.Line.Add(new OutputChunk(match.Groups[3].Value, ConsoleColor.Yellow));
            result.Line.Add(new OutputChunk(" ("));
            result.Line.Add(new OutputChunk(match.Groups[4].Value, ConsoleColor.Yellow));
            result.Line.Add(new OutputChunk(")"));

            if(6 == match.Groups.Count)
            {
                result.Line.Add(new OutputChunk(match.Groups[5].Value));
            }

            return result;
        }

        internal static OutputLine Error(string line, Match match)
        {
            return new OutputLine(line, ConsoleColor.Red);
        }
    }
}
