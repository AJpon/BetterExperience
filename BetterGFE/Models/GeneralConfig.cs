using MessagePack;

namespace BetterGFE.Models
{
    [MessagePackObject(true)]
    public class GeneralConfig
    {
        public bool RunOnStartup { get; set; }
    }
}
