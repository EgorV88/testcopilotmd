using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoPilodMD.Core.pipes
{
    internal class PipeServer
    {
        private string ServiceName;

        public event EventHandler<ServiceMessage> onMessage;

        public PipeServer(string name)
        {
            ServiceName = name;
        }

        public void Start()
        {
            ThreadPool.QueueUserWorkItem(Loop);
        }

        private void Loop(object? state)
        {
            while (true)
            {
                using (var server = new NamedPipeServerStream(ServiceName, PipeDirection.In, NamedPipeServerStream.MaxAllowedServerInstances))
                {
                    server.WaitForConnection();

                    using (var reader = new StreamReader(server))
                    {
                        string message = reader.ReadLine();
                        try
                        {
                            var msg = JsonConvert.DeserializeObject<ServiceMessage>(message);
                            if (onMessage != null)
                                onMessage.Invoke(this, msg);
                        }
                        catch(Exception e)
                        {
                            SqlLogger.Error($"Cannot parse message {message}");
                        }
                       
                    }
                }
            }
        }
    }
}
