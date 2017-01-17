using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.TicketBuilder
{
    class ETicketConcreteBuilder : Cinema.TicketBuilder.ITicketBuilder
    {
        public Cinema.Models.ITicket BuildTicket(Cinema.Models.Reservation reservation)
        {
            try
            {
                return new Cinema.TicketBuilder.ETicket(reservation);
            }
            catch
            {
                return null;
            }
        }
    }
}
