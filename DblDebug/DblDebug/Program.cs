using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DblDebug
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            OutputLine.WriteLine("DblDebugger", foregroundColor: ConsoleColor.Yellow);

            //Test();
            ReadLine.HistoryEnabled = true;
            try
            {
                var result = GoAsync().GetAwaiter().GetResult();

            }
            catch (Exception e)
            {
                OutputLine.WriteLine(e.Message, foregroundColor: ConsoleColor.Red);
            }

            OutputLine.WriteLine("Done..");
            Console.ReadKey();  
        }

        private static void Test()
        {
            CoreDebug debug = new CoreDebug("172.16.128.21", 1024);

            debug.ProcessResponse
(@"Break at 462 in WHGINE (WHGINE.DBL) on entry

    462 >       a = 4
DBG> 
");
            debug.Outputs.General.Write();

            debug.ProcessResponse("Break at 462 in WHGINE (WHGINE.DBL)\r\n");
            debug.Outputs.General.Write();

            debug.ProcessResponse("Step to 10001 in WHGINE (WHGINE.DBL)\r\n");
            debug.Outputs.General.Write();

            debug.Outputs.Code.Write();

        }

        private static async Task<bool> GoAsync()
        {
            CoreDebug debug = new CoreDebug("172.16.128.21", 1024);

            bool startResponse = await debug.Start();
            string input = null;
            bool response = true;
            while (false != response)
            {
                input = ReadLine.Read("DBG> ");

                response = await debug.Command(input);

                debug.Outputs.Code.Write();
                debug.Outputs.General.Write();
            }

            return startResponse;
        }
    }
}
