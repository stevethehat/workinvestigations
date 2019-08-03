using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DblDebug
{
    public class Command
    {
        public string Name { get; set; } = ":unknown";
        public List<string> AlternateNames { get; set; } = new List<string>();
        public List<string> SubCommands { get; set; } = new List<string>();
        public CommandType CommandType { get; set; }
        public Func<State, IEnumerable<string>> SubOptions { get; set; }
        public Func<CoreDebug,string,  bool> Action { get; set; } = (d, c) => {
            d.Outputs.General.Lines.Add(new OutputLine($"Unknown command type {c}", ConsoleColor.Red));
            return true;
        };
        public Func<List<string>, List<string>> ResponsePreProcess { get; set; } = (r) => r;

        public bool IsInternal { get => Name.StartsWith(":"); }

        internal bool Execute(CoreDebug debug, string fullCommand)
        {
            bool result = true;
            if(default(Func<CoreDebug, string, bool>) != Action)
            {
                result = Action(debug, fullCommand);
            }
            return result;
        }
    }

    public enum CommandType
    {
        Info,
        Navigation
    }

    public class Commands
    {
        public Commands()
        {
            IEnumerable<char> alphabet = Enumerable.Range('a', 26).Select(x => (char)x);

            MainCommandNames = MainCommands.Select(s => s.Name);
            MainCommandNamesByLength = MainCommandNames.OrderByDescending(n => n.Length);
        }
        public Command GetCommand(string command)
        {
            Command result = new Command();

            Match match = Regex.Match(command, @"^([a-zA-Z0-9_]+)\s*");
            if(default(Match) != match && match.Success)
            {
                command = match.Groups[1].Value;
            }

            Command foundCommand = MainCommands.Find(c => 
                (command == c.Name) || c.AlternateNames.Contains(command)
            );

            if(default(Command) != foundCommand)
            {
                result = foundCommand;
            }
            return result;
        }

        public readonly IEnumerable<string> MainCommandNamesByLength;
        public readonly IEnumerable<string> MainCommandNames;
        public readonly List<Command> MainCommands = new List<Command>()
            {
                new Command() {
                    Name = "break",
                    AlternateNames = new List<string>(){ "b" },
                    SubOptions = (s) => s.DblSourceFile.BreakLocations
                                        .Concat(s.CurrentScope.Labels)
                },
                new Command() { Name = "cancel" },
                new Command() { Name = "delete" },
                new Command() {
                    Name = "deposit",
                    SubOptions = (s) => s.CurrentScope.Variables
                },
                new Command() {
                    Name = "examine",
                    AlternateNames = new List<string>(){ "e" },
                    SubOptions = (s) => s.CurrentScope.Variables,
                    ResponsePreProcess = (r) => Processors.PreProcessExamine(r)
                },
                new Command() { Name = "exit" },
                new Command() {
                    Name = "go",
                    AlternateNames = new List<string>() { "g" },
                    CommandType = CommandType.Navigation
                },
                new Command() { Name = "help" },
                new Command() { Name = "list" },
                new Command() { Name = "logging" },
                new Command() { Name = "openelb" },
                new Command() { Name = "quit" },
                new Command() { Name = "save" },
                new Command() { Name = "screen" },
                new Command() { Name = "search" },
                new Command() {
                    Name = "set",
                    AlternateNames = new List<string>(){ "se" }
                },
                new Command() {
                    Name = "show",
                    SubCommands = new List<string>()
                    {
                        "break", "channels", "classes", "dbgsrc", "dll", "dynmem", "elb", "error", "memory",
                        "options", "stack", "step", "stop", "trace", "trap", "variable", "watch"
                    }
                },
                new Command() {
                    Name = "step",
                    AlternateNames = new List<string>() { "s" },
                    CommandType = CommandType.Navigation
                },
                new Command() {
                    Name = "trace",
                    AlternateNames = new List<string>() { "b" }
                },
                new Command() {
                    Name = "view",
                    AlternateNames = new List<string>() { "v" }
                },
                new Command() {
                    Name = "watch",
                    AlternateNames = new List<string>() { "w" }
                },
                new Command() {
                    Name = ":peek",
                    SubOptions = (s)
                        => s.DblSourceFile.BreakLocations
                            .Concat(s.CurrentScope.Labels)
                },
                new Command() {
                    Name = ":save",
                    Action = (d, c) => d.State.Save(d)
                },
                new Command() {
                    Name = ":load",
                    Action = (d, c) => d.State.Load(d)
                },
                new Command() {
                    Name = ":scope",
                    Action = (d, c) => {
                        if(default(Scope) != d.State.CurrentScope)
                        {
                            d.State.CurrentScope.Info(d.Outputs.General);
                        }
                        return true;
                    }
                },
                new Command() {
                    Name = ":quit",
                    Action = (d, c) => false
                },
        };

    }
}
