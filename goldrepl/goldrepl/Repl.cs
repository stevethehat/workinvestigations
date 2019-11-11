using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Microsoft.CSharp;
using IronPython.Runtime;
using System.IO;

namespace GoldRepl
{
    public partial class Repl
    {
        protected readonly ScriptEngine     _python;
        protected readonly ScriptScope      _scope;
        protected readonly ConsoleOutput    _console;

        protected ConsoleOutput Console { get => _console; }


        public Repl()
        {
            _console = new ConsoleOutput();
            _python = Python.CreateEngine();
            _scope = _python.CreateScope();

            _scope.ImportModule("clr");
            _python.Execute("import clr");
            _python.Execute("import sys");
            string setPath = $"sys.path.append(\"{ Environment.GetEnvironmentVariable("PYTHONPATH")}\")";
            RunCode(setPath);
            _python.Execute("clr.AddReference(\"goldrepl\")", _scope);
            ReadLine.HistoryEnabled = true;
            ReadLine.AutoCompletionHandler = new AutoCompletionHandler(_scope);

            _python.Execute("clr.AddReference(\"System\")", _scope);
            _python.Execute("clr.AddReference(\"Gold\")", _scope);
            _python.Execute("clr.AddReference(\"System.Core\")", _scope);
            _python.Execute("clr.AddReference(\"GoldApiServer.DataLayer\")", _scope);
            _python.Execute("from Net.Ibcos.GoldAPIServer.DataLayer.Models import *", _scope);

            _python.Execute("import System", _scope);
            _python.Execute("from System import Linq", _scope);
            _python.Execute("clr.ImportExtensions(System.Linq)", _scope);
        }

        public void InitData(string dataFolder = "~/gold/data")
        {
            _console.Lines.Add(new OutputLine($"Data= {dataFolder}"));
            try
            {
                Gold.Gold gold = new Gold.Gold(dataFolder);
                _console.Lines.Add(new OutputLine("Gold Initialized", ConsoleColor.Yellow));

                _scope.SetVariable("gold", gold);
            } catch(Exception e)
            {
                _console.Lines.Add(new OutputLine(e.Message, ConsoleColor.Red, ConsoleColor.Black));
                _console.Lines.Add(new OutputLine(e.StackTrace, ConsoleColor.Red, ConsoleColor.Black));
            }
                        
            _python.Execute("from GoldRepl import *", _scope);
            _console.Lines.Add(new OutputLine("imported *", ConsoleColor.Yellow));

            Isams isams = new Isams();
            _scope.SetVariable("isams", isams);
            _scope.SetVariable("scope", _scope);
            _scope.SetVariable("repl", this);

            RunCode(@"
def output(value):
    repl.Output(value)
            ");

            RunCode(@"
def save(path):
    repl.Save(path)
            ");

            RunCode(@"
def load(path):
    repl.Load(path)
            ");

            _console.Lines.Add(new OutputLine("ISAMS Initialized", ConsoleColor.Yellow));
            _console.Write(true);
        }

        internal void Execute(string scriptFile)
        {
            ScriptSource source = _python.CreateScriptSourceFromFile(scriptFile);
            RunCode(source, "");
        }

        public void RunInteractive()
        {
            string code = "";
            while (false == code.StartsWith(":q\n"))
            {
                if (false == string.IsNullOrEmpty(code))
                {
                    ScriptSource source = _python.CreateScriptSourceFromString(code);
                    RunCode(source, code);
                }
                code = GetCode();
                _console.Write(true);
            }
        }

        protected string GetCode()
        {
            string result = "";
            bool codeComplete = false;
            int indent = 0;

            while(false == codeComplete)
            {
                //_console.Lines.Add(new OutputLine("".PadLeft(indent, '>')));
                System.Console.Write("".PadLeft(indent, '>'));
                if(indent > 0)
                {
                    //_console.Lines.Add(new OutputLine(" "));
                    System.Console.Write(" ");
                }
                //_console.Write();
                string line = GetCodeLine();
                result = $"{result}{"".PadLeft(indent * 2, ' ')}{line}{Environment.NewLine}";

                if (line.EndsWith(":", StringComparison.InvariantCulture))
                {
                    indent++;
                }
                else
                {
                    if(true == string.IsNullOrEmpty(line) && indent > 0)
                    {
                        indent--;
                    }
                    codeComplete = 0 == indent;
                }

            }
            return result;
        }

        protected string GetCodeLine()
        {
            return ReadLine.Read("");
        }

        public void Save(string path)
        {
            var history = ReadLine.GetHistory();
                
            var historyStr = string.Join("\n", history);

            if(true == File.Exists(path))
            {
                File.Delete(path);
            }

            File.WriteAllText(path, historyStr);
        }

        public void Load(string path)
        {
            if(true == File.Exists(path))
            {
                string code = File.ReadAllText(path);
                RunCode(code);
                ReadLine.AddHistory(code.Split('\n'));

            }
            else
            {
                System.Console.WriteLine($"{path} Not found");
            }
        }
    }
}
