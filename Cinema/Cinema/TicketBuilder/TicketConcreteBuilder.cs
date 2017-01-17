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
        public Cinema.Models.ITicket BuildTicket(Reservation reservation)
        {
            try
            {
                return new Cinema.TicketBuilder.Ticket(reservation);
            }
            catch
            {

                return null;
            }
        }
    }
}
