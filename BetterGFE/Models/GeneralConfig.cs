using System.ComponentModel;
using MessagePack;

namespace BetterGFE.Models
{
    [MessagePackObject(true)]
    public class GeneralConfig
    {
        [DefaultValue(true)]
        public bool RunOnStartup { get; set; } = true;
    }
}
