using Cinema.Custom.Commands;
using Cinema.Interfaces;
using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Cinema.ViewModels
{
    public class ShowsViewModel : BaseViewModel, IShowViewModel
    {
        #region fields
        private List<Show> _showsToRemove;

        private ObservableCollection<Movie> _movies;
        private ObservableCollection<string> _halls;
        private ObservableCollection<Show> _showsToAdd;

        private ICommand _addShowCmd;
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
        public Action<object, RoutedEventArgs> Action;
        public int ActiveMovie { get; set; }
        public int ActiveHall { get; set; }
        public ICommand AddShowCommand { get; set; }
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
        #endregion
        #region ctors
        public ShowsViewModel(IDbManager db)
        {
            Init(db);
            _halls = new ObservableCollection<string> { "Wszystkie", "1", "2", "3", "4", "5" };
            _showsToAdd = new ObservableCollection<Show>();
            _showsToRemove = new List<Show>();

            AddShowCommand = new RelayCommand(AddShow_Executed, AddShow_CanExecute);
            PrevWeekCommand = new RelayCommand(PrevWeek_Executed, PrevWeek_CanExecute);
            NextWeekCommand = new RelayCommand(NextWeek_Executed);
        }
        #endregion

        #region methods
        public void Filter(params object[] parameters)
        {
            if (!(parameters[2] is DateTime))
            {
                return;
            }
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
            show.Movie = Movies[ActiveMovie];
            show.StartTime = ts;
            show.ShowDate = _currentDate.AddDays(day);
            show.Hall = int.Parse(_halls[ActiveHall]);
            ShowsToAdd.Add(show);
            Action.Invoke(null, null);
        }
        public bool AddShow_CanExecute(object sender)
        {
            if (ActiveMovie < 1 || ActiveHall < 1)
            {
                return false;
            }

            Movie mov = _movies[ActiveMovie];
            Button btn = sender as Button;

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
                Filter();
                _currentWeekNumber++;
                Action.Invoke(null, null);
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
            Filter();
            _currentWeekNumber--;
            Action.Invoke(null, null);
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
    }
}
