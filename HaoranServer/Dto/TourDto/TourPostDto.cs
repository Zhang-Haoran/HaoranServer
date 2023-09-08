﻿namespace HaoranServer.Dto.TourDto
{
    public class TourPostDto
    {
        public int price { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string? state { get; set; }
        public string? city { get; set; }
        public string? title { get; set; }
        public string? subtitle { get; set; }
        public string? introduction { get; set; }
        public string? highlights { get; set; }
        public string? included { get; set; }
        public string? itinerary { get; set; }
        public string? image { get; set; }
        public string? map { get; set; }
    }
}
