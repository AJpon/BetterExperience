using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BetterGFE.Core
{
    internal class DelegateCommand : ICommand
    {
        public Action ExecuteFunc { get; set; }
        public Func<bool> CanExecuteFunc { get; set; }

        public DelegateCommand() { }

        public DelegateCommand(Action execute)
        {
            this.ExecuteFunc = execute;
        }

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            this.ExecuteFunc = execute;
            this.CanExecuteFunc = canExecute;
        }

        public void Execute(object parameter)
        {
            ExecuteFunc();
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
