using System;
using System.IO;
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
        public Scope CurrentScope { get; set; }
        public DblSourceFile DblSourceFile { get; set; }

        public string SaveFolder { get; set; } = Path.Combine("~/", ".dbldebug");
        public string SourceDirectory { get; internal set; }
        public string HomeDirectory { get; }
        public string StateName { get; internal set; }

        public State(string sourceDirectory)
        {
            SourceDirectory = sourceDirectory;
            HomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }

        public async Task<bool> Save(CoreDebug debug, string fullCommand)
        {
            string name = "unknown";

            Match match = Regex.Match(fullCommand, @"^([a-zA-Z0-9_:]+)\s*([a-zA-Z0-9_:]+)");
            if (default(Match) != match && match.Success)
            {
                name = match.Groups[2].Value;
            }

            string saveDirectory = Path.Combine(HomeDirectory, ".dbldebug", name);
            if (false == Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            File.WriteAllLines(Path.Combine(saveDirectory, "history"), ReadLine.GetHistory());

            string[] breaks = await debug.GetResponseFromCommand("show break");
            File.WriteAllLines(Path.Combine(saveDirectory, "breaks"), breaks);

            debug.Outputs.General.Lines.Add(new OutputLine($"Session saved to {saveDirectory}"));
            return true;
        }

        public async Task<bool> Load(CoreDebug debug, string fullCommand)
        {
            debug.Outputs.General.Lines.Add(new OutputLine($"Session loaded"));
            return true;
        }
    }
}
