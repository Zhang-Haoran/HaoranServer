namespace HaoranServer.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth {get; set; }
        public string? Role { get; set; }
        public string Password { get; set; }

        public ICollection<Review> Review { get; set; }
    }
}
