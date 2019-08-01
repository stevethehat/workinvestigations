using System;
using System.Text.RegularExpressions;

namespace DblDebug
{
    public class Formatters
    {
        public static OutputLine CodeLine(string line, Match match)
        {
            OutputLine result = new OutputLine();
            result.Line.Add(new OutputChunk()
            {
                Text = $"  "
            });
            result.Line.Add(new OutputChunk()
            {
                Text = $"{match.Groups[1].Value}",
                ForegroundColor = ConsoleColor.Yellow
            });
            result.Line.Add(new OutputChunk()
            {
                Text = $" > {match.Groups[2].Value}"
            });

            return result;
        }

        public static OutputLine LineNumber(string line, Match match)
        {
            OutputLine result = new OutputLine();
            result.Line.Add(new OutputChunk()
            {
                Text = $"{match.Groups[1].Value} ("
            }); ;
            result.Line.Add(new OutputChunk()
            {
                Text = $"{match.Groups[2].Value}",
                ForegroundColor = ConsoleColor.Yellow
            });
            result.Line.Add(new OutputChunk()
            {
                Text = $") in "
            });
            result.Line.Add(new OutputChunk()
            {
                Text = $"{match.Groups[3].Value}",
                ForegroundColor = ConsoleColor.Yellow
            });
            result.Line.Add(new OutputChunk()
            {
                Text = $" ("
            });
            result.Line.Add(new OutputChunk()
            {
                Text = $"{match.Groups[4].Value}",
                ForegroundColor = ConsoleColor.Yellow
            });
            result.Line.Add(new OutputChunk()
            {
                Text = $")"
            });

            if(6 == match.Groups.Count)
            {
                result.Line.Add(new OutputChunk()
                {
                    Text = match.Groups[5].Value
                });
            }

            return result;
        }
    }
}
