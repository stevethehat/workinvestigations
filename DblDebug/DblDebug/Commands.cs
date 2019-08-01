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
        public List<string> SubCommands { get; set; }
        public CommandType CommandType { get; set; }
    }

    public enum CommandType
    {
        Info,
        Navigation
    }

    public class Commands
    {
        public readonly List<Command> MainCommands = new List<Command>()
            {
                new Command() { Name = "break" },
                new Command() { Name = "cancel" },
                new Command() { Name = "delete" },
                new Command() { Name = "deposit" },
                new Command() { Name = "examine" },
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
                new Command() { Name = "traceiew" },
                new Command() { Name = "watch" },
            };

    }
}
