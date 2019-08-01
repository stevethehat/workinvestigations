using System;
namespace DblDebug
{
    public class State
    {
        public string CurrentFile { get; set; } = "";
        public string CurrentFunction { get; set; } = "";
        public int CurrentLineNo { get; set; } = 0;
        public string CurrentLine { get; set; }
        public Scope CurrentScope { get; set; }
        public DblSourceFile DblSourceFile { get; set; }
    }
}
