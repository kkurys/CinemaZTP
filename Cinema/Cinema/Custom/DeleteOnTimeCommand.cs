using Cinema.Interfaces;
using Cinema.Models;
using Cinema.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cinema.Custom.Commands
{
    class DeleteOnTimeCommand : IObserver
    {
        #region fields
        private List<ICommand> commands;
        private DbManager db;
        #endregion
        #region constructors
        public DeleteOnTimeCommand()
        {
            this.db = DbManager.GetInstance();
            Thread thread = new Thread(new ThreadStart(this.ThreadFunction));
            thread.Start();
        }
        #endregion
        #region thread methode
        public void ThreadFunction()
        {
            RemoveOld();
            Thread.Sleep(60);
        }
        public void RemoveOld()
        {
            ICollection<Show> shows = db.GetObjects<Show>();
            ICollection<Reservation> reservations = db.GetObjects<Reservation>();
            foreach (Reservation r in reservations)
            {
                if (DateTime.Compare((DateTime)r.Show.ShowDate, DateTime.Now.AddMinutes(30)) > 0)
                {
                    db.Delete(r);
                }
            }
            foreach (Show s in shows)
            {
                if (DateTime.Compare((DateTime)s.ShowDate, DateTime.Now) > 0)
                {
                    db.Delete(s);
                }
            }
        }
        #endregion
        #region observer
        public void Execute()
        {

        }
        public bool CanExecute()
        {
            return true;
        }
        public void AddComannds(ICommand command)
        {
            this.commands.Add(command);
        }
        #endregion
        #region observer
        public void Update(Type t)
        {

        }
        #endregion
    }
}
