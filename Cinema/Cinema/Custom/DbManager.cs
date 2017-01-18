using Cinema.Interfaces;
using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cinema.ViewModels
{
    public class DbManager : IDbManager, IObservable
    {
        private List<IObserver> _observers;
        CinemaDbContext db = new CinemaDbContext();

        private static DbManager _instance;

        private DbManager()
        {
            _observers = new List<IObserver>();
        }

        public static DbManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DbManager();
            }
            return _instance;
        }


        // NIE MAM POJĘCIA CZY TO ZADZIAŁA
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
            db.Entry(obj).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            NotifyObservers(obj.GetType()); // OBSERVER NOTIFICATION
        }

        public void Update(object obj)
        {
            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            NotifyObservers(obj.GetType());
        }

        public void Delete(object obj)
        {
            if (db.Entry(obj) == null) return;
            db.Entry(obj).State = System.Data.Entity.EntityState.Deleted;
            if (db.SaveChanges() > 0)
            {
                NotifyObservers(obj.GetType()); // OBSERVER NOTIFICATION
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
