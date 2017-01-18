using Cinema.Models;
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
    /// Interaction logic for TicketWindow.xaml
    /// </summary>
    public partial class TicketWindow : Window
    {
        private ITicket _ticket;
        public TicketWindow(ITicket ticket)
        {
            InitializeComponent();
            _ticket = ticket;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IdentityLabel.Content = _ticket.Identity;
            SeatsLabel.Content = _ticket.getSeats();
            HallLabel.Content = _ticket.getShow().Hall;
            TitleLabel.Content = _ticket.getShow().Title;

            if (_ticket is TicketBuilder.Ticket)
            {
                PrintButton.Content = "Drukuj";
            }
            else if (_ticket is TicketBuilder.ETicket)
            {
                PrintButton.Content = "Wyślij";
            }
        }
    }
}
