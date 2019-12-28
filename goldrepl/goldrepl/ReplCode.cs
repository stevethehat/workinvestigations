using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldRepl
{
    public partial class Repl
    {
        public void ExecuteFile(string fullPath)
        {
            ScriptSource source = _python.CreateScriptSourceFromFile(fullPath);
            RunCode(source, "");
        }

        protected dynamic RunCode(string code)
        {
            ScriptSource source = _python.CreateScriptSourceFromString(code);
            return RunCode(source, code);
        }

        protected dynamic RunCode(ScriptSource source, string code)
        {
            dynamic result = new { };
            try
            {
                result = source.Execute(_scope);

                if (null != result)
                {
                    source = _python.CreateScriptSourceFromString("output(" + code + ")");
                    source.Execute(_scope);
                    //_console.Write(true);
                }
            }
            catch (IronPython.Runtime.UnboundNameException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

    }
}
