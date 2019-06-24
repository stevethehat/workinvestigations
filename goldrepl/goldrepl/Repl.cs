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

            string code = "print \"hello lets explore\"";
            while (code != "q")
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

                code = Console.ReadLine();
            }

        }
    }
}
