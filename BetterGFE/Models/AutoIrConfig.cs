using System.Collections.Generic;
using System.ComponentModel;
using MessagePack;

namespace BetterGFE.Models
{
    [MessagePackObject(true)]
    public class AutoIrConfig
    {
        [DefaultValue(false)]
        public bool Enabled { get; set; } = false;
        /// <summary>
        /// Whether to automatically disable Instant Replay if the whitelisted app is not running
        /// </summary>
        [DefaultValue(false)]
        public bool DisableIrWhenWhiteListNotRunning { get; set; } = false;
        public List<ProcessInfo> WhiteList { get; set; } = new List<ProcessInfo>();
        public List<ProcessInfo> BlackList { get; set; } = new List<ProcessInfo>();
        [DefaultValue(3000)]
        public int Interval { get; set; } = 3000;
    }
}
