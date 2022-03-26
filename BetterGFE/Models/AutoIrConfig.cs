using System.Collections.Generic;
using System.ComponentModel;
using MessagePack;

namespace BetterGFE.Models
{
    [MessagePackObject(true)]
    public class AutoIrConfig
    {
        /// <summary>
        /// Whether to automatically disable Instant Replay if the whitelisted app is not running
        /// </summary>
        [DefaultValue(false)]
        public bool DisableIrWhenWhiteListNotRunning { get; set; } = false;
        public List<ProcessInfo> WhiteList { get; set; }
        public List<ProcessInfo> BlackList { get; set; }
    }
}
