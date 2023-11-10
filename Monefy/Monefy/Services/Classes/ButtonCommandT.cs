using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Monefy.Services.Classes
{
    public class ButtonCommand<T> : ICommand
    {
        private readonly Action<T> _funcToExecute;
        private readonly Func<T, bool> _funcToCheck = _ => true;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public ButtonCommand(Action<T> funcToExecute)
        {
            _funcToExecute = funcToExecute;
        }

        public ButtonCommand(Action<T> funcToExecute, Func<T, bool> funcToCheck)
        {
            _funcToExecute = funcToExecute;
            _funcToCheck = funcToCheck;
        }

        public bool CanExecute(object? parameter)
        {
            if (parameter is T value)
            {
                return _funcToCheck(value);
            }
            return false;
        }

        public void Execute(object? parameter)
        {
            if (parameter is T value)
            {
                _funcToExecute(value);
            }
            else if (parameter == null && typeof(T).IsClass)
            {
                _funcToExecute(default);
            }
        }
    }
}