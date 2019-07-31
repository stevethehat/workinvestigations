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
    public class CoreDebug: IDisposable
    {
        private readonly Func<bool, string> _input;
        private readonly string _host;
        private readonly int _port;
        private PrimS.Telnet.Client _client;
        private List<string> _response;

        public Outputs Outputs { get; set; } = new Outputs();
        public State State { get; private set; }

        private List<LineProcessor> _lineProcessors = new List<LineProcessor>();

        private bool CheckLine(string line)
        {
            foreach(LineProcessor lineProcessor in _lineProcessors)
            {
                Match match = lineProcessor.MatchRegex.Match(line);
                if(default(Match) != match && true == match.Success)
                {
                    State = lineProcessor.Processor(State, line, match);
                    Outputs.General.Lines.Add(lineProcessor.Formatter(line, match));
                    break;
                }
            }
            return true;
        }

        public CoreDebug(string host, int port)
        {
            _host = host;
            _port = port;
            _response = new List<string>();

            _lineProcessors = new List<LineProcessor>()
            {
                new LineProcessor(new Regex(@"(Break at) (\d*) in ([A-Z]*) \(([A-Z]*\.[A-Z]*)\)(.*)"),
                   (s, l, m) => Processors.LineNumber(s, l, m),
                   (l, m) => Formatters.LineNumber(l, m)
                ),
                new LineProcessor(new Regex(@"(Step to) (\d*) in ([A-Z]*) \(([A-Z]*\.[A-Z]*)\)"),
                   (s, l, m) => Processors.LineNumber(s, l, m),
                   (l, m) => Formatters.LineNumber(l, m)
                ),
                new LineProcessor(new Regex(@"(\d*) > (.*)"),
                   (s, l, m) => Processors.Default(s, l, m),
                   (l, m) => Formatters.CodeLine(l, m)
                ),
                new LineProcessor(new Regex(@".*"),
                   (s, l, m) => Processors.Default(s, l, m),
                   (l, m) => new OutputLine(l)
                )
            };
        }
        
        public async Task<bool> Start()
        {
            bool result = false;
            _client = new Client(_host, _port, new CancellationToken());

            string response = default(string);
            response = await GetResponse();
            Console.WriteLine(response);
            response = await SendCommand("se st ov");
            response = await SendCommand("s");
            Console.WriteLine(response);

            return result;
        }

        public async Task<bool> Command(string command)
        {
            bool result = true;
            if ("q" == command)
            {
                result = false;
            }
            else
            {
                if(false == string.IsNullOrEmpty(command))
                {
                    string commandResult = await SendCommand(command);
                    result = ProcessResponse(commandResult);
                }
            }
            return result;
        }

        internal bool ProcessResponse(string resposne)
        {
            bool result = true;

            Outputs.General.Lines.Clear();

            string Trim(string line)
            {
                return line.TrimEnd(new char[] { '\r', '\n' });
            }

            string trimmedCommandResult = Trim(resposne);
            foreach(string line in trimmedCommandResult.Split('\n'))
            {
                CheckLine(line);
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
