using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterGFE.NvNodeApi;

namespace BetterGFE
{
    public class NvNodeApiWrapper
    {
        public uint ServerPort { get; set; }
        public string? SecurityToken { get; set; }
    }
}
