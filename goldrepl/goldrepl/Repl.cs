using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Microsoft.CSharp;
using IronPython.Runtime;

namespace GoldRepl
{
    public partial class Repl
    {
        protected readonly ScriptEngine _python;
        protected readonly ScriptScope _scope;

        public Repl()
        {
            _python = Python.CreateEngine();
            _scope = _python.CreateScope();

            _scope.ImportModule("clr");
            _python.Execute("import clr");
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
            Console.WriteLine($"Data= {dataFolder}");
            //Assembly assembly = 
            try
            {
                Gold.Gold gold = new Gold.Gold(dataFolder);
                Console.WriteLine("Gold Initialized");

                _scope.SetVariable("gold", gold);
            } catch(Exception e)
            {
                Console.Write(e.Message);
                Console.Write(e.StackTrace);

                //throw e;
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

            Console.WriteLine("isams initialized");
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
            }
        }

        protected string GetCode()
        {
            string result = "";
            bool codeComplete = false;
            int indent = 0;

            while(false == codeComplete)
            {
                Console.Write("".PadLeft(indent, '>'));
                if(indent > 0)
                {
                    Console.Write(" ");
                }

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
            return ReadLine.Read(">");
        }
    }
}
