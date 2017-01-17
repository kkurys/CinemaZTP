using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.TicketBuilder
{
    interface ITicketBuilder
    {
        Cinema.Models.ITicket BuildTicket(Cinema.Models.Reservation reservation);
    }
}
