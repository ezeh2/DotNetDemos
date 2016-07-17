using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace WpfApplication2016_06_28
{
    public class CommandHandler : ICommand
    {
        private Action _action;
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            // return CanExecuteChanged(this);
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
