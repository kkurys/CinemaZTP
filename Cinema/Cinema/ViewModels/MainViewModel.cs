using Cinema.Custom.Commands;
using Cinema.Interfaces;
using Cinema.Models;
using Cinema.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Cinema.ViewModels
{
    class MainViewModel : BaseViewModel, IMainViewModel
    {
        #region Fields
        private IDbManager _db;

        CultureInfo cultureInfo;

        private Reservation _reservation;
        private Movie _selectedMovie;
        private Show _selectedShow;
        private string _idFilter;
        private string _nameFilter;
        private string _surnameFilter;

        private ObservableCollection<Show> _shows;
        private ObservableCollection<Movie> _movies;
        private ObservableCollection<Reservation> _reservations;
        #endregion

        #region Ctors
        public MainViewModel(IDbManager db)
        {
            cultureInfo = new CultureInfo("pl-PL");

            _reservation = new Reservation();
            _shows = new ObservableCollection<Show>();
            _movies = new ObservableCollection<Movie>();
            _reservations = new ObservableCollection<Reservation>();

            DeleteMovieCommand = new RelayCommand(DeleteMovie_Executed, Movie_CanExecute);
            EditMovieCommand = new RelayCommand(EditMovie_Executed, Movie_CanExecute);

            Init(db);
            LoadCollections();
        }
        #endregion

        #region Properties
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

        public ICommand DeleteMovieCommand { get; set; }
        public ICommand EditMovieCommand { get; set; }

        public Movie SelectedMovie
        {
            get { return _selectedMovie; }
            set
            {
                _selectedMovie = value;
                OnPropertyChanged("SelectedMovie");
            }
        }
        public Show SelectedShow
        {
            get { return _selectedShow; }
            set
            {
                _selectedShow = value;
                OnPropertyChanged("SelectedShow");
            }
        }
        public string IdFilter
        {
            get { return _idFilter; }
            set
            {
                _idFilter = value;
                OnPropertyChanged("IdFilter");
            }
        }
        public string NameFilter
        {
            get { return _nameFilter; }
            set
            {
                _nameFilter = value;
                OnPropertyChanged("NameFilter");
            }
        }
        public string SurnameFilter
        {
            get { return _surnameFilter; }
            set
            {
                _surnameFilter = value;
                OnPropertyChanged("SurnameFilter");
            }
        }
        #endregion

        #region Commands
        private bool Movie_CanExecute(object sender)
        {
            if (SelectedMovie != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void DeleteMovie_Executed(object sender)
        {
            _db.Delete(SelectedMovie);
            LoadCollections();
        }

        private void EditMovie_Executed(object obj)
        {
            MovieWindow newMovie = new MovieWindow(new MovieViewModel(DbManager.GetInstance(), SelectedMovie));
            newMovie.Show();
        }
        #endregion

        #region Methods
        public void LoadCollections()
        {
            _movies = new ObservableCollection<Movie>(_db.GetObjects<Movie>());
            _shows = new ObservableCollection<Show>(_db.GetObjects<Show>());
            _reservations = new ObservableCollection<Reservation>(_db.GetObjects<Reservation>());
        }

        public void AddReservation()
        {
            _reservation.ShowId = SelectedShow.Id;
            _db.Add(_reservation);
        }
        
        public override void Init(IDbManager db)
        {
            _db = db;
        }
        #endregion

        #region Filters
        public ListCollectionView ShowsView
        {
            get
            {
                return (ListCollectionView)CollectionViewSource.GetDefaultView(GetView(_shows));
            }
        }
        private ListCollectionView ReservationsView
        {
            get
            {
                return (ListCollectionView)CollectionViewSource.GetDefaultView(GetView(_reservations));
            }
        }
        private void RemoveFilter()
        {
            ShowsView.Filter = null;
        }
        private void ApplyDateFilter()
        {
            var currentDate = DateTime.Today;
            ShowsView.Filter = delegate (object item)
            {
                Show show = item as Show;
                if (show.ShowDate == currentDate)
                {
                    return true;
                }
                return false;
            };
        }
        private void ApplyTitleFilter()
        {
            ShowsView.Filter = delegate (object item)
            {
                Show show = item as Show;
                if (show.Movie.Title == SelectedMovie.Title)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
        }
        private void ReservFilterChanged(object sender, TextChangedEventArgs e)
        {
            ReservationsView.Filter = delegate (object item)
            {
                Reservation res = item as Reservation;
                if (IdFilter != "")
                {
                    int id;
                    if (int.TryParse(IdFilter, out id))
                    {
                        if (cultureInfo.CompareInfo.IndexOf(res.Name, NameFilter, CompareOptions.IgnoreCase) >= 0 && cultureInfo.CompareInfo.IndexOf(res.Surname, SurnameFilter, CompareOptions.IgnoreCase) >= 0 && res.Id == id)
                        {
                            return true;
                        }
                    }
                    return false;
                }
                else
                {
                    if (cultureInfo.CompareInfo.IndexOf(res.Name, NameFilter, CompareOptions.IgnoreCase) >= 0 && cultureInfo.CompareInfo.IndexOf(res.Surname, SurnameFilter, CompareOptions.IgnoreCase) >= 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            };
        }
        #endregion
    }
}
