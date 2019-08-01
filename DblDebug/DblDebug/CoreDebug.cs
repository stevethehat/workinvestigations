using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using PrimS.Telnet;

namespace DblDebug
{
    public class LineProcessor
    {
        public Regex MatchRegex { get; set; }
        public Func<string, Match, bool> Processor { get; set; }
        public Func<string, Match, string> Formatter { get; set; }

        public LineProcessor(Regex matchRegex, Func<string, Match, bool> processor, Func<string, Match, string> formatter)
        {
            MatchRegex = matchRegex;
            Processor = processor;
            Formatter = formatter;
        }

        public LineProcessor()
        {
                
        }
    }
    public class CoreDebug: IDisposable
    {
        private readonly Func<bool, string> _input;
        private readonly string _host;
        private readonly int _port;
        private readonly PrimS.Telnet.Client _client;
        private string _currentFile = "";
        private int _currentLine = 0;
        private List<string> _response;

        private List<LineProcessor> _lineProcessors = new List<LineProcessor>();

        private bool ProcessLine(string line, Match match)
        {
            return true;
        }
        private string FormatLine(string line, Match match)
        {
            return "";
        }

        private bool CheckLine(string line)
        {
            foreach(LineProcessor lineProcessor in _lineProcessors)
            {
                Match match = lineProcessor.MatchRegex.Match(line);
                if(default(Match) != match && true == match.Success)
                {
                    lineProcessor.Processor(line, match);
                    lineProcessor.Formatter(line, match);
                }
            }
            return true;
        }

        public CoreDebug(string host, int port)
        {
            _host = host;
            _port = port;
            _response = new List<string>();
            _client = new Client(_host, _port, new CancellationToken());

            _lineProcessors = new List<LineProcessor>()
            {
                new LineProcessor(new Regex(@"Break at (\d*) in ([A-Z]*) \(([A-Z]*\.[A-Z]*)\)"),
                   (l, m) => ProcessLine(l, m),
                   (l, m) => FormatLine(l, m)
                ),
                new LineProcessor(new Regex(@"Step to (\d*) in ([A-Z]*) \(([A-Z]*\.[A-Z]*)\)"),
                   (l, m) => ProcessLine(l, m),
                   (l, m) => FormatLine(l, m)
                )
            };
        }
        
        public async Task<bool> Start()
        {
            bool result = false;
            string response = default(string);
            response = await GetResponse();
            Console.WriteLine(response);

            ConsoleOutput consoleOutput = await Command("se st ov");
            consoleOutput.Write();

            consoleOutput = await Command("s");
            consoleOutput.Write();

            return result;
        }

        public async Task<ConsoleOutput> Command(string command)
        {
            string Trim(string line){
                return line.TrimEnd(new char[] { '\r', '\n' });
            }

            ConsoleOutput result = new ConsoleOutput();
            if("q" == command)
            {
                result = default(ConsoleOutput);
            }
            else
            {
                if(false == string.IsNullOrEmpty(command))
                {
                    string commandResult = await SendCommand(command);
                    string trimmedCommandResult = Trim(commandResult);
                    //foreach(string line in )
                    result.Lines = trimmedCommandResult.Split('\n')
                        .Select(l => new OutputLine(l));
                }
            }
            return result;
        }

        protected async Task<string> GetResponse()
        {
            return await _client.TerminatedReadAsync("DBG>", TimeSpan.FromHours(1));
        }

        protected async Task<string> SendCommand(string command)
        {
            await _client.Write($"{command}\n");
            return await GetResponse();
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _client.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CoreDebug()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
