using CoPilodMD.Core;

namespace CoPilotMD.NetworkListener
{
    public class NetworkListenerService : BaseService
    {
        private TcpReceiver receiver;

        public NetworkListenerService()
        {
            receiver = new TcpReceiver();
        }
        protected override void Start()
        {
            receiver.Start();
        }
    }
}
