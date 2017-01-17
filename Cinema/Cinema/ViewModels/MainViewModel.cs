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

        private Movie _movie = new Movie();
        private Reservation _reservation = new Reservation();
        private Show _show = new Show();

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

        public Movie Movie
        {
            get { return _movie; }
            set
            {
                _movie = value;
                OnPropertyChaned("Movie");
            }
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

        public Show Show
        {
            get { return _show; }
            set
            {
                _show = value;
                OnPropertyChaned("Show");
            }
        }

        public void LoadCollections()
        {
            _movies = new ObservableCollection<Movie>(db.GetObjects<Movie>());
            _shows = new ObservableCollection<Show>(db.GetObjects<Show>());
        }

        public event PropertyChangedEventHandler PropertyChanged = null;

        private void OnPropertyChaned(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
