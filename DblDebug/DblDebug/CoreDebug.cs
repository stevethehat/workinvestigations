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
        private IClient _client;
        private List<string> _response;

        public IClient Client
        {
            get => _client;
        }
        public Outputs Outputs { get; set; } = new Outputs();
        public State State { get; private set; }
        public Settings Settings { get; } = new Settings();
        public Commands Commands { get; set; } = new Commands();
        public Command LastCommand { get; internal set; } = new Command();
        public bool Connected { get; private set; }

        private List<LineProcessor> _lineProcessors = new List<LineProcessor>();

        public CoreDebug(string sourceDirectory)
        {
            Connected = false;
            State = new State(sourceDirectory);
            _response = new List<string>();

            _lineProcessors = new List<LineProcessor>()
            {
                new LineProcessor(new Regex(@"(Break at) (\d*) in ([A-Z_]*) \(([A-Z_]*\.[A-Z_]*)\)(.*)"),
                   (s, l, m)    => Processors.LineNumber(s, l, m),
                   (l, m)       => Formatters.LineNumber(this, l, m)
                ),
                new LineProcessor(new Regex(@"(Step to) (\d*) in ([A-Z_]*) \(([A-Z_]*\.[A-Z_]*)\)"),
                   (s, l, m)    => Processors.LineNumber(s, l, m),
                   (l, m)       => Formatters.LineNumber(this, l, m)
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
        
        public async Task<bool> Start(IClient client)
        {
            bool result = false;
            _client = client;

            Connected = _client.Connect();

            string response = default(string);
            response = await _client.TerminatedReadAsync(TimeSpan.FromSeconds(10));

            bool commandResult = await Command("se st ov");
            Outputs.General.Write();

            commandResult = await Command("s");
            Outputs.General.Write();

            Connected = true;
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

                if(default(Func<CoreDebug, string, string>) != command.PreProcess)
                {
                    enteredCommand = command.PreProcess(this, enteredCommand);
                }

                if (true == command.IsInternal)
                {
                    result = await ProcessInternalCommand(command);
                }
                else
                {
                    string commandResult = await SendCommand(enteredCommand);

                    result = ProcessResponse(command, commandResult);
                }
            }

            return result;
        }

        internal async Task<bool> ProcessInternalCommand(Command command)
        {
            bool result = true;

            if(default(Command) != command)
            {
                result = await command.Execute(this, State.LastEnteredCommand);
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

            string trimmedCommandResult = Trim(resposne);
            List<string> lines = trimmedCommandResult.Split('\n')
                .Where(l => (false == l.StartsWith("DBG>") && false == string.IsNullOrEmpty(Trim(l))))
                .Select(l => l)
                .ToList();

            lines = command.ResponsePreProcess(this, lines);

            foreach (string line in lines)
            {
                ProcessLine(line);
            }

            if (
                default(DblSourceFile) != State.DblSourceFile && 
                CommandType.Navigation == command.CommandType
            )
            {
                Console.Clear();
                State.DblSourceFile.SetCode(Outputs.Code, State.CurrentLineNo, Settings.Get("autoviewlines"));
                State.CurrentFile = State.DblSourceFile.FileName;
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
                    // this seems wrong..
                    State.CurrentLine = line;
                    State = lineProcessor.Processor(State, line, match);

                    Outputs.General.Lines.Add(lineProcessor.Formatter(State.CurrentLine, match));

                    break;
                }
            }
            return true;
        }

        public async Task<string> SendCommand(string command)
        {
            await _client.Write($"{command}\n");
            return await _client.TerminatedReadAsync(TimeSpan.FromHours(1));
        }

        public async Task<string[]> GetResponseFromCommand(string command)
        {
            string Trim(string line)
            {
                return line.TrimEnd(new char[] { '\r', '\n' });
            }

            await _client.Write($"{command}\n");
            string response = await _client.TerminatedReadAsync(TimeSpan.FromHours(1));

            string trimmedCommandResult = Trim(response);
            string[] lines = trimmedCommandResult.Split('\n')
                .Where(l => (false == l.StartsWith("DBG>") && false == string.IsNullOrEmpty(Trim(l))))
                .Select(l => l)
                .ToArray();

            return lines;
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
