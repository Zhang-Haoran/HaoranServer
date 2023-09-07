using AutoMapper;
using HaoranServer.Dto.ReviewDto;
using HaoranServer.Models;

namespace HaoranServer.Mappers
{
    public class ReviewProfile: Profile
    {
        public ReviewProfile() {
            CreateMap<ReviewPostDto, Review>();
            CreateMap<ReviewPutDto, Review>();
        }
    }
}
