namespace Cinema.Models
{
    public enum Ticket
    {
        Standard,
        HalfPrices
    };

    public class Reservation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Seats { get; set; }
        public Ticket TicketType { get; set; }
        public bool WasPaid { get; set; }
        public int ShowId { get; set; }
        public virtual Show Show { get; set; }
    }
}
