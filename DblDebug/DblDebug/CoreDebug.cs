using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
//using PrimS.Telnet;

namespace DblDebug
{
    public class CoreDebug: IDisposable
    {
        private readonly Func<bool, string> _input;
        private readonly string _host;
        private readonly int _port;
        private TcpClient _client;
        private NetworkStream _clientStream;
        //private Client _client;

        public CoreDebug(string host, int port)
        {
            _host = host;
            _port = port;
            _response = new List<string>();
            Connect();

            GetResponse();
            SendCommand("se st ov");
            SendCommand("s");
        }

        public bool Command(string command)
        {
            bool result = true;
            if("q" == command)
            {
                result = false;
            }
            else
            {
                if(false == string.IsNullOrEmpty(command))
                {
                    SendCommand(command);
                    Console.WriteLine(string.Join("\n", _response));
                }
            }
            return result;
        }

        protected bool GetResponse()
        {
            Byte[] recData = new Byte[4096];

            int bytes = _clientStream.Read(recData, 0, recData.Length);

            string rec = System.Text.Encoding.ASCII.GetString(recData, 0, bytes).Replace("\r", "");
            _response.AddRange(rec.Split('\n').ToList());
            if (rec.EndsWith("DBG> "))
            {
                //_response.Remove(_response.Count - 1);
                return false;
            }

            return true;
        }

        protected void SendCommand(string command)
        {
            string result;
            _response = new List<string>();


            Byte[] sendData = System.Text.Encoding.ASCII.GetBytes($"{command}\n");
            _clientStream.Write(sendData, 0, sendData.Length);

            while (GetResponse())
            {

            }
        }

        protected void Connect()
        {
            _client = new TcpClient();
            _client.Connect(_host, _port);
            _clientStream = _client.GetStream();
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
                    _clientStream.Close();
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
