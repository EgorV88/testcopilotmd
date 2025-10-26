using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace network
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
               .UseWindowsService()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddHostedService<CoPilotMD.NetworkListener.NetworkListenerService>();
               })
               .Build()
               .Run();
        }
    }
}
