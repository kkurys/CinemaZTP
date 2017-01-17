using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public enum Ticket
    {
        Standard,
        HalfPrice
    };

    class Reservation
    {
        public int Id { get; set; }
        public Show Show { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Seats { get; set; }
        public Ticket TicketType { get; set; }
        public bool WasPaid { get; set; }
    }
}
