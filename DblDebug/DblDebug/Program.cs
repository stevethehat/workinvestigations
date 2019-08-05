using Microsoft.Extensions.CommandLineUtils;
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

            var app = new CommandLineApplication(false);
            app.Name = "DblDebug";
            app.HelpOption("-?|--help");
            app.ExtendedHelpText = ":q to quit.";

            CommandOption hostOption = app.Option(
                "-h|--host <host>",
                "Host",
                CommandOptionType.SingleValue);

            CommandOption portOption = app.Option(
                "-p|--port <port>",
                "Port",
                CommandOptionType.SingleValue);

            CommandOption sourceDirectoryOption = app.Option(
                "-s|--source <source>",
                "Source code directory",
                CommandOptionType.SingleValue);

            CommandOption modeOption = app.Option(
                "-m|--mode <mode>",
                "Mode test/live (default)",
                CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                string host = "172.16.128.21";
                int port = 1024;
                string sourceDirectory = default(string);

                if (hostOption.HasValue())
                {
                    host = hostOption.Value();
                }

                if (portOption.HasValue())
                {
                    port = Convert.ToInt32(portOption.Value());
                }

                if (sourceDirectoryOption.HasValue())
                {
                    sourceDirectory = sourceDirectoryOption.Value();
                }

                IClient client = default(SocketClient);
                if (modeOption.HasValue() && "test" == modeOption.Value())
                {
                    client = new TestClient(host, 1024);
                }
                else
                {
                    client = new SocketClient(host, 1024); 
                }
                
                CoreDebug debug = new CoreDebug(sourceDirectory);

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

                return 0;
            });

            app.Execute(args);
        }

        private static async Task<bool> GoAsync(IClient client, CoreDebug debug)
        {
            bool startResponse = false;
            string input = null;
            bool response = true;
            while (false != response)
            {
                try
                {
                    if(false == debug.Connected){
                        startResponse = await debug.Start(client);
                    }
                    input = ReadLine.Read("DBG>");

                    if (true == string.IsNullOrEmpty(input))
                    {
                        input = debug.State.LastEnteredCommand;
                    }

                    response = await debug.Command(input);

                    debug.Outputs.Code.Write();
                    debug.Outputs.General.Write();
                }
                catch(System.Net.Sockets.SocketException se)
                {
                    OutputLine.WriteLine("Connection error. Start GOLD and try again.", foregroundColor: ConsoleColor.Red);
                    Console.ReadKey();
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
