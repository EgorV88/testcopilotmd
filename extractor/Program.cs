using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace extractor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<CoPilotMD.Extracter.ExtracterService>();
                })
                .Build()
                .Run();
        }
    }
}
