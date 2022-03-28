using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace BetterGFE.Models
{
    [MessagePackObject]
    public class Config
    {
        public static Config Instance { get; set; }

        [Key(0)]
        public GeneralConfig GeneralConfig { get; set; } = new GeneralConfig();
        [Key(1)]
        public AutoIrConfig AutoIrConfig { get; set; } = new AutoIrConfig();

        public static void SaveConfig() 
        {
            var bytes = MessagePackSerializer.Serialize(Instance);
            using (var fs = new FileStream(Environment.ConfigurationPath, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
        }
        public static void LoadConfig()
        {
            if (!File.Exists(Environment.ConfigurationPath))
            {
                Instance = new Config();
                SaveConfig();
            }
            else
            {
                using (var fs = new FileStream(Environment.ConfigurationPath, FileMode.Open))
                {
                    Instance = MessagePackSerializer.Deserialize<Config>(fs);
                }
            }
        }
    }
}
