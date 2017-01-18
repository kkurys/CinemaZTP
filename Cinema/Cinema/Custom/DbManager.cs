using Cinema.Interfaces;
using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cinema.ViewModels
{
    public class DbManager : IDbManager
    {
        private IObserver observer;
        CinemaDbContext db = new CinemaDbContext();

        private static DbManager _instance;

        private DbManager() { }
        
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
            this.NotifyObservers(obj.GetType()); // OBSERVER NOTIFICATION
        }

        public void Update(object obj)
        {
            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            this.NotifyObservers(obj.GetType()); // OBSERVER NOTIFICATION
        }

        public void Delete(object obj)
        {
            db.Entry(obj).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            this.NotifyObservers(obj.GetType()); // OBSERVER NOTIFICATION
        }

        // OBSERVER METHODES
        public void AddObserver(IObserver observer)
        {
            this.observer = observer;
        }

        public void RemoveObserver(IObserver observer)
        {
            this.observer = null;
        }

        public void NotifyObservers(Type t)
        {
            this.observer.Update(t);
        }
    }
}
