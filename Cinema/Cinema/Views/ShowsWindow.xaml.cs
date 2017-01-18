using Cinema.Models;
using Cinema.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cinema.Views
{
    /// <summary>
    /// Interaction logic for ShowsWindow.xaml
    /// </summary>
    public partial class ShowsWindow : Window
    {
        private ShowsViewModel _viewModel;
        public ShowsWindow(ShowsViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _viewModel.GenerateTable = GenerateTable;
            _viewModel.Close = Close;
            gridMain.DataContext = _viewModel;

        }
        private void GenerateTable(object sender, RoutedEventArgs e)
        {
            if (CBMovies.SelectedIndex == -1 || CBHalls.SelectedIndex == -1) return;

            _viewModel.Filter(CBMovies.SelectedItem, CBHalls.SelectedItem);

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
                    foreach (Show s in _viewModel.ShowsToAdd)
                    {
                        if (!s.ShowDate.Equals(_viewModel.CurrentDate.AddDays(day)))
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
            _viewModel.NoFilter();
            _viewModel.SaveChanges();
            Close();
        }
        private void KeyShortcuts(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                if (BTPrev.IsEnabled == true)
                {
                    _viewModel.PrevWeek_Executed(null);
                }
            }
            if (e.Key == Key.Right)
            {
                if (BTNext.IsEnabled == true)
                {
                    _viewModel.NextWeek_Executed(null);
                }
            }
            e.Handled = true;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
