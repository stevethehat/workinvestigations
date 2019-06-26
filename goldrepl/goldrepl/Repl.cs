using System;
using System.Collections.Generic;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace GoldRepl
{
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
            _python.Execute("clr.AddReference(\"goldrepl\")", _scope);
            _python.Execute("from GoldRepl import *", _scope);
            
            Isams isams = new Isams();
            _scope.SetVariable("isams", isams);
        }

        public void RunInteractive()
        {
            string code = "";
            while (code != "q\n")
            {
                if (false == string.IsNullOrEmpty(code))
                {
                    ScriptSource source = _python.CreateScriptSourceFromString(code);
                    try
                    {
                        dynamic result = source.Execute(_scope);
                        /*
                        if(null != result)
                        {
                            Console.WriteLine(result);
                        }
                        */
                        //Console.WriteLine(Convert.ToString(result));
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
            string result = "";
            ConsoleKeyInfo key = new ConsoleKeyInfo();

            while (ConsoleKey.Enter != key.Key)
            {
                bool echoChar = true;
                if('\t' == key.KeyChar)
                {
                    result += "hello";
                    Console.Write("hello");
                    echoChar = false;
                }
                if ('\0' != key.KeyChar)
                {
                    result = result + key.KeyChar;
                }
                if (echoChar)
                {
                    Console.Write(key.KeyChar);
                }
                key = Console.ReadKey(true);
            }

            Console.Write(key.KeyChar);

            return result;
        }
    }
}
