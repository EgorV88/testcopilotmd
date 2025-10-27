using CoPilodMD.Core;
using CoPilodMD.Core.pipes;
using NLog;

namespace CoPilotMD.GuiClient
{
    public class ClientService : BaseService
    {
        public ClientService()
        {
            settings = ServiceSettings.LoadSettings<ServiceSettings>("gui.json");
            SqlLogger.InitLogger(LogManager.GetCurrentClassLogger());
        }
        protected override void Receive(object? sender, ServiceMessage msg)
        {
            if (msg == null) return;
            switch (msg.Topic.ToLower())
            {
                case ServiceMessage.TopicHumanizedLogs:
                    {
                        onLogRecieved?.Invoke(this, msg); 
                        break;
                    }
            }
        }

        protected override void Start()
        {
        }

        public event EventHandler<ServiceMessage> onLogRecieved; 
    }
}
