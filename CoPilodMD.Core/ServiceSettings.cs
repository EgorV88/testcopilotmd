using Newtonsoft.Json;

namespace CoPilodMD.Core
{
    public class ServiceSettings
    {
        public string ServiceName { get; set; }

        public static T LoadSettings<T>(string path) where T : ServiceSettings
        {
            if (!File.Exists(path)) { return null; }

            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }
    }
}
