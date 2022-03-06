using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NvNodeApi
{
    internal interface IApi
    {
        public bool IsSupported { get; protected set; }
        public void Init()
        {
            IsSupported = IsSupportedCheck();
        }
        // Check if the API in question is supported
        protected bool IsSupportedCheck();
    }
}