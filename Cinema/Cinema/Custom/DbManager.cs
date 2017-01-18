using Cinema.Custom.Commands;
using Cinema.Interfaces;
using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Cinema.ViewModels
{
    public class DbManager : IDbManager, IObservable
    {
        private List<IObserver> _observers;
        CinemaDbContext db = new CinemaDbContext();
        private static DeleteOnTimeCommand _timeKeeper;
        private static DbManager _instance;

        private DbManager()
        {
            _observers = new List<IObserver>();
        }
        public void InitKeeper()
        {
            _timeKeeper = new DeleteOnTimeCommand();
            Thread thread = new Thread(new ThreadStart(KeepTime));
            thread.Start();
        }
        public static DbManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DbManager();
            }
            return _instance;
        }

        public ICollection<T> GetObjects<T>()
        {
            if (typeof(T) == typeof(Movie))
            {
                IList<T> list = (IList<T>)db.Movies.ToList();
                return list;
            }
            else if (typeof(T) == typeof(Reservation))
            {
                IList<T> list = (IList<T>)db.Reservations.ToList();
                return list;
            }
            else if (typeof(T) == typeof(Show))
            {
                IList<T> list = (IList<T>)db.Shows.ToList();
                return list;
            }
            else
            {
                return null;
            }
        }
        public void Add(object obj)
        {
            if (obj is Movie)
            {
                var objToAdd = obj as Movie;
                if (db.Movies.ToList().Find(x => x.Id == objToAdd.Id) != null) return;
                db.Movies.Add(objToAdd);
            }
            else if (obj is Show)
            {
                var objToAdd = obj as Show;
                if (db.Shows.ToList().Find(x => x.Id == objToAdd.Id) != null) return;
                db.Shows.Add(objToAdd);
            }
            else if (obj is Reservation)
            {
                var objToAdd = obj as Reservation;
                if (db.Reservations.ToList().Find(x => x.Id == objToAdd.Id) != null) return;
                db.Reservations.Add(objToAdd);

            }
            if (db.SaveChanges() > 0)
            {
                NotifyObservers(obj.GetType()); // OBSERVER NOTIFICATION
            }
        }
        public void Update(object obj)
        {
            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            if (db.SaveChanges() > 0)
            {
                NotifyObservers(obj.GetType()); // OBSERVER NOTIFICATION
            }
        }

        public void Delete(object obj)
        {
            if (obj is Movie)
            {
                var objToDelete = obj as Movie;
                if (db.Movies.ToList().Find(x => x.Id == objToDelete.Id) == null) return;
                db.Movies.Remove(objToDelete);
            }
            else if (obj is Show)
            {
                var objToDelete = obj as Show;
                if (db.Shows.ToList().Find(x => x.Id == objToDelete.Id) == null) return;
                db.Shows.Remove(objToDelete);
            }
            else if (obj is Reservation)
            {
                var objToDelete = obj as Reservation;
                if (db.Reservations.ToList().Find(x => x.Id == objToDelete.Id) == null) return;
                db.Reservations.Remove(objToDelete);

            }
            if (db.SaveChanges() > 0)
            {
                NotifyObservers(obj.GetType());
            }
             // OBSERVER NOTIFICATION

        }
        public void KeepTime()
        {
            while (true)
            {
                Thread.Sleep(30000);

                foreach (var _show in GetObjects<Show>())
                {
                    var _showEnd = _show.ShowDate.Value.Add(_show.EndTime);
                    if (_showEnd.CompareTo(DateTime.Now) <= 0)
                    {
                        _timeKeeper.AddCommand(new DeleteCommand(Delete, _show));
                    }
                }
                foreach (var _reservation in GetObjects<Reservation>())
                {
                    if (_reservation.WasPaid) continue;
                    var showTime = _reservation.Show.ShowDate.Value.Add(_reservation.Show.StartTime);
                    if (showTime.CompareTo(DateTime.Now.Add(new TimeSpan(0, 30, 0))) > 0)
                    {
                        _timeKeeper.AddCommand(new DeleteCommand(Delete, _reservation));
                    }
                }
                _timeKeeper.Execute(null);
            }

        }

        // OBSERVER METHODS
        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }

        }

        public void NotifyObservers(Type t)
        {
            foreach (var obs in _observers)
            {
                obs.Update(t);
            }

        }
    }
}
