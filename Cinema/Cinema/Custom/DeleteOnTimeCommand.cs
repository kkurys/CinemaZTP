using Cinema.Interfaces;
using Cinema.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Cinema.Custom.Commands
{
    class DeleteOnTimeCommand : ICommand, IObserver
    {
        #region fields
        private List<ICommand> commands;

        public event EventHandler CanExecuteChanged;
        #endregion
        #region constructors
        public DeleteOnTimeCommand()
        {
            commands = new List<ICommand>();
        }
        #endregion
 
        public void AddCommand(ICommand command)
        {
            this.commands.Add(command);
        }
        public void Update(Type t)
        {

        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            for (int i = 0; i < commands.Count; i++)
            {
                commands[i].Execute(null);
            }
            commands.Clear();
        }

    }
}
