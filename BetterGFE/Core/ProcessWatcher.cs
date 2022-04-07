using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BetterGFE.Core
{
    internal class ProcessWatcher
    {
        public List<Process> Processes { get; set; }
        private CancellationTokenSource _watcherTokenSource;

        public static string GetFilePath(Process ps)
        {
            return ps.MainModule.FileName;
        }

        public void Start()
        {
            Stop();
            _watcherTokenSource = new CancellationTokenSource();
            Task.Factory.StartNew(Watch, _watcherTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private void Update()
        {
            Processes = Process.GetProcesses().ToList();
        }

        private async void Watch()
        {
            while (!_watcherTokenSource.IsCancellationRequested)
            {
                Update();
                await Task.Delay(3000);
            }
            Debug.WriteLine("[BetterGFE] ProcessWatcher stopped");
        }

        public void Stop()
        {
            if (_watcherTokenSource != null)
            {
                _watcherTokenSource.Cancel();
                _watcherTokenSource.Dispose();
            }
        }
    }
}
