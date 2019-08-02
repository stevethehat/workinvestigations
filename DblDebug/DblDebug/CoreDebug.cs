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
        private readonly string _terminator = "";
        private PrimS.Telnet.Client _client;
        private List<string> _response;

        public Outputs Outputs { get; set; } = new Outputs();
        public State State { get; private set; } = new State();
        public Commands Commands { get; set; } = new Commands();
        public Command LastCommand { get; internal set; } = new Command();

        private List<LineProcessor> _lineProcessors = new List<LineProcessor>();

        public CoreDebug(string host, int port)
        {
            _host = host;
            _port = port;
            _response = new List<string>();

            _lineProcessors = new List<LineProcessor>()
            {
                new LineProcessor(new Regex(@"(Break at) (\d*) in ([A-Z_]*) \(([A-Z_]*\.[A-Z_]*)\)(.*)"),
                   (s, l, m)    => Processors.LineNumber(s, l, m),
                   (l, m)       => Formatters.LineNumber(l, m)
                ),
                new LineProcessor(new Regex(@"(Step to) (\d*) in ([A-Z_]*) \(([A-Z_]*\.[A-Z_]*)\)"),
                   (s, l, m)    => Processors.LineNumber(s, l, m),
                   (l, m)       => Formatters.LineNumber(l, m)
                ),
                new LineProcessor(new Regex(@"^%DBG-E-.*"),
                   (s, l, m)    => Processors.Default(s, l, m),
                   (l, m)       => Formatters.Error(l, m)
                ),
                new LineProcessor(new Regex(@"(\s+)(\d*)> (.*)"),
                   (s, l, m)    => Processors.Default(s, l, m),
                   (l, m)       => Formatters.CodeLine(l, m)
                ),
                new LineProcessor(new Regex(@".*"),
                   (s, l, m)    => Processors.Default(s, l, m),
                   (l, m)       => new OutputLine(l)
                )
            };
        }
        
        public async Task<bool> Start()
        {
            bool result = false;
            _client = new Client(_host, _port, new CancellationToken());

            string response = default(string);
            response = await _client.TerminatedReadAsync(_terminator, TimeSpan.FromSeconds(10));

            bool commandResult = await Command("se st ov");
            Outputs.General.Write();

            commandResult = await Command("s");
            Outputs.General.Write();

            return result;
        }

        public async Task<bool> Command(string enteredCommand)
        {
            bool result = true;

            if(true == string.IsNullOrEmpty(enteredCommand))
            {
                return true;
            }

            Command command = Commands.GetCommand(enteredCommand);
            if (default(Command) != command)
            {
                State.LastEnteredCommand = enteredCommand;
                if (command.Name != LastCommand.Name)
                {
                    LastCommand.Name = command.Name;
                }

                Outputs.General.Lines.Clear();
                Outputs.Code.Lines.Clear();

                if (true == command.IsInternal)
                {
                    result = ProcessInternalCommand(command);
                }
                else
                {
                    string commandResult = await SendCommand(enteredCommand);

                    result = ProcessResponse(command, commandResult);
                }
            }

            return result;
        }

        internal bool ProcessInternalCommand(Command command)
        {
            bool result = true;

            if(default(Command) != command)
            {
                result = command.Execute(this);
            }
            return result;
        }

        internal bool ProcessResponse(Command command, string resposne)
        {
            bool result = true;

            string Trim(string line)
            {
                return line.TrimEnd(new char[] { '\r', '\n' });
            }

            string trimmedCommandResult = command.ResponsePreProcess(Trim(resposne));

            foreach(string line in trimmedCommandResult.Split('\n'))
            {
                if(
                    false == line.StartsWith("DBG>") &&
                    false == string.IsNullOrEmpty(Trim(line))
                )
                {
                    ProcessLine(line);
                }
            }

            if(
                default(DblSourceFile) != State.DblSourceFile && 
                CommandType.Navigation == command.CommandType
            )
            {

                State.DblSourceFile.SetCode(Outputs.Code, State.CurrentLineNo);
            }

            return result;
        }

        private bool ProcessLine(string line)
        {
            foreach (LineProcessor lineProcessor in _lineProcessors)
            {
                Match match = lineProcessor.MatchRegex.Match(line);
                if (default(Match) != match && true == match.Success)
                {
                    State.CurrentLine = line;
                    State = lineProcessor.Processor(State, line, match);

                    Outputs.General.Lines.Add(lineProcessor.Formatter(State.CurrentLine, match));

                    break;
                }
            }
            return true;
        }

        protected async Task<string> SendCommand(string command)
        {
            await _client.Write($"{command}\n");
            return await _client.TerminatedReadAsync("DBG>", TimeSpan.FromHours(1));
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
