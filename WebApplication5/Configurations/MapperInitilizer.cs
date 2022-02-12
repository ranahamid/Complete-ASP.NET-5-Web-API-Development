using AutoMapper;
using System;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Configurations
{
    public class MapperInitilizer:Profile 
    {
        public MapperInitilizer()
        {
            CreateMap<Country,CountryDTO>().ReverseMap();
            CreateMap<Country, CreateCountryDTO>().ReverseMap();

            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, CreateHotelDTO>().ReverseMap();

            CreateMap<ApiUser, UserDTO>().ReverseMap();
        }
    }
}
