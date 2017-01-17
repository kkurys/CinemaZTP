using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models
{
    class Show
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public List<Reservation> Reservation { get; set; }
        public DateTime? ShowDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Hall { get; set; }
    }
}
