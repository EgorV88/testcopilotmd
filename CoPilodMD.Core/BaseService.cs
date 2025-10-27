using CoPilodMD.Core.pipes;
using Microsoft.Extensions.Hosting;
using NLog;
using System.Threading;

namespace CoPilodMD.Core
{
    public abstract class BaseService: BackgroundService
    {
        protected ServiceSettings settings;

        public string Name => settings.ServiceName;
        public string NextService => settings.NextService;
        protected PipeMessenger pipe;

        public BaseService() { }

        protected void InitPipe()
        {
            pipe = new PipeMessenger(Name);
            pipe.onMessage += Receive;
            pipe.Start();
        }

        protected abstract void Receive(object? sender, ServiceMessage msg);

        public void StartService()
        {
            InitPipe();
            Start();
            SendClientLogs($"Service started");
        }

        protected abstract void Start();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SqlLogger.InitLogger(LogManager.GetCurrentClassLogger());

            this.StartService();
        }

        protected virtual void SendFinishNotif(ServiceMessage msg)
        {
            msg.Sender = Name;
            msg.To = NextService;
            if (!string.IsNullOrEmpty(msg.To))
            {
                pipe.Send(msg);
                SqlLogger.Info($"notif {msg.Topic} sent to {NextService}");
            }
        }

        protected void SendClientLogs(string message)
        {
            var msg = new ServiceMessage();
            msg.Sender = Name;
            msg.To = "GUI";
            msg.Topic = ServiceMessage.TopicHumanizedLogs;
            msg.Message = message;
            if (!string.IsNullOrEmpty(msg.To))
            {
                pipe.Send(msg);
                SqlLogger.Info($"logs {msg.Topic} sent ");
            }
        }

    }
}
