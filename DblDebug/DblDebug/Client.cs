using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using PrimS.Telnet;

namespace DblDebug
{
    public interface IClient
    {
        Task<string> TerminatedReadAsync(string terminator, TimeSpan timeout);
        Task Write(string command);
        void Dispose();
    }

    public class SocketClient : IClient
    {
        private readonly Client _client;
        private readonly string _host;
        private readonly int _port;

        public SocketClient(string host, int port)
        {
            _host = host;
            _port = port;
            _client = new Client(_host, _port, new CancellationToken());
        }

        public async Task<string> TerminatedReadAsync(string terminator, TimeSpan timeout)
        {
            return await _client.TerminatedReadAsync(terminator, timeout);
        }

        public async Task Write(string command)
        {
            await _client.Write(command);
        }

        public void Dispose() {
            _client.Dispose();
        }
    }

    public class TestClient : IClient
    {
        private string _lastCommandEntered = "";
        private string _lastCommand = "";
        private string _responseFolder = "/Users/stevelamb/Development/ibcos/investigations/DblDebug/DblDebug/TestResponses";

        public TestClient(string host, int port)
        {
        }

        public async Task<string> TerminatedReadAsync(string terminator, TimeSpan timeout)
        {
            string response = "";
            string responseFileName = Path.Combine(_responseFolder, $"{_lastCommand}.txt");
            if (File.Exists(responseFileName))
            {
                response = File.ReadAllText(responseFileName);
            }
            responseFileName = Path.Combine(_responseFolder, $"{_lastCommandEntered.Replace(" ", "").Replace("\n", "")}.txt");
            if (File.Exists(responseFileName))
            {
                response = File.ReadAllText(responseFileName);
            }
            return response;
        }

        public async Task Write(string command)
        {
            Regex commandRegex = new Regex(@"^([^\s]+)");
            Match match = commandRegex.Match(command);
            if(default(Match) != match && match.Success)
            {
                _lastCommand = match.Groups[1].Value;
            }
            _lastCommandEntered = command;
        }
        public void Dispose() { }
    }
}
