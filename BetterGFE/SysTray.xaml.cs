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

namespace BetterGFE
{
    /// <summary>
    /// SysTray.xaml の相互作用ロジック
    /// </summary>
    public partial class SysTray : UserControl
    {
        private NvNodeApiWrapper api;
        public SysTray(NvNodeApiWrapper api)
        {
            this.api = api;
            InitializeComponent();
        }

        private void ShowSettings(object sender, RoutedEventArgs e)
        {
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
