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
        public MainWindow()
        {
            InitializeComponent();
            MainGrid.DataContext = new MainViewModel();
        }

        #region methods
        private void ReservationSelected(object sender, SelectionChangedEventArgs e)
        {
            /*         if (LBReservedList.SelectedIndex > -1 && CBPaid.IsChecked == false)
                     {
                         GBReservationDetails.IsEnabled = true;
                     }
                     else
                     {
                         GBReservationDetails.IsEnabled = false;
                     }
                     */
            //if (GBReservationDetails.IsEnabled == false && CBPaid.IsChecked == false)
            //{
            //    GBReservationDetails.IsEnabled = true;
            //}
            //else
            //{
            //    GBReservationDetails.IsEnabled = false;
            //}
        }
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
            MovieWindow newMovie = new MovieWindow(new MovieViewModel(DbManager.Instance()) { Close = Close });
            newMovie.Show();
        }
        /*
        public int FindAvailableId()
        {
            int id = 1;
            bool sw = true;
            if (db.Reservations == null || db.Reservations.Count == 0)
            {
                sw = false;
            }
            while (sw)
            {
                foreach (Reservation r in db.Reservations)
                {
                    if (r.Id == id)
                    {
                        id++;
                        sw = true;
                        break;
                    }
                    sw = false;
                }
            }
            return id;
        } */
        private void OpenShowsWindow(object sender, RoutedEventArgs e)
        {
            //    ShowsWindow shows = new ShowsWindow(db);

            //shows.Show();
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
        private void KeyShortcuts(object sender, KeyEventArgs e)
        {
            /*
            if (TCMainWindow.SelectedIndex == 1)
            {
                if (e.Key == Key.Left)
                {
                    if (BTPrev.IsEnabled == true)
                    {
                        //PrevWeek_Executed(null, null);
                    }
                    e.Handled = true;
                }
                if (e.Key == Key.Right)
                {
                    if (BTNext.IsEnabled == true)
                    {
                      //  NextWeek_Executed(null, null);
                    }
                    e.Handled = true;
                }
            } */
        }
        private void EditMovie(object sender, MouseButtonEventArgs e)
        {
            //  AddMovieWindow newMovie = new AddMovieWindow(LBRepertuar.SelectedItem as Movie, db);
            //   newMovie.Show();
        }
        #endregion
        #region groups&sorts methods
        private ListCollectionView MoviesView
        {
            get
            {
                //        return (ListCollectionView)CollectionViewSource.GetDefaultView(db.Movies);
                return null;
            }
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
        #region filters
        public ListCollectionView ShowsView
        {
            get
            {
                //      return (ListCollectionView)CollectionViewSource.GetDefaultView(db.Shows);
                return null;
            }
        }
        private ListCollectionView ReservationsView
        {
            get
            {
                //       return (ListCollectionView)CollectionViewSource.GetDefaultView(db.Reservations);
                return null;
            }
        }
        private void RemoveFilter()
        {
            //    ShowsView.Filter = null;
        }
        private void ApplyDateFilter()
        {
            /*     var currentDate = DateTime.Today;
                 ShowsView.Filter = delegate (object item)
                 {
                     Show show = item as Show;
                     if (show.ShowDate == currentDate)
                     {
                         return true;
                     }
                     return false;
                 }; */
        }
        private void ApplyTitleFilter()
        {
            /*
            ShowsView.Filter = delegate (object item)
            {
                Show show = item as Show;
                if (show.Movie.Title == (LBRepertuar.SelectedItem as Movie).Title)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
            */
        }
        private void ReservFilterChanged(object sender, TextChangedEventArgs e)
        {
            /*
            ReservationsView.Filter = delegate (object item)
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
            }; */
        }
        #endregion

    }
}
