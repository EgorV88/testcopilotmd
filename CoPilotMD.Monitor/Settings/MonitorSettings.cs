using CoPilodMD.Core;

namespace CoPilotMD.Monitor.Settings
{
    public class MonitorSettings : ServiceSettings
    {
        public Dictionary<string, string> Services { get; set; }

        public int MonitorPeriodMs { get; set; }
    }
}
