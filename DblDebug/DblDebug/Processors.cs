using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DblDebug
{
    public class Processors
    {
        public static State LineNumber(State currentState, string line, Match match)
        {
            string lineNumber   = match.Groups[2].Value;
            string functionName = match.Groups[3].Value;
            string fileName     = match.Groups[4].Value;

            if(default(DblSourceFile) == currentState.DblSourceFile ||
                currentState?.DblSourceFile?.FileName != fileName)
            {
                currentState.DblSourceFile = new DblSourceFile(fileName);
            }

            currentState.CurrentLineNo      = Convert.ToInt32(lineNumber);
            currentState.CurrentFunction    = functionName;
            currentState.CurrentScope       = currentState.DblSourceFile.GetScope(functionName.ToLower());

            return currentState;
        }

        internal static State Default(State currentState, string line, Match match)
        {
            return currentState;
        }

        public static List<string> PreProcessExamine(List<string> response)
        {
            if(1 == response.Count)
            {
                return response;
            }
            IEnumerable<FieldInfo> fieldInfo = response.Select(l => new FieldInfo(l));
            int maxNameLength = fieldInfo.Max(f => f.Name.Length);
            int maxTypeLength = fieldInfo.Max(f => f.Type.Length);
            int maxValueLength = fieldInfo.Max(f => f.Value.Length);

            if(maxValueLength > 80)
            {
                maxValueLength = 80;
            }

            List<string> result = fieldInfo
                .OrderBy(f => f.Name)
                .Select(f => $"{f.Name.PadRight(maxNameLength, ' ')} {f.Type.PadRight(maxTypeLength, ' ')} {f.Value.PadRight(maxValueLength, ' ')}").ToList();

            return result;
        }
    }

    class FieldInfo
    {
        private static Regex _match = new Regex(@"^([a-zA-Z0-9_]+),\s+([a-zA-Z0-9_\[\]]+),\s+(.+)");
        public FieldInfo(string line)
        {
            Match match = _match.Match(line);

            if(default(Match) != match && match.Success)
            {
                Name    = match.Groups[1].Value;
                Type    = match.Groups[2].Value;
                Value   = match.Groups[3].Value;
            }
            else
            {
                Console.WriteLine($"Error in field {line}");
            }
        }

        public string Name { get; set; } = "";
        public string Type { get; set; } = "";
        public string Value { get; set; } = "";
    }
}
