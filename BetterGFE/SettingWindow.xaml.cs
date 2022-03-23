using System;
using System.Collections.Generic;
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
        }

        public ICommand  SaveAndCloseCommand
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

        public ICommand  Cancel
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
