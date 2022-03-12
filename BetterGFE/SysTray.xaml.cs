using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NvNodeApi;
using BetterGFE.Core;
using System.Windows.Threading;

namespace BetterGFE
{
    /// <summary>
    /// SysTray.xaml の相互作用ロジック
    /// </summary>
    public partial class SysTray : UserControl
    {
        private NvNodeApiWrapper api;
        /// <summary>
        /// ShadowPlay サーバーが起動しているかどうか
        /// </summary>
        private bool isSpRunning = false;
        /// <summary>
        /// Instant Replay が有効かどうか
        /// </summary>
        private bool isIrEnabled = false;
        /// <summary>
        /// Instant Replay が実行中かどうか
        /// </summary>
        private bool isIrRunning = false;

        public SysTray(NvNodeApiWrapper api)
        {
            this.api = api;
            UpdateStatus().Wait();
            InitializeComponent();
            this.DataContext = this;
        }

        void TrayContextMenuOpening(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("[BetterExperience] ContextMenuOpening");
            _ = UpdateStatus();
        }

        async Task UpdateStatus()
        {
            isSpRunning = await api.ShadowPlay.GetSpRunning();
            // Debug.WriteLine("[BetterExperience] isSpRunning=" + isSpRunning);
            ToggleSpActionLabel = isSpRunning ? "Stop ShadowPlay" : "Start ShadowPlay";
            // Debug.WriteLine("[BetterExperience] ToggleSpActionLabel = " + ToggleSpActionLabel);
            isIrEnabled = await api.ShadowPlay.GetIrEnabled();
            isIrRunning = await api.ShadowPlay.GetIrRunning();
            Debug.WriteLine("[BetterExperience] Status updated");
        }

        ////////////////////////////////////////////////////////////////////////////////
        // Common Actions

        public ICommand ShowSettings
        {
            get => new DelegateCommand()
            {
                CanExecuteFunc = () => true,
                ExecuteFunc = () =>
                {
                    SettingWindow sw = Application.Current.Windows.OfType<SettingWindow>().FirstOrDefault();
                    if (sw == null)
                    {
                        sw = new SettingWindow();
                        sw.Show();
                    }
                    else
                    {
                        sw.WindowState = WindowState.Normal;
                        sw.Focus();
                    }
                }
            };
        }

        public ICommand Exit
        {
            get => new DelegateCommand()
            {
                CanExecuteFunc = () => true,
                ExecuteFunc = () =>
                {
                    Application.Current.Shutdown();
                }
            };
        }

        ////////////////////////////////////////////////////////////////////////////////
        // ShadowPlay Actions

        public ICommand StartSp
        {
            get => new DelegateCommand()
            {
                CanExecuteFunc = () => isSpRunning == false,
                ExecuteFunc = () =>
                {
                    api.ShadowPlay.LaunchSp(true);
                }
            };
        }

        public ICommand StopSp
        {
            get => new DelegateCommand()
            {
                CanExecuteFunc = () => isSpRunning == true,
                ExecuteFunc = () =>
                {
                    api.ShadowPlay.LaunchSp(false);
                }
            };
        }
        public string ToggleSpActionLabel { get; set; }

        ////////////////////////////////////////////////////////////////////////////////
        // Instant Replay(ShadowPlay) Actions
        public ICommand StartIr
        {
            get => new DelegateCommand()
            {
                CanExecuteFunc = () => isSpRunning && isIrEnabled == false,
                ExecuteFunc = () =>
                {
                    api.ShadowPlay.EnableIr(true);
                }
            };
        }

        public ICommand StopIr
        {
            get => new DelegateCommand()
            {
                CanExecuteFunc = () => isSpRunning && isIrEnabled,
                ExecuteFunc = () =>
                {
                    api.ShadowPlay.EnableIr(false);
                }
            };
        }

        public ICommand SaveIr
        {
            get => new DelegateCommand()
            {
                CanExecuteFunc = () => isSpRunning && isIrEnabled && isIrRunning,
                ExecuteFunc = () =>
                {
                    api.ShadowPlay.SaveIr();
                }
            };
        }
    }
}

