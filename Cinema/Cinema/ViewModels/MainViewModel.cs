using Cinema.Interfaces;
using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ViewModels
{
    class MainViewModel : BaseViewModel, IMainViewModel
    {
        private IDbManager _db;

        private Reservation _reservation;

        private ObservableCollection<Show> _shows;
        private ObservableCollection<Movie> _movies;
        private ObservableCollection<Reservation> _reservations;


        public MainViewModel(IDbManager db)
        {
            _reservation = new Reservation();
            _shows = new ObservableCollection<Show>();
            _movies = new ObservableCollection<Movie>();
            _reservations = new ObservableCollection<Reservation>();

            Init(db);
            LoadCollections();
        }

        public ObservableCollection<Reservation> Reservations
        {
            get { return _reservations; }
        }
        public ObservableCollection<Show> Shows
        {
            get { return _shows; }
        }
        public ObservableCollection<Movie> Movies
        {
            get { return _movies; }
        }

        public Reservation Reservation
        {
            get { return _reservation; }
            set
            {
                _reservation = value;
                OnPropertyChanged("Reservation");
            }
        }

        public void LoadCollections()
        {
            _movies = new ObservableCollection<Movie>(_db.GetObjects<Movie>());
            _shows = new ObservableCollection<Show>(_db.GetObjects<Show>());
            _reservations = new ObservableCollection<Reservation>(_db.GetObjects<Reservation>());
        }

        public void AddReservation()
        {
            _reservation.ShowId = 4; // podpiąć zaznaczony seans z listboxa
            _db.Add(_reservation);
        }
        
        public override void Init(IDbManager db)
        {
            _db = db;
        }
    }
}
