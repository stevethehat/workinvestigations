using System;
using System.Collections.Generic;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace GoldRepl
{
    public class Isams
    {
        public void Write(string output)
        {
            Console.WriteLine($"write >> {output}");
        }
        public Dictionary<string, object> Get(string isam)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add("one", 1);
            result.Add("s", "a string");

            return result;
        }
    }

    public class Repl
    {
        private readonly ScriptEngine _python;
        private readonly ScriptScope _scope;

        public Repl()
        {
            _python = Python.CreateEngine();
            _scope = _python.CreateScope();

            _scope.ImportModule("clr");
            _python.Execute("import clr");
            _python.Execute("clr.AddReference(\"ironpython\")", _scope);
            
            Isams isams = new Isams();
            _scope.SetVariable("isams", isams);

            //_python.Execute("def func():");
            //_python.Execute("\tisams.Write(\"hi\")\n");
            //_python.Execute("\tpass");
        }
        public void Run()
        {
            Console.WriteLine("Gold Repl");
            string code = "";
            while (code != "q\n")
            {
                if (false == string.IsNullOrEmpty(code))
                {
                    ScriptSource source = _python.CreateScriptSourceFromString(code);
                    try
                    {
                        dynamic result = source.Execute(_scope);
                    }
                    catch (IronPython.Runtime.UnboundNameException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
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
                result = $"{result}{"".PadLeft(indent * 2, ' ')}{line}\n";

                if (line.EndsWith(":", StringComparison.InvariantCulture))
                {
                    indent++;
                }
                else
                {
                    if(true == string.IsNullOrEmpty(line))
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
            string result = "";
            ConsoleKeyInfo key = new ConsoleKeyInfo();

            while (ConsoleKey.Enter != key.Key)
            {
                if ('\0' != key.KeyChar)
                {
                    result = result + key.KeyChar;
                }
                key = Console.ReadKey();
            }
            return result;
        }
    }
}
