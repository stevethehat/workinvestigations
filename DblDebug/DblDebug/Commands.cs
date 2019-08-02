using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DblDebug
{
    public class Command
    {
        public string Name { get; set; }
        public List<string> SubCommands { get; set; } = new List<string>();
        public CommandType CommandType { get; set; }
        public Func<State, IEnumerable<string>> SubOptions { get; set; }
        public Func<CoreDebug, bool> Action { get; set; }

        internal bool Execute(CoreDebug debug)
        {
            bool result = true;
            if(default(Func<CoreDebug, bool>) != Action)
            {
                result = Action(debug);
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
            MainCommandNames = MainCommands.Select(s => s.Name);
            MainCommandNamesByLength = MainCommandNames.OrderByDescending(n => n.Length);
        }
        public Command GetCommand(string name)
        {
            Command result = new Command();
            Command command = MainCommands.Find(c => name == c.Name);
            if(default(Command) != command)
            {
                result = command;
            }
            return result;
        }

        public readonly IEnumerable<string> MainCommandNamesByLength;
        public readonly IEnumerable<string> MainCommandNames;
        public readonly List<Command> MainCommands = new List<Command>()
            {
                new Command() {
                    Name = "break",
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
                    SubOptions = (s) => s.CurrentScope.Variables
                },
                new Command() { Name = "exit" },
                new Command() {
                    Name = "go",
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
                new Command() { Name = "set" },
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
                    CommandType = CommandType.Navigation
                },
                new Command() { Name = "trace" },
                new Command() { Name = "view" },
                new Command() { Name = "watch" },
                new Command() {
                    Name = ":peek",
                    SubOptions = (s)
                        => s.DblSourceFile.BreakLocations
                            .Concat(s.CurrentScope.Labels)
                },
                new Command() { Name = ":save" },
                new Command() { Name = ":load" },
                new Command() {
                    Name = ":scope",
                    Action = (d) => {
                        d.State.CurrentScope.Info(d.Outputs.General);
                        return true;
                    }
                },
                new Command() {
                    Name = ":quit",
                    Action = (d) => false
                },
        };

    }
}
