using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DblDebug
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            OutputLine.WriteLine("DblDebugger");
            OutputLine.WriteLine("");

            CoreDebug debug = new CoreDebug("172.16.128.21", 1024);

            //Test(debug);
            //return;

            ReadLine.HistoryEnabled = true;
            ReadLine.AutoCompletionHandler = new AutoCompleteHandler(debug);

            try
            {
                var result = GoAsync(debug).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                OutputLine.WriteLine(e.Message, foregroundColor: ConsoleColor.Red);
            }

            OutputLine.WriteLine("Done..");
            Console.ReadKey();  
        }

        private static async Task<bool> GoAsync(CoreDebug debug)
        {
            bool startResponse = await debug.Start();
            string input = null;
            bool response = true;
            while (false != response)
            {
                input = ReadLine.Read("DBG> ");

                if(true == string.IsNullOrEmpty(input))
                {
                    input = debug.LastCommand;
                }

                response = await debug.Command(input);

                debug.Outputs.Code.Write();
                debug.Outputs.General.Write();
            }

            return startResponse;
        }

        private static void Test(CoreDebug debug)
        {
            debug.ProcessResponse
("g", @"Break at 462 in WHGINE (WHGINE.DBL) on entry

    462 >       a = 4
DBG> 
");
            debug.Outputs.General.Write();

            debug.ProcessResponse("g", "Break at 462 in WHGINE (WHGINE.DBL)\r\n");
            debug.Outputs.General.Write();

            debug.ProcessResponse("s", "Step to 4207 in WHGINE_PROC_HDR (WHGINE.DBL)\r\n");
            Scope scope = debug.State.CurrentScope;
            scope.Info(debug.Outputs.General);
            debug.Outputs.General.Write();

            debug.Outputs.Code.Write();

            Command test = debug.Commands.GetCommand("e test");
            Console.Write($"command name = '{test.Name}'");

            Console.ReadKey();
        }

    }
}
