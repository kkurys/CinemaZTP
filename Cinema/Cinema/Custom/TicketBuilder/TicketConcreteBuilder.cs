using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Models;

namespace Cinema.TicketBuilder
{
    class TicketConcreteBuilder : Cinema.TicketBuilder.ITicketBuilder
    {
        private string _identity;
        private Reservation _reservation;
        public void BuildIdentity()
        {
            _identity = _reservation.Name + " " + _reservation.Surname;
        }

        public void BuildReservation(Reservation reserv)
        {
            _reservation = reserv;
        }

        public Cinema.Models.ITicket BuildTicket()
        {
            try
            {
                return new Cinema.TicketBuilder.Ticket(_reservation, _identity);
            }
            catch
            {

                return null;
            }
        }
    }
}
