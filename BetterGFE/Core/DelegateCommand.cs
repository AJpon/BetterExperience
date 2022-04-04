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
        /// <summary>
        /// The action to be called when the command is invoked.
        /// </summary>
        public virtual Action ExecuteFunc { get; set; }
        public virtual Func<bool> CanExecuteFunc { get; set; }

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

        public virtual void Execute(object? parameter)
        {
            ExecuteFunc();
        }

        public virtual bool CanExecute(object? parameter)
        {
            return CanExecuteFunc();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    /// <typeparam name="T"><see cref="InputBinding.CommandParameter">CommandParameter</see> type</typeparam>
    internal class DelegateCommand<T> : DelegateCommand, ICommand
    {
        /// <summary>
        /// The action to be called when the command is invoked.
        /// </summary>
        public new Action<T> ExecuteFunc { get; set; }
        public new Func<T, bool> CanExecuteFunc { get; set; }

        public DelegateCommand() { }

        public DelegateCommand(Action<T> execute)
        {
            this.ExecuteFunc = execute;
        }

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            this.ExecuteFunc = execute;
            this.CanExecuteFunc = canExecute;
        }

        public new void Execute(object? parameter)
        {
            ExecuteFunc((T)parameter);
        }

        public new bool CanExecute(object? parameter)
        {
            return CanExecuteFunc((T)parameter);
        }
    }
}
