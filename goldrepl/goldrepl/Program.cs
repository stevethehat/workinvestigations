using System;

namespace GoldRepl
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Repl repl = new Repl();
            repl.Run();
        }
    }
}
