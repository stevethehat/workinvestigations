using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DblDebug
{
    public class State
    {
       
        public string CurrentFile { get; set; } = "";
        public string CurrentFunction { get; set; } = "";
        public int CurrentLineNo { get; set; } = 0;
        public string LastEnteredCommand { get; set; } = "";
        public string CurrentLine { get; set; }
        public RoutineScope CurrentScope { get; set; }
        public DblSourceFile DblSourceFile { get; set; }

        private List<string> _breaks = new List<string>();
        public string SaveFolder { get; set; } = Path.Combine("~/", ".dbldebug");
        public string SourceDirectory { get; internal set; }
        public string HomeDirectory { get; }
        public string StateName { get; internal set; }

        public State(string sourceDirectory)
        {
            SourceDirectory = sourceDirectory;
            HomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }

        internal string GetName(string fullCommand)
        {
            string result = "unknown";

            if(default(string) != StateName)
            {
                result = StateName;
            }
            else
            {
                Match match = Regex.Match(fullCommand, @"^([a-zA-Z0-9_:]+)\s*([a-zA-Z0-9_:]+)");
                if (default(Match) != match && match.Success)
                {
                    result = match.Groups[2].Value;
                    StateName = result;
                }
            }

            return result;
        }

        internal string GetSaveDirectory(string name)
        {
            string saveDirectory = Path.Combine(HomeDirectory, ".dbldebug", name);
            if (false == Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            return saveDirectory;
        }

        public async Task<bool> Save(CoreDebug debug, string fullCommand)
        {
            string name = GetName(fullCommand);
            string saveDirectory = GetSaveDirectory(name);

            File.WriteAllLines(Path.Combine(saveDirectory, "history"), ReadLine.GetHistory());

            File.WriteAllLines(Path.Combine(saveDirectory, "breaks"), _breaks);

            debug.Outputs.General.Lines.Add(new OutputLine($"Session saved to {saveDirectory}"));
            return true;
        }

        public async Task<bool> Load(CoreDebug debug, string fullCommand)
        {
            string name = GetName(fullCommand);
            string saveDirectory = GetSaveDirectory(name);

            ReadLine.AddHistory(File.ReadAllLines(Path.Combine(saveDirectory, "history")));
            _breaks = File.ReadAllLines(Path.Combine(saveDirectory, "breaks"))
                .ToList();

            foreach(string breakCommand in _breaks)
            {
                string commandResult = await debug.SendCommand(breakCommand);
            }

            debug.Outputs.General.Lines.Add(new OutputLine($"Session loaded"));
            return true;
        }

        internal string SetBreak(CoreDebug debug, string fullCommand)
        {
            _breaks.Add(fullCommand);
            return fullCommand;
        }
    }
}
