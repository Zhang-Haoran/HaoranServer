namespace HaoranServer.Dto.ReviewDto
{
    public class ReviewPostDto
    {
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public int UserId { get; set; }
    }
}
