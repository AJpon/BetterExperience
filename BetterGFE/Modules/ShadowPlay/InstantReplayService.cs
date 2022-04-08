using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NvNodeApi;
using BetterGFE.Core;
using BetterGFE.Models;
using System.Diagnostics;

namespace BetterGFE.Modules.ShadowPlay
{
    internal class InstantReplayService
    {
        private NvNodeApiWrapper _api;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _autoEnabled;

        public InstantReplayService(NvNodeApiWrapper api)
        {
            _api = api;
            Initialize();
        }
        
        private void Initialize()
        {
            _autoEnabled = false;
            if (Config.Instance.AutoIrConfig.Enabled)
            {
                Start();
            }
        }

        public void Start()
        {
            Stop();
            _cancellationTokenSource = new CancellationTokenSource();
            AutoToggleIr();
        }

        public void Stop()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _autoEnabled = false;
            }
        }
        private async void AutoToggleIr()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                if (await _api.ShadowPlay.GetIrRunning() == true)
                {
                    _autoEnabled = true;
                    var isRunningBlacklistedProcess = CheckRunningBlacklistedProcess();
                    if (isRunningBlacklistedProcess)
                    {
                        await _api.ShadowPlay.EnableIr(false);
                    }
                }
                else
                {
                    var isRunningWhiteListedProcess = CheckRunningWhitelistedProcess();
                    if (isRunningWhiteListedProcess && _autoEnabled == false)
                    {
                        await _api.ShadowPlay.EnableIr(true);
                        _autoEnabled = true;
                    }
                }
                await Task.Delay(Config.Instance.AutoIrConfig.Interval);
            }

            bool CheckRunningBlacklistedProcess()
            {
                foreach (var ps in App.Instance.Watcher.Processes)
                {
                    foreach (var blackListed in Config.Instance.AutoIrConfig.BlackList)
                    {
                        if (ProcessWatcher.GetFilePath(ps).Equals(blackListed.FilePath))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

            bool CheckRunningWhitelistedProcess()
            {
                foreach (var ps in App.Instance.Watcher.Processes)
                {
                    try
                    {
                        var filePath = ProcessWatcher.GetFilePath(ps);
                        Debug.WriteLine("[" + ps.Id + "]" + filePath);
                        foreach (var whiteListed in Config.Instance.AutoIrConfig.WhiteList)
                        {
                            if (filePath.Equals(whiteListed.FilePath))
                            {
                                return true;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        // Debug.WriteLine(e.Message);
                        continue;
                    }
                }
                _autoEnabled = false;
                return false;
            }
        }
    }
}
