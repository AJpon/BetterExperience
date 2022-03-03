using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using NvNodeApi;

namespace BetterGFE
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public NvNodeApiWrapper api;
        //private TaskbarIcon tb;
        private SysTray st;
        public new int Run()
        {
            //tb = (TaskbarIcon)FindResource("NotifyIcon");
            api = new NvNodeApiWrapper();
            st = new SysTray(api);
            return base.Run();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
        }
    }
}
