namespace HaoranServer.Dto.BookingDto
{
    public class BookingPostDto
    {
        public int BookingId { get; set; }
        public int price { get; set; }
        public bool paid { get; set; } = false;
        public int userId { get; set; }
        public int tourId { get; set; }
    }
}
