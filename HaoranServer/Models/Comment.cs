namespace HaoranServer.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? CreatedTime { get; set; }
        public string? UpdatedTime { get; set; }
    }
}
