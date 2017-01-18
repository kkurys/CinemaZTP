using Cinema.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Cinema.Views
{
    /// <summary>
    /// Interaction logic for HallWindow.xaml
    /// </summary>
    public partial class HallWindow : Window
    {
        public List<string> reservedSeats;

        public HallWindow(Show show)
        {
            InitializeComponent();
            reservedSeats = new List<string>();
            PrepareHall(show);
        }
        private void SeatClick(object sender, RoutedEventArgs e)
        {
            Button CurrentSeat = (Button)sender;

            if (CurrentSeat.Background == Brushes.DeepSkyBlue)   // odznaczanie
            {
                CurrentSeat.ClearValue(BackgroundProperty);
                reservedSeats.Remove(CurrentSeat.Content.ToString());

            }
            else    // zaznaczanie
            {
                CurrentSeat.SetValue(BackgroundProperty, Brushes.DeepSkyBlue);
                reservedSeats.Add(CurrentSeat.Content.ToString());
            }

            reservedSeats.Sort();
        }
        private void PrepareHall(Show show)
        {
            Button btn;
            foreach (Reservation reservation in show.Reservation)
            {
                if (reservation.Seats != null)
                {
                    string[] seatsTaken = reservation.Seats.Split(';');
                    foreach (string seat in seatsTaken)
                    {
                        btn = LogicalTreeHelper.FindLogicalNode(SeatsGrid, seat) as Button;
                        btn.IsEnabled = false;
                    }
                }
            }
            foreach (string seat in reservedSeats)
            {
                btn = LogicalTreeHelper.FindLogicalNode(SeatsGrid, seat) as Button;
                btn.SetValue(BackgroundProperty, Brushes.DeepSkyBlue);
            }
        }
        private void AcceptButton(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
