using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NvNodeApi.Interfaces
{
    public interface IApi
    {
        public bool IsSupported { get => _isSupported; internal set => _isSupported = value; }
        private static bool _isSupported;
        public void Init() => IsSupported = IsSupportedCheck();

        // Check if the API in question is supported
        internal bool IsSupportedCheck();
    }
}