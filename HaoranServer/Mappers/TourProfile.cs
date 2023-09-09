using AutoMapper;
using HaoranServer.Dto.TourDto;
using HaoranServer.Models;

namespace HaoranServer.Mappers
{
    public class TourProfile: Profile
    {
        public TourProfile() {
            CreateMap<TourPostDto, Tour>();
            CreateMap<TourPutDto, Tour>();
        }
    }
}
