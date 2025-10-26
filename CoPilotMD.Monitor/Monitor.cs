using CoPilodMD.Core;
using CoPilotMD.Monitor.Settings;
using System.Diagnostics;

namespace CoPilotMD.Monitor
{
    public class Monitor
    {
        private MonitorSettings settings;
        public void Start()
        {
            ThreadPool.QueueUserWorkItem(MonitorLoop);
        }

        private void MonitorLoop(object? state)
        {
            int period = 1000;
            while (true)
            {
                if (settings != null && settings.Services != null)
                {
                    foreach (var kvp in settings.Services)
                    {
                        SqlLogger.Info($"Check state of {kvp.Key} service");
                        if (!IsServiceLive(kvp.Value))
                        {
                            SqlLogger.Info($"Process {kvp.Key} ({kvp.Value}) does not exists. Start new");
                            RunService(kvp.Value);
                        }
                    }
                }

                Thread.Sleep(period);
            }
        }

        private bool IsServiceLive(string path)
        {
            //TODO: change logic of process searching
            var name = Path.GetFileNameWithoutExtension(path);
            var processes = Process.GetProcessesByName(name);
            if (processes!=null && processes.Length>0 && processes.Any(p => !p.HasExited))
            {
                return true;
            }
            return false;
        }

        private void RunService(string path)
        {
            Process.Start(path);
        }
    }
}
