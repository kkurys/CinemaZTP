using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.TicketBuilder
{
    class ETicketConcreteBuilder : Cinema.TicketBuilder.ITicketBuilder
    {
        private string _identity;
        private Reservation _reservation;
        public void BuildIdentity()
        {
            Random rnd = new Random();

            _identity = _reservation.Id.ToString();
            for (int i = 0; i < 10; i++)
            {
                _identity += rnd.Next(0, 10);
            }
        }

        public void BuildReservation(Reservation reserv)
        {
            _reservation = reserv;
        }

        public Cinema.Models.ITicket BuildTicket()
        {
            try
            {
                return new Cinema.TicketBuilder.ETicket(_reservation, _identity);
            }
            catch
            {
                return null;
            }
        }
    }
}
