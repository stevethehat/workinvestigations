using System;
namespace DblDebug
{
    public class State
    {
        public string CurrentFile { get; set; } = "";
        public int CurrentLineNo { get; set; } = 0;
        public string CurrentLine { get; set; }
        public DblSourceFile DblSourceFile { get; set; }
    }
}
