﻿namespace HaoranServer.Dto.BookingDto
{
    public class BookingPostDto
    {
        public int price { get; set; }
        public bool paid { get; set; }
        public int userId { get; set; }
        public int tourId { get; set; }
    }
}
