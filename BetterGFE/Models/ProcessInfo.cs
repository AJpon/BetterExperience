using MessagePack;

namespace BetterGFE.Models
{
    [MessagePackObject(true)]
    public class ProcessInfo
    {
        public string ProcessName { get; set; }
        public string FilePath { get; set; }
    }
}
