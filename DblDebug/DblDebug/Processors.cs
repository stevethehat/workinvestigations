using System;
using System.Text.RegularExpressions;

namespace DblDebug
{
    public class Processors
    {
        public static State LineNumber(State currentState, string line, Match match)
        {
            return currentState;
        }

        internal static State Default(State currentState, string line, Match match)
        {
            return currentState;
        }
    }
}
