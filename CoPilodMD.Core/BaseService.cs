using CoPilodMD.Core.pipes;
using Microsoft.Extensions.Hosting;
using NLog;
using System.Threading;

namespace CoPilodMD.Core
{
    public abstract class BaseService: BackgroundService
    {
        public string Name { get; protected set; }
        public string NextService { get; protected set; }
        protected PipeMessenger pipe;

        public BaseService() { }

        protected void InitPipe()
        {
            pipe = new PipeMessenger(Name);
            pipe.onMessage += Receive;
            pipe.Start();
        }

        protected abstract void Receive(object? sender, ServiceMessage msg);

        private void StartService()
        {
            InitPipe();
            Start();
        }

        protected abstract void Start();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SqlLogger.InitLogger(LogManager.GetCurrentClassLogger());

            this.StartService();

        }

    }
}
