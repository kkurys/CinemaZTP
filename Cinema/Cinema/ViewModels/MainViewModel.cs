using Cinema.Custom.Commands;
using Cinema.Interfaces;
using Cinema.Models;
using Cinema.Views;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cinema.ViewModels
{
    class MainViewModel : BaseViewModel, IMainViewModel, IObserver
    {
        #region Fields
        private IDbManager _db;
        private string _dateFormat = "yyyy/MM/dd";

        CultureInfo cultureInfo;
        
        private string _reservationSeats;
        private string _reservationName;
        private string _reservationSurname;
        private bool _reservationWasPaid;
        private Ticket _reservationTicketType;

        private Movie _selectedMovie;
        private Show _selectedShow;
        private Reservation _selectedReservation;
        private string _idFilter;
        private string _nameFilter;
        private string _surnameFilter;
        private string _showDateFilter;
        private bool _reservationErrors;

        private ObservableCollection<Show> _shows;
        private ObservableCollection<Movie> _movies;
        private ObservableCollection<Reservation> _reservations;
        #endregion

        #region Ctors
        public MainViewModel(IDbManager db)
        {
            cultureInfo = new CultureInfo("pl-PL");
            ShowDateFilter = DateTime.Now.ToString(_dateFormat);

            DeleteMovieCommand = new RelayCommand(DeleteMovie_Executed, Movie_CanExecute);
            EditMovieCommand = new RelayCommand(EditMovie_Executed, Movie_CanExecute);
            AddMovieCommand = new RelayCommand(AddMovie_Executed);
            NextDayCommand = new RelayCommand(NextDay_Executed);
            PreviousDayCommand = new RelayCommand(PreviousDay_Executed);
            ShowManageCommand = new RelayCommand(ShowManage_Executed);
            AddReservationCommand = new RelayCommand(AddReservation_Executed, AddReservation_CanExecute);
            HallCommand = new RelayCommand(Hall_Executed, Hall_CanExecute);
            DeleteReservationCommand = new RelayCommand(DeleteReservation_Executed, DeleteReservation_CanExecute);

            Init(db);
            ApplyDateFilter();
        }
        #endregion

        #region Properties
        public ObservableCollection<Reservation> Reservations
        {
            get
            {
                return _reservations;
            }

        }
        public ObservableCollection<Show> Shows
        {
            get
            {
                return _shows;
            }

        }
        public ObservableCollection<Movie> Movies
        {
            get
            {
                return _movies;
            }
        }
        public string ReservationSeats
        {
            get { return _reservationSeats; }
            set
            {
                _reservationSeats = value;
                OnPropertyChanged("ReservationSeats");
            }
        }
        public string ReservationName
        {
            get { return _reservationName; }
            set
            {
                _reservationName = value;
                OnPropertyChanged("ReservationName");
            }
        }
        public string ReservationSurname
        {
            get { return _reservationSurname; }
            set
            {
                _reservationSurname = value;
                OnPropertyChanged("ReservationSurname");
            }
        }
        public bool ReservationWasPaid
        {
            get { return _reservationWasPaid; }
            set
            {
                _reservationWasPaid = value;
                OnPropertyChanged("ReservationWasPaid");
            }
        }
        public Ticket ReservationTicketType
        {
            get { return _reservationTicketType; }
            set
            {
                _reservationTicketType = value;
                OnPropertyChanged("ReservationTicketType");
            }
        }
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
        public Reservation SelectedReservation
        {
            get { return _selectedReservation; }
            set
            {
                _selectedReservation = value;
                OnPropertyChanged("SelectedReservation");
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

        public string ShowDateFilter
        {
            get { return _showDateFilter; }
            set
            {
                _showDateFilter = value;
                OnPropertyChanged("ShowDateFilter");
            }
        }
        public bool ReservationErrors
        {
            get { return _reservationErrors; }
            set
            {
                _reservationErrors = value;
                OnPropertyChanged("ReservationErrors");
            }
        }
        public ICommand DeleteMovieCommand { get; set; }
        public ICommand EditMovieCommand { get; set; }
        public ICommand AddMovieCommand { get; set; }
        public ICommand NextDayCommand { get; set; }
        public ICommand PreviousDayCommand { get; set; }
        public ICommand ShowManageCommand { get; set; }
        public ICommand AddReservationCommand { get; set; }
        public ICommand HallCommand { get; set; }
        public ICommand DeleteReservationCommand { get; set; }

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
        }
        private void EditMovie_Executed(object obj)
        {
            MovieWindow newMovie = new MovieWindow(new MovieViewModel(DbManager.GetInstance(), SelectedMovie));
            newMovie.Show();
        }
        private void AddMovie_Executed(object obj)
        {
            MovieWindow newMovie = new MovieWindow(new MovieViewModel(DbManager.GetInstance()));
            newMovie.Show();
        }
        private void PreviousDay_Executed(object obj)
        {
            RemoveFilter();
            var nextDay = DateTime.ParseExact(_showDateFilter, _dateFormat, null);
            ApplyDateFilter(nextDay.AddDays(-1));
        }
        private void NextDay_Executed(object obj)
        {
            RemoveFilter();
            var nextDay = DateTime.ParseExact(_showDateFilter, _dateFormat, null);
            ApplyDateFilter(nextDay.AddDays(1));
        }
        private void ShowManage_Executed(object obj)
        {
            ShowsWindow showManage = new ShowsWindow(new ShowsViewModel(DbManager.GetInstance()));
            showManage.Show();
        }
        private bool AddReservation_CanExecute(object obj)
        {
            if (_selectedShow != null && !ReservationErrors)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void AddReservation_Executed(object obj)
        {
            Reservation newReservation = new Reservation();
            newReservation.ShowId = SelectedShow.Id;
            newReservation.Seats = ReservationSeats;
            newReservation.Name = ReservationName;
            newReservation.Surname = ReservationSurname;
            newReservation.WasPaid = ReservationWasPaid;
            newReservation.TicketType = ReservationTicketType;
            _db.Add(newReservation);
            ReservationSeats = "";
            ReservationName = "";
            ReservationSurname = "";
            ReservationWasPaid = false;
            ReservationTicketType = Ticket.Standard;
        }
        private bool Hall_CanExecute(object obj)
        {
            if (_selectedShow != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Hall_Executed(object obj)
        {
            HallWindow hall = new HallWindow(SelectedShow);
            if (hall.ShowDialog() == true)
            {
                ReservationSeats = String.Join(";", hall.reservedSeats.ToArray());
            }
        }
        private bool DeleteReservation_CanExecute(object obj)
        {
            if (SelectedReservation != null && SelectedReservation.WasPaid == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void DeleteReservation_Executed(object obj)
        {
            _db.Delete(SelectedReservation);
        }
        #endregion

        #region Methods
        public void SaveReservation()
        {
            _db.Update(_selectedReservation);
        }

        public override void Init(IDbManager db)
        {
            _db = db;
            _movies = new ObservableCollection<Movie>(_db.GetObjects<Movie>());
            _shows = new ObservableCollection<Show>(_db.GetObjects<Show>());
            _reservations = new ObservableCollection<Reservation>(_db.GetObjects<Reservation>());
        }
        #endregion

        #region Filters
        private void RemoveFilter()
        {
            GetView(_shows).Filter = null;
        }
        private void ApplyDateFilter()
        {
            var currentDate = DateTime.Today;
            var view = GetView(_shows);
            if (view != null)
            {
                GetView(_shows).Filter = delegate (object item)
                {
                    Show show = item as Show;
                    if (show.ShowDate.Value.Date == currentDate.Date)
                    {
                        return true;
                    }
                    return false;
                };
            }
            ShowDateFilter = currentDate.ToString(_dateFormat);
        }
        private void ApplyDateFilter(DateTime dateToSet)
        {
            var currentDate = dateToSet;
            var view = GetView(_shows);
            if (view != null)
            {
                GetView(_shows).Filter = delegate (object item)
                {
                    Show show = item as Show;
                    if (show.ShowDate.Value.Date == currentDate.Date)
                    {
                        return true;
                    }
                    return false;
                };
            }
            ShowDateFilter = currentDate.ToString(_dateFormat);
        }
        private void ApplyTitleFilter()
        {
            GetView(_shows).Filter = delegate (object item)
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
            GetView(_reservations).Filter = delegate (object item)
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

        #region observer 
        public void Update(Type t)
        {
            if (t == typeof(Movie))
            {
                var _actualMovies = _db.GetObjects<Movie>();
                Movies.Clear();

                foreach (var movie in _actualMovies)
                {
                    Movies.Add(movie);
                }
            }
            else if (t == typeof(Show))
            {
                var _actualShows = _db.GetObjects<Show>();
                Shows.Clear();

                foreach (var show in _actualShows)
                {
                    Shows.Add(show);
                }
            }
            else if (t == typeof(Reservation))
            {
                var _actualReservations = _db.GetObjects<Reservation>();
                Reservations.Clear();

                foreach (var reservation in _actualReservations)
                {
                    Reservations.Add(reservation);
                }
            }
        }
        #endregion
    }
}
