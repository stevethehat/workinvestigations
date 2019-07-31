using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
        private readonly PrimS.Telnet.Client _client;

        public CoreDebug(string host, int port)
        {
            _host = host;
            _port = port;
            _response = new List<string>();
            _client = new Client(_host, _port, new CancellationToken());
        }

        public async Task<bool> Start()
        {
            bool result = false;
            string response = default(string);
            response = await GetResponse();
            response = await SendCommand("se st ov");
            response = await SendCommand("s");

            return result;
        }

        public async Task<List<string>> Command(string command)
        {
            List<string> result = new List<string>();
            if("q" == command)
            {
                result = default(List<string>);
            }
            else
            {
                if(false == string.IsNullOrEmpty(command))
                {
                    string commandResult = await SendCommand(command);
                    result.AddRange(commandResult.Split('\n').ToList());
                }
            }
            return result;
        }

        protected async Task<string> GetResponse()
        {
            return await _client.TerminatedReadAsync("DBG>");
        }

        protected async Task<string> SendCommand(string command)
        {
            await _client.Write($"{command}\n");
            return await GetResponse();
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        private List<string> _response;

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
