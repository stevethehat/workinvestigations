using System;
using System.IO;
using Microsoft.Extensions.CommandLineUtils;

namespace GoldRepl
{
    class MainClass
    {
        // ../../test.py --notinit=../../init.py -d="C:\ibcos\Repositorys\golddata\gold\data"
        /// <summary>
        ///
        /// </summary>
        /// <param name="args"></param>
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

            CommandOption dataFolder = app.Option(
                "-d |--data <data>",
                "The data folder to use.",
                CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                /*
                if (dataFolder.HasValue())
                {
                    repl.InitData(dataFolder.Value());
                } else
                {
                    repl.InitData();
                }
                */
                repl.InitData("C:\\ibcos\\Repositorys\\golddata\\gold\\data");
                //repl.InitData();

                if (initFile.HasValue())
                {
                    string path = initFile.Value();
                    string fullPath = Path.GetFullPath(path);

                    Console.WriteLine($"Init {fullPath}");

                    repl.ExecuteFile(fullPath);
                }

                if (false == string.IsNullOrEmpty(script.Value))
                {
                    string scriptFile = script.Value;
                    string fullPath = Path.GetFullPath(scriptFile);
                    
                    Console.WriteLine($"Running {fullPath} ...");
                    repl.Execute(fullPath);

                    Console.WriteLine("Press any key to continue..");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Gold Interactive Repl");
                    Console.WriteLine("=====================");
                    Console.WriteLine(":q to quit.");
                    Console.WriteLine("");


                    repl.RunInteractive();
                }
                return 0;
            });

            app.Execute(args);
        }
    }
}
