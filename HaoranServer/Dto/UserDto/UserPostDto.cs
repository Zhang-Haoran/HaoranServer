﻿namespace HaoranServer.Dto.UserDto
{
    public class UserPostDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string? Role { get; set; }
        public string Password { get; set; }
    }
}
