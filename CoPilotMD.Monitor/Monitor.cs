using CoPilodMD.Core;
using CoPilodMD.Core.pipes;
using CoPilotMD.Monitor.Settings;
using System.Diagnostics;

namespace CoPilotMD.Monitor
{
    public class Monitor : BaseService
    {
        private MonitorSettings m_settings => settings as MonitorSettings;
        public Monitor()
        {
            settings = ServiceSettings.LoadSettings<MonitorSettings>("monitor.json");
        }

        protected override void Receive(object? sender, ServiceMessage msg)
        {
        }

        protected override void Start()
        {
            ThreadPool.QueueUserWorkItem(MonitorLoop);
        }

        internal void MonitorLoop(object? state)
        {
            int period = 1000;
            while (true)
            {
                if (settings != null && m_settings.Services != null)
                {
                    foreach (var kvp in m_settings.Services)
                    {
                        SqlLogger.Info($"Check state of {kvp.Key} service");
                        if (!IsServiceLive(kvp.Value))
                        {
                            SqlLogger.Info($"Process {kvp.Key} ({kvp.Value}) does not exists. Start new");
                            SendClientLogs($"Process {kvp.Key} ({kvp.Value}) does not exists. Start new");
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

        private bool RunService(string path)
        {
            var p = new Process();
            p.StartInfo.FileName = path;
            p.StartInfo.WorkingDirectory = Path.GetDirectoryName(path);
            p.Start();

            return p != null && !p.HasExited;
        }
    }
}
