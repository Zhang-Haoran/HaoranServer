using AutoMapper;
using HaoranServer.Dto.BookingDto;
using HaoranServer.Models;

namespace HaoranServer.Mappers
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<BookingPostDto, Booking>();
            CreateMap<BookingPutDto, Booking>();
        }
    }
}
