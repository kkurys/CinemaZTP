using Cinema.Custom.Commands;
using Cinema.Interfaces;
using Cinema.Models;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cinema.ViewModels
{
    public class MovieViewModel : BaseViewModel, IMovieViewModel
    {
        private IDbManager _db;
        private Movie _movie;
        private Image _img;
        private DateTime? _premiereDate;
        private bool _isEdit, _movieErrors;
        private string _description, _director, _genre, _imageFilename, _writer, _title, _production;
        private int _length;
        #region ctors
        public MovieViewModel(IDbManager db)
        {
            Init(db);
            AddCommand = new RelayCommand(AddCommand_Execute, AddCommand_CanExecute);

        }
        public MovieViewModel(IDbManager db, Movie movie)
        {
            _movie = movie;
            Description = movie.Description;
            Director = movie.Director;
            Genre = movie.Genre;
            Length = movie.Length;
            ImageFilename = movie.ImageFileName;
            PremiereDate = movie.PremiereDate;
            Writer = movie.Writer;
            Title = movie.Title;
            _isEdit = true;
            Init(db);
            AddCommand = new RelayCommand(AddCommand_Execute, AddCommand_CanExecute);
        }
        #endregion
        #region properties
        public bool MovieErrors
        {
            get
            {
                return _movieErrors;
            }
            set
            {
                _movieErrors = value;
                OnPropertyChanged("MovieErrors");
            }
        }
        public Image Img
        {
            get
            {
                return _img;
            }
            set
            {
                _img = value;
                OnPropertyChanged("Img");
            }
        }
        public string ImageFilename
        {
            get
            {
                return _imageFilename;
            }
            set
            {
                _imageFilename = value;
                OnPropertyChanged("ImageFilename");
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
        public string Director
        {
            get
            {
                return _director;
            }
            set
            {
                _director = value;
                OnPropertyChanged("Director");
            }
        }
        public string Genre
        {
            get
            {
                return _genre;
            }
            set
            {
                _genre = value;
                OnPropertyChanged("Genre");
            }
        }
        public string Writer
        {
            get
            {
                return _writer;
            }
            set
            {
                _writer = value;
                OnPropertyChanged("Writer");
            }
        }
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Production
        {
            get
            {
                return _production;
            }
            set
            {
                _production = value;
                OnPropertyChanged("Production");
            }
        }
        public DateTime? PremiereDate
        {
            get
            {
                return _premiereDate;
            }
            set
            {
                _premiereDate = value;
                OnPropertyChanged("PremiereDate");
            }

        }
        public int Length
        {
            get
            {
                return _length;
            }
            set
            {
                _length = value;
                OnPropertyChanged("Length");
            }
        }
        public ICommand AddCommand
        {
            get;
            set;
        }
        public Action Close
        {
            get;
            set;
        }
        #endregion

        #region methods
        public override void Init(IDbManager db)
        {
            _db = db;
        }
        private bool AddCommand_CanExecute(object sender)
        {
            if (!MovieErrors)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void AddCommand_Execute(object sender)
        {
            if (!_isEdit)
            {
                _movie = new Movie();
            }
            _movie.Description = Description;
            _movie.Director = Director;
            _movie.Genre = Genre;
            _movie.ImageFileName = ImageFilename;
            _movie.PremiereDate = PremiereDate;
            _movie.Title = Title;

            if (_isEdit)
            {
                _db.Update(_movie);
            }
            else
            {
                _db.Add(_movie);
            }
            Close();
        }
        #endregion
    }
}
