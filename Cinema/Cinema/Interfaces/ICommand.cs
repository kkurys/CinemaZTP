using System;

namespace Cinema.Interfaces
{
    public interface ICommand
    {
        bool CanExecute(object param);
        void Execute(object param);
        event EventHandler CanExecuteChanged;
    }
}
