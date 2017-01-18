using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.TicketBuilder
{
    class ETicket : Cinema.Models.ITicket
    {
        private ETicket() {}
        public ETicket(Cinema.Models.Reservation reservation, string identity)
        {
            this.setShow(reservation.Show);
            this.setSeats(reservation.Seats);
            this.Identity = identity;
            this.Reservation = reservation;
            // this.setPrice(19.99);
        }
    }
}
