using Newtonsoft.Json;
using System.IO.Pipes;

namespace CoPilodMD.Core.pipes
{
    internal class PipeClient
    {
        private string ServiceName;

        public PipeClient(string name)
        {
            ServiceName = name;
        }

        public void Send(ServiceMessage message)
        {
            using (var client = new NamedPipeClientStream(".", message.To, PipeDirection.Out))
            {
                client.Connect();
                message.Sender = ServiceName;
                var json = JsonConvert.SerializeObject(message);
                using (var writer = new StreamWriter(client))
                {
                    writer.AutoFlush = true;
                    writer.WriteLine(json);
                    SqlLogger.Info($"Sent {json}");
                }
            }
        }

    }
}
