using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace deidentifier
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<CoPilotMD.Deidentifier.DeidentifierService>();
                })
                .Build()
                .Run();
        }
    }
}
