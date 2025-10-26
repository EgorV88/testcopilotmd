using Microsoft.Extensions.Hosting;
using NLog;
using System.Threading;

namespace CoPilodMD.Core
{
    public abstract class BaseService: BackgroundService
    {
        public string Name { get; protected set; }
      
        public BaseService() { }

        protected abstract void Start();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SqlLogger.InitLogger(LogManager.GetCurrentClassLogger());

            this.Start();

        }

    }
}
