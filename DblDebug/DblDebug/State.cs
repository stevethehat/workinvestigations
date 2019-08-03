using System;
using System.IO;

namespace DblDebug
{
    public class State
    {
        public string CurrentFile { get; set; } = "";
        public string CurrentFunction { get; set; } = "";
        public int CurrentLineNo { get; set; } = 0;
        public string LastEnteredCommand { get; set; } = "";
        public string CurrentLine { get; set; }
        public Scope CurrentScope { get; set; }
        public DblSourceFile DblSourceFile { get; set; }

        public string SaveFolder { get; set; } = Path.Combine("~/", ".dbldebug");

        public bool Save(CoreDebug debug)
        {
            debug.Outputs.General.Lines.Add(new OutputLine($"Session saved"));
            //Environment.SpecialFolder.UserProfile
            return true;
        }

        public bool Load(CoreDebug debug)
        {
            debug.Outputs.General.Lines.Add(new OutputLine($"Session loaded"));
            return true;
        }
    }
}
