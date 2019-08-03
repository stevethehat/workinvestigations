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

            IClient client = new TestClient("172.16.128.21", 1024);
            CoreDebug debug = new CoreDebug();

            //Test(debug);
            //return;

            ReadLine.HistoryEnabled = true;
            ReadLine.AutoCompletionHandler = new AutoCompleteHandler(debug);

            try
            {
                var result = GoAsync(client, debug).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                OutputLine.WriteLine(e.Message, foregroundColor: ConsoleColor.Red);
            }

            OutputLine.WriteLine("Done..");
            Console.ReadKey();  
        }

        private static async Task<bool> GoAsync(IClient client, CoreDebug debug)
        {
            bool startResponse = await debug.Start(client);
            string input = null;
            bool response = true;
            while (false != response)
            {
                try
                {
                    input = ReadLine.Read("DBG>");

                    if (true == string.IsNullOrEmpty(input))
                    {
                        input = debug.State.LastEnteredCommand;
                    }

                    response = await debug.Command(input);

                    debug.Outputs.Code.Write();
                    debug.Outputs.General.Write();
                }
                catch (Exception e)
                {
                    OutputLine.WriteLine(e.Message, foregroundColor: ConsoleColor.Red);
                    OutputLine.WriteLine(e.StackTrace, foregroundColor: ConsoleColor.Red);

                }
            }

            return startResponse;
        }

        private static void Test(CoreDebug debug)
        {
            Command go = new Command()
            {
                Name = "g",
                CommandType = CommandType.Navigation
            };
            debug.ProcessResponse
(go, @"Break at 462 in WHGINE (WHGINE.DBL) on entry

    462 >       a = 4
DBG> 
");
            debug.Outputs.General.Write();

            debug.ProcessResponse(go, "Break at 462 in WHGINE (WHGINE.DBL)\r\n");
            debug.Outputs.General.Write();

            debug.ProcessResponse(go, "Step to 4207 in WHGINE_PROC_HDR (WHGINE.DBL)\r\n");
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
