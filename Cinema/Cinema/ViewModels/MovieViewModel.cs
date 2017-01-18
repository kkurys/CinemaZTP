using Cinema.Interfaces;
using Cinema.Models;
using System;
using System.Windows.Controls;

namespace Cinema.ViewModels
{
    public class MovieViewModel : BaseViewModel, IMovieViewModel, IObserver
    {
        private IDbManager _db;
        private Movie _newMovie, _oldMovie;
        private Image _img;
        private bool _isEdit, _movieErrors;
        #region ctors
        public MovieViewModel(IDbManager db)
        {
            _newMovie = new Movie();
            Init(db);
        }
        public MovieViewModel(IDbManager db, Movie movie)
        {
            _newMovie = new Movie();

            _newMovie.Description = movie.Description;
            _newMovie.Director = movie.Director;
            _newMovie.Genre = movie.Genre;
            _newMovie.Length = movie.Length;
            _newMovie.ImageFileName = movie.ImageFileName;
            _newMovie.PremiereDate = movie.PremiereDate;
            _newMovie.Writer = movie.Writer;

            Init(db);
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
            get; set;
        }
        #endregion

        #region methods
        public override void Init(IDbManager db)
        {
            _db = db;
        }
        #endregion
        // OBSERVER METHODE
        #region observer
        public void Update(Type t)
        {
            _movie = new ObservableCollection<Movie>(_db.GetObjects<Movie>());
        }
        #endregion
    }
}
