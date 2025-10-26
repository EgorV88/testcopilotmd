using CoPilodMD.Core;
using CoPilodMD.Core.pipes;

namespace CoPilotMD.NetworkListener
{
    public class NetworkListenerService : BaseService
    {
        private TcpReceiver receiver;

        public NetworkListenerService()
        {
            receiver = new TcpReceiver();
        }

        protected override void Receive(object? sender, ServiceMessage msg)
        {
        }

        protected override void Start()
        {
            receiver.Start();
        }
    }
}
