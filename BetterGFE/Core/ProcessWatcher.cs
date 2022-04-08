using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BetterGFE.Core
{
    internal class ProcessWatcher
    {
        public List<Process> Processes { get; set; }
        private CancellationTokenSource _watcherTokenSource;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

        [DllImport("psapi.dll")]
        static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, [In][MarshalAs(UnmanagedType.U4)] int nSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetFilePath(Process ps)
        {
            var processHandle = OpenProcess(0x0400 | 0x0010, false, ps.Id);

            if (processHandle == IntPtr.Zero)
            {
                var error = Marshal.GetLastWin32Error();
                if (error == 5)
                {
                    throw new UnauthorizedAccessException("Access denied (Error code: " + error + ")");
                }
                else if (error == 87)
                {
                    throw new InvalidOperationException("The specified process is not running (Error code: " + error + ")");
                }
                else
                {
                    throw new Win32Exception("Unknown error (Error code: " + error + ")");
                }
            }

            const int lengthSb = 4000;

            var sb = new StringBuilder(lengthSb);

            string result = null;

            if (GetModuleFileNameEx(processHandle, IntPtr.Zero, sb, lengthSb) > 0)
            {
                result = sb.ToString();
            }
            else
            {
                throw new Win32Exception("GetModuleFileNameEx() returned zero");
            }

            CloseHandle(processHandle);

            return result;
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
