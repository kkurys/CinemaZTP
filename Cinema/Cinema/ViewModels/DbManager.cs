using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ViewModels
{
    class DbManager
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

        public void Add(object toAdd)
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
            db.SaveChanges();
        }

        public void Update(object toUpdate)
        {
            db.Entry(toUpdate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(object toDelete)
        {
            if (toDelete is Movie)
            {
                db.Movies.Remove(toDelete as Movie);
            }
            else if (toDelete is Reservation)
            {
                db.Reservations.Remove(toDelete as Reservation);
            }
            else if (toDelete is Show)
            {
                db.Shows.Remove(toDelete as Show);
            }
            db.SaveChanges();
        }

        public List<object> GetObjects(Type type)
        {
            if (type == typeof(Movie))
            {
                return db.Movies.ToList();
            }
            else if (type is Reservation)
            {
                return db.Reservations.ToList();
            }
            else if (type is Show)
            {
                return db.Shows.ToList();
            }
            else return null;
        }
    }
}
