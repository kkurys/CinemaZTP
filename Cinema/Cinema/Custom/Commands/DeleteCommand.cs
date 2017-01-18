using System;
using System.Windows.Input;

namespace Cinema.Custom.Commands
{
    class DeleteCommand : ICommand
    {
        private object _target;
        readonly Action<object> _execute;
        public DeleteCommand(Action<object> execute, object target)
        {
            _target = target;
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object s)
        {
            _execute(_target);
        }
    }
}
