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

        private static async Task<bool> GoAsync()
        {
            CoreDebug debug = new CoreDebug("172.16.128.21", 1024);

            bool startResponse = await debug.Start();
            string input = null;
            ConsoleOutput response = new ConsoleOutput();
            while (default(ConsoleOutput) != response)
            {
                input = ReadLine.Read();

                response = await debug.Command(input);

                response.Write();
            }

            return startResponse;
        }
    }
}
