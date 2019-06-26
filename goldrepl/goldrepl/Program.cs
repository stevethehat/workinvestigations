using System;
using System.IO;
using Microsoft.Extensions.CommandLineUtils;

namespace GoldRepl
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Repl repl = new Repl();
            var app = new CommandLineApplication(false);
            app.Name = "goldrepl";
            app.ExtendedHelpText = "q to quit.";
            app.HelpOption("-?|--help");

            CommandArgument script = app.Argument("script", "The sript to run.", false);

            CommandOption initFile = app.Option(
                "-i |--init <init>",
                "The init file to use.",
                CommandOptionType.SingleValue);


            app.OnExecute(() =>
            {
                if (initFile.HasValue())
                {
                    string path = initFile.Value();
                    string fullPath = Path.GetFullPath(path);

                    Console.WriteLine($"Init {fullPath}");

                    repl.Init(fullPath);

                }

                if (false == string.IsNullOrEmpty(script.Value))
                {
                    string scriptFile = script.Value;
                    string outputFile = Path.GetFileName(scriptFile);

                    Console.WriteLine($"Running {scriptFile} ...");

                }
                else
                {
                    Console.WriteLine("Gold Interactive Repl");
                    Console.WriteLine("=====================");
                    Console.WriteLine("q to quit.");
                    Console.WriteLine("");


                    repl.RunInteractive();
                }
                return 0;
            });

            app.Execute(args);
        }
    }
}
