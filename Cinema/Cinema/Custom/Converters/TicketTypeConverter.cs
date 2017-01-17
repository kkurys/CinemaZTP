using Cinema.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Cinema.converters
{
    class TicketTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Ticket ticketType = (Ticket)value;
            if (ticketType == (Ticket)Enum.Parse(typeof(Ticket), parameter.ToString()))
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }
    }
}
