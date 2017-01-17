using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cinema.Views
{
    /// <summary>
    /// Interaction logic for ShowsWindow.xaml
    /// </summary>
    public partial class ShowsWindow : Window
    {
        public ShowsWindow()
        {
            InitializeComponent();
        }
        private void GenerateTable(object sender, RoutedEventArgs e)
        {
            /*
            if (CBMovies.SelectedIndex == -1 || CBHalls.SelectedIndex == -1) return;
            Filter();
            TurnAllButtonsOn();
            Button btn;
            TimeSpan tempStartTime = new TimeSpan(10, 0, 0);
            TimeSpan tempEndTime = tempStartTime.Add(new TimeSpan(0, (CBMovies.SelectedItem as Movie).Length, 0)).Add(new TimeSpan(0, 30, 0));
            int row;
            bool cont = false;
            for (int day = 0; day < 7; day++)
            {
                tempStartTime = new TimeSpan(10, 0, 0);
                tempEndTime = tempStartTime.Add(new TimeSpan(0, (CBMovies.SelectedItem as Movie).Length, 0));
                row = 0;
                while (tempStartTime.CompareTo(new TimeSpan(22, 30, 0)) < 0)
                {
                    cont = false;
                    btn = LogicalTreeHelper.FindLogicalNode(DataGrid, "btn" + row + "" + day) as Button;
                    foreach (Show s in showsToAdd)
                    {
                        if (!s.ShowDate.Equals(dt.AddDays(day)))
                        {
                            continue;
                        }
                        else
                        {
                            int hall;
                            if (int.TryParse(CBHalls.SelectedItem as string, out hall))
                            {
                                if (hall == s.Hall && ((tempEndTime.CompareTo(s.StartTime) >= 0 && tempEndTime.CompareTo(s.EndTime) <= 0) ||
                                    (tempStartTime.CompareTo(s.StartTime) >= 0 && tempStartTime.CompareTo(s.EndTime) <= 0)))
                                {
                                    cont = true;
                                    break;
                                }
                            }

                        }
                    }

                    if (cont)
                        btn.IsEnabled = false;

                    row++;
                    tempStartTime = tempStartTime.Add(new TimeSpan(0, 30, 0));
                    tempEndTime = tempEndTime.Add(new TimeSpan(0, 30, 0));
                }
            }
            */
        }

        private void TurnAllButtonsOn()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    Button btn = LogicalTreeHelper.FindLogicalNode(DataGrid, "btn" + j + "" + i) as Button;
                    btn.IsEnabled = true;
                }
            }
        }
        private void FinishEditing(object sender, RoutedEventArgs e)
        {
            /*
            View.Filter = null;
            foreach (Show s in showsToRemove)
            {
                if (data.Shows.Contains(s))
                {
                    data.Shows.Remove(s);
                    s.Movie.NumberOfShows--;
                }
            }
            foreach (Show s in showsToAdd)
            {
                if (!data.Shows.Contains(s))
                {
                    data.Shows.Add(s);
                    s.Movie.NumberOfShows++;
                }
            } */
            Close();
        }
        private void KeyShortcuts(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                if (BTPrev.IsEnabled == true)
                {
                 //   PrevWeek_Executed(null, null);
                }
            }
            if (e.Key == Key.Right)
            {
                if (BTNext.IsEnabled == true)
                {
                //    NextWeek_Executed(null, null);
                }
            }
            e.Handled = true;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void Add_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }
        public void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
