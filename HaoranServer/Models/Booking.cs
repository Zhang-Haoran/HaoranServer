namespace HaoranServer.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int price { get; set; }
        public bool paid { get; set; } = false;
        public int userId { get; set; }
        public int tourId { get; set; }
        public User User { get; set; }
        public Tour Tour { get; set; }

    }
}
