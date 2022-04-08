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
using BetterGFE.Modules.ShadowPlay;
using BetterGFE.Models;

namespace BetterGFE
{
    /// <summary>
    /// SysTray.xaml の相互作用ロジック
    /// </summary>
    public partial class SysTray : UserControl
    {
        private NvNodeApiWrapper _api;
        private InstantReplayService _irService;
        /// <summary>
        /// ShadowPlay サーバーが起動しているかどうか
        /// </summary>
        private bool _isSpRunning = false;
        /// <summary>
        /// Instant Replay が有効かどうか
        /// </summary>
        private bool _isIrEnabled = false;
        /// <summary>
        /// Instant Replay が実行中かどうか
        /// </summary>
        private bool _isIrRunning = false;

        public SysTray(NvNodeApiWrapper api)
        {
            _api = api;
            _irService = new InstantReplayService(_api);
            // UpdateStatus().Wait();
            InitializeComponent();
            this.DataContext = this;
        }

        void TrayContextMenuOpening(object sender, RoutedEventArgs e)
        {
            // Debug.WriteLine("[BetterExperience] ContextMenuOpening");
            _ = UpdateStatus();
        }

        async Task UpdateStatus()
        {
            _isSpRunning = await _api.ShadowPlay.GetSpRunning();
            // Debug.WriteLine("[BetterExperience] _isSpRunning=" + _isSpRunning);
            spStatus.Header = SpStatusLabel;
            spToggle.Header = ToggleSpActionLabel;
            // Debug.WriteLine("[BetterExperience] ToggleSpActionLabel = " + ToggleSpActionLabel);
            _isIrEnabled = await _api.ShadowPlay.GetIrEnabled();
            _isIrRunning = await _api.ShadowPlay.GetIrRunning();
            // Debug.WriteLine("[BetterExperience] Status updated");
            toggleAutoIr.IsChecked = Config.Instance.AutoIrConfig.Enabled;
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

        public ICommand ToggleSp
        {
            get => new DelegateCommand()
            {
                CanExecuteFunc = () => true,
                ExecuteFunc = () =>
                {
                    _api.ShadowPlay.LaunchSp(!_isSpRunning);
                }
            };
        }
        public string SpStatusLabel { get => _isSpRunning ? "ShadowPlay service is running" : "ShadowPlay service is stopped"; }
        public string ToggleSpActionLabel { get => _isSpRunning ? "Stop ShadowPlay" : "Start ShadowPlay"; }

        ////////////////////////////////////////////////////////////////////////////////
        // Instant Replay(ShadowPlay) Actions
        public ICommand StartIr
        {
            get => new DelegateCommand()
            {
                CanExecuteFunc = () => _isSpRunning && _isIrEnabled == false,
                ExecuteFunc = () =>
                {
                    _api.ShadowPlay.EnableIr(true);
                }
            };
        }

        public ICommand StopIr
        {
            get => new DelegateCommand()
            {
                CanExecuteFunc = () => _isSpRunning && _isIrEnabled,
                ExecuteFunc = () =>
                {
                    _api.ShadowPlay.EnableIr(false);
                }
            };
        }

        public ICommand SaveIr
        {
            get => new DelegateCommand()
            {
                CanExecuteFunc = () => _isSpRunning && _isIrEnabled && _isIrRunning,
                ExecuteFunc = () =>
                {
                    _api.ShadowPlay.SaveIr();
                }
            };
        }

        public ICommand ToggleAutoIr
        {
            get => new DelegateCommand()
            {
                CanExecuteFunc = () => true,
                ExecuteFunc = () =>
                {
                    Config.Instance.AutoIrConfig.Enabled = !Config.Instance.AutoIrConfig.Enabled;
                    if (Config.Instance.AutoIrConfig.Enabled)
                    {
                        _irService.Start();
                    }
                    else
                    {
                        _irService.Stop();
                    }
                }
            };
        }
    }
}

