using System;
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
    }
}
