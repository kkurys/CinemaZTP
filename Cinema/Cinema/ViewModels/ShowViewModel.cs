using Cinema.Custom.Commands;
using Cinema.Interfaces;
using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Cinema.ViewModels
{
    public class ShowsViewModel : BaseViewModel, IShowViewModel, IObserver
    {
        #region fields
        private List<Show> _showsToRemove;

        private ObservableCollection<Movie> _movies;
        private ObservableCollection<string> _halls;
        private ObservableCollection<Show> _showsToAdd;

        private IDbManager _db;

        private DateTime _currentDate;
        private string _dateFrom, _dateTo;

        private Week _currentWeek;
        private int _currentWeekNumber;
        #endregion
        #region properties
        public ObservableCollection<Movie> Movies
        {
            get
            {
                return _movies;
            }
        }
        public ObservableCollection<Show> ShowsToAdd
        {
            get
            {
                return _showsToAdd;
            }
        }
        public ObservableCollection<string> Halls
        {
            get
            {
                return _halls;
            }
        }
        public Action<object, RoutedEventArgs> GenerateTable;
        public Action Close;
        public int ActiveMovie { get; set; }
        public int ActiveHall { get; set; }
        public ICommand AddShowCommand { get; set; }
        public ICommand RemoveShowCommand { get; set; }
        public ICommand PrevWeekCommand { get; set; }
        public ICommand NextWeekCommand { get; set; }

        public string DateFrom
        {
            get
            {
                return _dateFrom;
            }
            set
            {
                _dateFrom = value;
                OnPropertyChanged("DateFrom");
            }
        }
        public string DateTo
        {
            get
            {
                return _dateTo;
            }
            set
            {
                _dateTo = value;
                OnPropertyChanged("DateTo");
            }
        }
        public Week CurrentWeek
        {
            get
            {
                return _currentWeek;
            }
            set
            {
                _currentWeek = value;
                OnPropertyChanged("CurrentWeek");
            }
        }
        public DateTime CurrentDate
        {
            get
            {
                return _currentDate;
            }
            set
            {
                _currentDate = value;
                OnPropertyChanged("CurrentDate");
            }
        }
        #endregion
        #region ctors
        public ShowsViewModel(IDbManager db)
        {
            Init(db);
            _halls = new ObservableCollection<string> { "Wszystkie", "1", "2", "3", "4", "5" };

            _showsToRemove = new List<Show>();

            CurrentDate = DateTime.Today;
            DateFrom = CurrentDate.ToString("dd-MM-yyyy");
            DateTo = CurrentDate.AddDays(7).ToString("dd-MM-yyyy");

            CurrentWeek = new Week(_currentDate);

            AddShowCommand = new RelayCommand(AddShow_Executed, AddShow_CanExecute);
            RemoveShowCommand = new RelayCommand(RemoveShow_Executed);
            PrevWeekCommand = new RelayCommand(PrevWeek_Executed, PrevWeek_CanExecute);
            NextWeekCommand = new RelayCommand(NextWeek_Executed);

            GetView(_showsToAdd).GroupDescriptions.Add(new PropertyGroupDescription("Title"));
            GetView(_showsToAdd).SortDescriptions.Add(new SortDescription("ShowDate", ListSortDirection.Ascending));
        }
        #endregion

        #region methods
        public void Filter(params object[] parameters)
        {
            var movie = parameters[0] as Movie;
            var hall = parameters[1] as string;
            if (movie != null && hall != null)
            {
                GetView(ShowsToAdd).Filter = delegate (object item)
                {
                    Show s = item as Show;
                    if (s != null)
                    {
                        if (movie.Title != "Wszystkie")
                        {
                            if (hall != "Wszystkie")
                            {
                                if (s.Movie == movie && s.Hall == int.Parse(hall) && s.ShowDate.Value.CompareTo(_currentDate) >= 0 && s.ShowDate.Value.CompareTo(_currentDate.AddDays(7)) <= 0)
                                {
                                    return true;
                                }
                                return false;
                            }
                            else
                            {
                                if (s.Movie == movie && s.ShowDate.Value.CompareTo(_currentDate) >= 0 && s.ShowDate.Value.CompareTo(_currentDate.AddDays(7)) <= 0)
                                {
                                    return true;
                                }
                                return false;
                            }
                        }
                        else
                        {
                            if (hall != "Wszystkie")
                            {
                                if (s.Hall == int.Parse(hall) && s.ShowDate.Value.CompareTo(_currentDate) >= 0 && s.ShowDate.Value.CompareTo(_currentDate.AddDays(7)) <= 0)
                                {
                                    return true;
                                }
                                return false;
                            }
                            else
                            {
                                if (s.ShowDate.Value.CompareTo(_currentDate) >= 0 && s.ShowDate.Value.CompareTo(_currentDate.AddDays(7)) <= 0)
                                {
                                    return true;
                                }
                                return false;
                            }
                        }
                    }
                    return false;

                };
            }

        }
        public void NoFilter()
        {
            GetView(ShowsToAdd).Filter = null;
        }
        public override void Init(IDbManager db)
        {
            _db = db;
            _movies = new ObservableCollection<Movie>(db.GetObjects<Movie>());
            _showsToAdd = new ObservableCollection<Show>(db.GetObjects<Show>());

        }
        public void SaveChanges()
        {
            foreach (Show s in _showsToRemove)
            {
                _db.Delete(s);
            }
            foreach (Show s in ShowsToAdd)
            {
                _db.Add(s);
            }
            Close();
        }
        #endregion
        #region commands
        public void AddShow_Executed(object sender)
        {
            TimeSpan ts = new TimeSpan(10, 0, 0);
            Button btn = sender as Button;

            int row = Grid.GetRow(btn);
            int day = Grid.GetColumn(btn);

            for (int i = 0; i < row; i++)
            {
                ts = ts.Add(new TimeSpan(0, 30, 0));
            }

            Show show = new Show();
            show.Movie = Movies[ActiveMovie - 1];
            show.MovieId = show.Movie.Id;
            show.StartTime = ts;
            show.EndTime = ts.Add(new TimeSpan(0, show.Movie.Length, 0));
            show.ShowDate = _currentDate.AddDays(day);

            show.Hall = int.Parse(_halls[ActiveHall]);
            ShowsToAdd.Add(show);
            GenerateTable(null, null);
        }
        public void RemoveShow_Executed(object sender)
        {
            Show s = sender as Show;
            TimeSpan diff = s.ShowDate.Value - _currentDate;
            int day = diff.Days;

            _showsToRemove.Add(s);
            ShowsToAdd.Remove(s);
            GenerateTable(null, null);
        }
        public bool AddShow_CanExecute(object sender)
        {
            if (ActiveMovie < 1 || ActiveHall < 1)
            {
                return false;
            }

            Movie mov = _movies[ActiveMovie - 1];
            Button btn = sender as Button;
            if (btn == null) return false;
            int days = Grid.GetColumn(btn);
            string hall = Halls[ActiveHall];

            if (mov.PremiereDate != null && mov.PremiereDate.Value.CompareTo(_currentDate.AddDays(days)) <= 0 && mov.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void NextWeek_Executed(object sender)
        {
            if (DateFrom != null && DateTo != null)
            {
                _currentDate = _currentDate.AddDays(7);
                DateFrom = _currentDate.ToString("dd/MM/yyyy");
                DateTo = _currentDate.AddDays(7).ToString("dd/MM/yyyy");
                CurrentWeek = new Week(_currentDate);
                _currentWeekNumber++;
                GenerateTable(null, null);
            }
        }
        public void PrevWeek_Executed(object sender)
        {
            _currentDate = _currentDate.AddDays(-7);
            if (DateFrom != null)
            {
                DateFrom = _currentDate.ToString("dd/MM/yyyy");
            }
            if (DateTo != null)
            {
                DateTo = _currentDate.AddDays(7).ToString("dd/MM/yyyy");
            }
            CurrentWeek = new Week(_currentDate);
            _currentWeekNumber--;
            GenerateTable(null, null);
        }

        public bool PrevWeek_CanExecute(object sender)
        {
            if (DateFrom != null && DateFrom != DateTime.Today.ToString("dd/MM/yyyy"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        // OBSERVER METHODE
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
                ShowsToAdd.Clear();

                foreach (var show in _actualShows)
                {
                    ShowsToAdd.Add(show);
                }
            }

        }
        #endregion
    }
}
