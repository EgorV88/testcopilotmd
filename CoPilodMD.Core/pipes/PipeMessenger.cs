using Newtonsoft.Json;

namespace CoPilodMD.Core.pipes
{
    public class PipeMessenger
    {
        private PipeServer server;
        private PipeClient client;

        public event EventHandler<ServiceMessage> onMessage;
        public PipeMessenger(string name)
        {
            server = new PipeServer(name);
            client = new PipeClient(name);

            server.onMessage += Server_onMessage;
        }

        private void Server_onMessage(object? sender, ServiceMessage message)
        {
            SqlLogger.Info($"receive {message}");
            if (onMessage != null)
            {
                Task.Run(() => { onMessage(sender, message); });
            }
        }

        public void Start()
        {

            server.Start();
        }

        internal void Send(ServiceMessage msg)
        {
            client.Send(msg);
        }
    }
}
