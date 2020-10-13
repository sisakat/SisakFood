using System.IO;
using System.Text.Json;

namespace SisakFood.Cli
{
    public class Config
    {
        public string Location { get; set; } = ".";

        public static Config Load(string location)
        {
            if (File.Exists(location))
            {
                string json = File.ReadAllText(location);
                return JsonSerializer.Deserialize<Config>(json, new JsonSerializerOptions() {
                    WriteIndented = true   
                });
            }
            else
            {
                return new Config();
            }
        }

        public void Save(string location)
        {
            string json = JsonSerializer.Serialize(this);
            if (File.Exists(location)) File.Delete(location);
            File.WriteAllText(location, json);
        }
    }
}