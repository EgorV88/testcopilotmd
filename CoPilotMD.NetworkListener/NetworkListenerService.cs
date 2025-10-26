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
            receiver.onFinished += OnReceiveFinish;
            settings = ServiceSettings.LoadSettings<ServiceSettings>("network.json");
        }

        private void OnReceiveFinish(object? sender, string fileId)
        {
            var msg = new ServiceMessage()
            {
                Topic = ServiceMessage.TopicFile,
                Message = fileId
            };
            SendFinishNotif(msg);
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
