using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using BetterGFE.Core;
using BetterGFE.Models;

namespace BetterGFE
{
    /// <summary>
    /// SettingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            runOnStartup.IsChecked = Config.Instance.GeneralConfig.RunOnStartup;

            irWhiteList.ItemsSource = Config.Instance.AutoIrConfig.WhiteList;
            irBlackList.ItemsSource = Config.Instance.AutoIrConfig.BlackList;
        }

        public ICommand SaveAndCloseCommand
        {
            get => new DelegateCommand()
            {
                CanExecuteFunc = () => true,
                ExecuteFunc = () =>
                {
                    //Console.WriteLine("[BetterGFE] Push OK on config window");
                    Config.Instance.GeneralConfig.RunOnStartup = runOnStartup.IsChecked??false;

                    Config.Instance.AutoIrConfig.WhiteList = (List<ProcessInfo>)irWhiteList.ItemsSource;
                    Config.Instance.AutoIrConfig.BlackList = (List<ProcessInfo>)irBlackList.ItemsSource;

                    Config.SaveConfig();
                    //Console.WriteLine("[BetterGFE] Save config");
                    this.Close();
                }
            };
        }

        public ICommand Cancel
        {
            get => new DelegateCommand()
            {
                CanExecuteFunc = () => true,
                ExecuteFunc = () =>
                {
                    this.Close();
                }
            };
        }
    }
}
