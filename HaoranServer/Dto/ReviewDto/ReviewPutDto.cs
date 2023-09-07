namespace HaoranServer.Dto.ReviewDto
{
    public class ReviewPutDto
    {
        public int ReviewId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
    }
}
