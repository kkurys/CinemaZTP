using Cinema.ViewModels;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Cinema.Views
{
    /// <summary>
    /// Interaction logic for MovieWindow.xaml
    /// </summary>
    public partial class MovieWindow : Window
    {
        MovieViewModel _viewModel;
        public MovieWindow(MovieViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            _viewModel.Close = Close;
        }
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = _viewModel;
        }
        #region validation
        private bool HasErrors(DependencyObject gridInfo)
        {
            foreach (object child in LogicalTreeHelper.GetChildren(gridInfo))
            {
                TextBox element = child as TextBox;
                if (element == null)
                {
                    continue;
                }
                if (Validation.GetHasError(element) || HasErrors(element))
                {
                    return true;
                }
            }
            return false;
        }
        private void movieValidationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                _viewModel.MovieErrors = true;
            }
            else if (!HasErrors(MovieGrid))
            {
                _viewModel.MovieErrors = false;
            }
        }
        #endregion
        #region events
        private void BTLoadPoster(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Wybierz obraz";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                //    File.Copy(op.FileName, "/Images/");
                image.Source = new BitmapImage(new Uri(op.FileName));
                _viewModel.Img = new Image();
                _viewModel.Img.Source = new BitmapImage(new Uri(op.FileName));
                _viewModel.ImageFilename = op.FileName;
            }
        }
        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion
    }
}
