using AutoMapper;
using HaoranServer.Dto.UserDto;
using HaoranServer.Models;

namespace HaoranServer.Mappers
{
    public class UserProfile: Profile
    {
        public UserProfile() 
        {
            CreateMap<UserPutDto, User>();
            CreateMap<UserPostDto, User>();
        }
    }
}
