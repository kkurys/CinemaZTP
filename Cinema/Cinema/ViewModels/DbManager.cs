using Cinema.Interfaces;
using Cinema.Models;
using System;
using System.Collections.Generic;

namespace Cinema.ViewModels
{
    public class DbManager : IDbManager
    {
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

        public void Add(Object toAdd)
        {
            if (toAdd is Movie)
            {
                db.Movies.Add(toAdd as Movie);
            }
            else if (toAdd is Reservation)
            {
                db.Reservations.Add(toAdd as Reservation);
            }
            else if (toAdd is Show)
            {
                db.Shows.Add(toAdd as Show);
            }
        }

        IDbManager IDbManager.GetInstance()
        {
            throw new NotImplementedException();
        }

        public ICollection<T> GetObjects<T>()
        {
            throw new NotImplementedException();
        }

        public void Update(object obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(object obj)
        {
            throw new NotImplementedException();
        }

        public void AddObserver(IObserver observer)
        {
            throw new NotImplementedException();
        }

        public void NotifyObservers(Type t)
        {
            throw new NotImplementedException();
        }
    }
}
