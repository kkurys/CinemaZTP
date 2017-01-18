using Cinema.Models;
using Cinema.ViewModels;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Cinema.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CultureInfo cultureInfo = new CultureInfo("pl-PL");
        MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new MainViewModel(DbManager.GetInstance());
            DbManager.GetInstance().InitKeeper();
            MainGrid.DataContext = _viewModel;
        }
        #region methods
        private void WasPaid(object sender, RoutedEventArgs e)
        {
            if (CBPaid != null && CBPaid.IsChecked == true)
            {
                CBPaid.IsEnabled = false;
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            // WriteToBinaryFile<Database>("database.bin", db);
            this.Close();
        }

        private void AddMovie(object sender, RoutedEventArgs e)
        {
            MovieWindow newMovie = new MovieWindow(new MovieViewModel(DbManager.GetInstance()) { Close = Close });
            newMovie.Show();
        }
        private void DisplayShowsForToday(object sender, RoutedEventArgs e)
        {
            /*
            BTShowAll.Visibility = Visibility.Collapsed;
            BTPrev.Visibility = Visibility.Visible;
            BTNext.Visibility = Visibility.Visible;
            LBCurrentDate.Visibility = Visibility.Visible;
            ApplyDateFilter();
            GroupHours();
            TBGroupHeader.Text = "Wszystkie seanse"; */
        }
        private void EditMovie(object sender, MouseButtonEventArgs e)
        {
            //  AddMovieWindow newMovie = new AddMovieWindow(LBRepertuar.SelectedItem as Movie, db);
            //   newMovie.Show();
        }
        #endregion
        #region groups&sorts methods
        private void ReservFilterChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.GetView(_viewModel.Reservations).Filter = delegate (object item)
            {
                Reservation res = item as Reservation;
                if (TBId.Text != "")
                {
                    int id;
                    if (int.TryParse(TBId.Text, out id))
                    {
                        if (cultureInfo.CompareInfo.IndexOf(res.Name, TBResName.Text, CompareOptions.IgnoreCase) >= 0 && cultureInfo.CompareInfo.IndexOf(res.Surname, TBResSurname.Text, CompareOptions.IgnoreCase) >= 0 && res.Id == id)
                        {
                            return true;
                        }
                    }
                    return false;
                }
                else
                {
                    if (cultureInfo.CompareInfo.IndexOf(res.Name, TBResName.Text, CompareOptions.IgnoreCase) >= 0 && cultureInfo.CompareInfo.IndexOf(res.Surname, TBResSurname.Text, CompareOptions.IgnoreCase) >= 0)
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

        private void SortNone(object sender, RoutedEventArgs e)
        {
            /*
            MoviesView.SortDescriptions.Clear();
            */
        }

        private void SortTitle(object sender, RoutedEventArgs e)
        {
            // MoviesView.SortDescriptions.Clear();
            //    MoviesView.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
        }

        private void SortPremiereDate(object sender, RoutedEventArgs e)
        {
            //   MoviesView.SortDescriptions.Clear();
            //   MoviesView.SortDescriptions.Add(new SortDescription("PremiereDate", ListSortDirection.Descending));
        }
        private void SortStartTime()
        {
            // ShowsView.SortDescriptions.Clear();
            //  ShowsView.SortDescriptions.Add(new SortDescription("StartTime", ListSortDirection.Ascending));
        }
        private void SortShowDate()
        {
            //   ShowsView.SortDescriptions.Clear();
            //   ShowsView.SortDescriptions.Add(new SortDescription("ShowDate", ListSortDirection.Ascending));
        }
        private void GroupNone(object sender, RoutedEventArgs e)
        {
            //  MoviesView.GroupDescriptions.Clear();
        }

        private void GroupGenre(object sender, RoutedEventArgs e)
        {
            //  MoviesView.GroupDescriptions.Clear();
            //  MoviesView.GroupDescriptions.Add(new PropertyGroupDescription("Genre"));
        }

        private void GroupProduction(object sender, RoutedEventArgs e)
        {
            // MoviesView.GroupDescriptions.Clear();
            //  MoviesView.GroupDescriptions.Add(new PropertyGroupDescription("Production"));
        }
        private void GroupDate()
        {
            //  ShowsView.GroupDescriptions.Clear();
            //  ShowsView.GroupDescriptions.Add(new PropertyGroupDescription("ShortDate"));

        }
        private void GroupHours()
        {
            //   ShowsView.GroupDescriptions.Clear();
            //      ShowsView.GroupDescriptions.Add(new PropertyGroupDescription("StartTime", showsGrouper));
        }
        #endregion

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
        private void reservationValidationError(object sender, ValidationErrorEventArgs e)
        {

            if (e.Action == ValidationErrorEventAction.Added)
            {
                _viewModel.ReservationErrors = true;
            }
            else if (!HasErrors(NewReservationGrid))
            {
                _viewModel.ReservationErrors = false;
            }
        }

        private void CBPaid_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveReservation();
        }

        private void LBReservedList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LBReservedList.SelectedIndex > -1 && ((Reservation)LBReservedList.SelectedItem).WasPaid == false)
            {
                GBReservationDetails.IsEnabled = true;
            }
            else
            {
                GBReservationDetails.IsEnabled = false;
            }
        }
    }
}
