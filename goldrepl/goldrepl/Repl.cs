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
using Microsoft.Scripting.Hosting.Providers;

namespace GoldRepl
{
    public partial class Repl
    {
        protected readonly ScriptEngine     _python;
        protected readonly ScriptScope      _scope;
        //protected readonly ConsoleOutput    _console;

        //public ConsoleOutput Console { get => _console; }
        private readonly SyntaxHighlight _syntaxHighlighter;
        
        public Repl()
        {
            //_console = new ConsoleOutput();
            _python = Python.CreateEngine();
            _scope = _python.CreateScope();

            _syntaxHighlighter = new SyntaxHighlight();

            _scope.ImportModule("clr");
            _scope.ImportModule("sys");
            _python.Execute("import clr");
            _python.Execute("import sys");
            string setPath = $"sys.path.append(\"{ Environment.GetEnvironmentVariable("PYTHONPATH")}\")";
            RunCode(setPath);
            _python.Execute("clr.AddReference(\"goldrepl\")", _scope);

            _python.Execute("clr.AddReference(\"System\")", _scope);
            //_python.Execute("clr.AddReference(\"Gold\")", _scope);
            _python.Execute("clr.AddReference(\"System.Core\")", _scope);
            //_python.Execute("clr.AddReference(\"GoldApiServer.DataLayer\")", _scope);
            //_python.Execute("from Net.Ibcos.GoldAPIServer.DataLayer.Models import *", _scope);

            // suddenly started getting a really odd error on import System
            // https://stackoverflow.com/questions/33896154/ironpython-cannot-import-os-importexception-not-a-zipfile
            var pc = HostingHelpers.GetLanguageContext(_python) as PythonContext;
            var hooks = pc.SystemState.Get__dict__()["path_hooks"] as List;
            hooks.Clear();

            _python.Execute("import System", _scope);
            _python.Execute("from System import Linq", _scope);
            _python.Execute("clr.ImportExtensions(System.Linq)", _scope);
        }

        public void InitData(string dataFolder = "~/gold/data")
        {
            Console.WriteLine($"Data= {dataFolder}");
            try
            {
                //Gold.Gold gold = new Gold.Gold(dataFolder);
                //_console.Lines.Add(new OutputLine("Gold Initialized", ConsoleColor.Yellow));

                //_scope.SetVariable("gold", gold);
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
                        
            _python.Execute("from GoldRepl import *", _scope);
            Console.WriteLine("imported *");

            Isams isams = new Isams();
            _scope.SetVariable("isams", isams);
            _scope.SetVariable("scope", _scope);
            _scope.SetVariable("repl", this);

            RunCode(@"
def output(value):
    repl.Output(value)
            ");

            RunCode(@"
def info(value):
    repl.Info(value)
            ");

            RunCode(@"
def save(path):
    repl.Save(path)
            ");

            RunCode(@"
def load(path):
    repl.Load(path)
            ");

            Console.WriteLine("ISAMS Initialized");
            //Console.Write(true);
        }

        internal void Execute(string scriptFile)
        {
            ScriptSource source = _python.CreateScriptSourceFromFile(scriptFile);
            RunCode(source, "");
        }

        public void RunInteractive()
        {
            ReadLine.HistoryEnabled = true;
            ReadLine.AutoCompletionHandler = new AutoCompletionHandler(_scope);

            ReadLine.Output = text =>
            {
                _syntaxHighlighter.WriteToConsole(text);

            };

            System.Console.WriteLine("Gold Interactive Repl");
            System.Console.WriteLine("=====================");
            System.Console.WriteLine(":q to quit.");
            System.Console.WriteLine("");


            string code = "";
            while (false == code.StartsWith(":q\n"))
            {
                if (false == string.IsNullOrEmpty(code))
                {
                    ScriptSource source = _python.CreateScriptSourceFromString(code);
                    RunCode(source, code);
                }
                code = GetCode();
                //_syntaxHighlighter.WriteToConsole(code);
                //_console.Write(true);
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
