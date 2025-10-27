using System.Net.Sockets;
using System.Net;
using CoPilodMD.Core;

namespace CoPilotMD.NetworkListener
{
    public class TcpReceiver: IDisposable
    {
        //TODO: load port from settings
        private int port = 8888;
        public int Port => port;

        internal EventHandler<string> onFinished;

        private TcpListener listener;
        private Thread listenThread;

        public string LastFileId{get;set;}

        public void Start()
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            SqlLogger.Info($"TcpReciever started at {Port}");
            listenThread = new Thread(ListenLoop)
            {
                Name = "Listener thread",
                IsBackground = true
            };
            listenThread.Start();
        }

        private void ListenLoop()
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                FileLoader loader = new FileLoader(client);
                loader.onFinished += onFinished;
                LastFileId = loader.StartLoading();
            }
        }

        public void Dispose()
        {
            listener.Stop();
            listener.Dispose();
        }
    }
}
