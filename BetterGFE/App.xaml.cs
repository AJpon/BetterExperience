using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using NvNodeApi;
using BetterGFE.Models;
using BetterGFE.Core;

namespace BetterGFE
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App Instance { get; private set; }
        public NvNodeApiWrapper api;
        //private TaskbarIcon tb;
        private SysTray st;
        internal ProcessWatcher Watcher;

        public new int Run()
        {
            Instance = this;
            Config.LoadConfig();
            //tb = (TaskbarIcon)FindResource("NotifyIcon");
            api = new NvNodeApiWrapper();
            st = new SysTray(api);
            Watcher = new ProcessWatcher();
            return base.Run();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Watcher.Start();
            while (Watcher.Processes == null)
            {
                System.Threading.Thread.Sleep(100);
            }
            foreach (var p in Watcher.Processes)
            {
                if (p.ProcessName.Equals("NVIDIA Web Helper"))
                {
                    Debug.WriteLine("[BetterGFE] Found Web Helper");
                    base.OnStartup(e);
                    return;
                }
            }
            MessageBox.Show("[BetterGFE] Web Helper not running. Please start it and try again.", "BetterGFE", MessageBoxButton.OK, MessageBoxImage.Error);
            base.OnStartup(e);
            Shutdown();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Watcher.Stop();
            Config.SaveConfig();
            base.OnExit(e);
        }
    }
}
