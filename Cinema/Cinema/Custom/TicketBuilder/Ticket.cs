using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.TicketBuilder
{
    class Ticket : Cinema.Models.ITicket
    {
        private Ticket() {}
        public Ticket(Cinema.Models.Reservation reservation)
        {
            this.setShow(reservation.Show);
            this.setSeats(reservation.Seats);
            // this.setPrice(29.99);
        }
    }
}
