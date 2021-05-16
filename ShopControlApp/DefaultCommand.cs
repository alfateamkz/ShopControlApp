using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace ShopControlApp
{
   public class DefaultCommand : ICommand
    {
        Action<object> execute;
        Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute (object parameter)
        {
            this.execute(parameter);
        }

        public bool CanExecute (object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public DefaultCommand(Action<object> execute, Func<object,bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
    }
}
