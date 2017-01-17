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
    class MainViewModel : IMainViewModel, INotifyPropertyChanged
    {
        private DbManager db;
        
        private Reservation _reservation = new Reservation();

        private ObservableCollection<Show> _shows = new ObservableCollection<Show>();
        private ObservableCollection<Movie> _movies = new ObservableCollection<Movie>();
        private ObservableCollection<Reservation> _reservations = new ObservableCollection<Reservation>();


        public MainViewModel()
        {
            db = DbManager.Instance();
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
                OnPropertyChaned("Reservation");
            }
        }

        public void LoadCollections()
        {
            _movies = new ObservableCollection<Movie>(db.GetObjects<Movie>());
            _shows = new ObservableCollection<Show>(db.GetObjects<Show>());
            _reservations = new ObservableCollection<Reservation>(db.GetObjects<Reservation>());
        }

        public void AddReservation()
        {
            _reservation.ShowId = 4; // podpiąć zaznaczony seans z listboxa
            db.Add(_reservation);
        }

        public event PropertyChangedEventHandler PropertyChanged = null;

        private void OnPropertyChaned(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
