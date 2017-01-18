using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public abstract class ITicket
    {
        private string _identity;
        private Reservation _reservation;
        private double price;
        private string seats;
        private Cinema.Models.Show show;

        public ITicket getTicket(){ return this; }
        public void setPrice(double price) { this.price = price; }
        public void setSeats(string seats) { this.seats = seats; }
        public void setShow(Cinema.Models.Show show) { this.show = show; }
        public double getPrice() { return this.price; }
        public string getSeats() { return this.seats; }
        public Cinema.Models.Show getShow() { return this.show; }
        public string Identity { get; set; }
        public Reservation Reservation { get; set; }
    }
}
