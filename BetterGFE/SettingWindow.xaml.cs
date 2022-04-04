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
using Microsoft.Win32;

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
            var config = Config.Instance.Clone();
            runOnStartup.IsChecked = config.GeneralConfig.RunOnStartup;

            irWhiteList.ItemsSource = config.AutoIrConfig.WhiteList;
            irBlackList.ItemsSource = config.AutoIrConfig.BlackList;
        }

        public ICommand AddProgram
        {
            get => new DelegateCommand<DataGrid>()
            {
                CanExecuteFunc = (dg) => true,
                ExecuteFunc = (dg) =>
                {
                    if (dg == null)
                    {
                        throw new ArgumentNullException(nameof(dg));
                    }

                    var diag = new OpenFileDialog
                    {
                        Filter = "Executable File|*.exe",
                    };
                    if (diag.ShowDialog() != true)
                    {
                        return;
                    }
                    var list = (List<ProcessInfo>)dg.ItemsSource;
                    list.Add(new ProcessInfo
                    {
                        FilePath = diag.FileName,
                        ProcessName = diag.SafeFileName,
                    });
                    dg.Items.Refresh();
                }
            };
        }

        public ICommand RemoveProgram
        {
            get => new DelegateCommand<DataGrid>()
            {
                CanExecuteFunc = (dg) => 0 <= dg.SelectedIndex,
                ExecuteFunc = (dg) =>
                {
                    if (dg == null)
                    {
                        throw new ArgumentNullException(nameof(dg));
                    }
                    var list = (List<ProcessInfo>)dg.ItemsSource;
                    list.RemoveAt(dg.SelectedIndex);
                    dg.Items.Refresh();
                }
            };
        }

        public ICommand SaveAndCloseCommand
        {
            get => new DelegateCommand()
            {
                CanExecuteFunc = () => true,
                ExecuteFunc = () =>
                {
                    //Console.WriteLine("[BetterGFE] Push OK on config window");
                    Config.Instance.GeneralConfig.RunOnStartup = runOnStartup.IsChecked ?? false;

                    Config.Instance.AutoIrConfig.WhiteList = (List<ProcessInfo>)irWhiteList.ItemsSource;
                    Config.Instance.AutoIrConfig.BlackList = (List<ProcessInfo>)irBlackList.ItemsSource;

                    Config.SaveConfig();
                    //Config.LoadConfig();
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
