using System;
using System.Text.RegularExpressions;

namespace DblDebug
{
    public class Processors
    {
        public static bool LineNumber(string line, Match match)
        {
            return true;
        }

        internal static bool Default(string l, Match m)
        {
            return true;
        }
    }
}
