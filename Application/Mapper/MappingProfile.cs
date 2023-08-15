using Application.Mapper.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateTime.ParseExact(src.DateOfBirth, "dd.MM.yyyy", null)));
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("dd.MM.yyyy")));

            CreateMap<CarDTO, Car>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Convert.FromBase64String(src.Image)));
            CreateMap<Car, CarDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Convert.ToBase64String(src.Image)));

            CreateMap<RentalDTO, Rental>();
            CreateMap<Rental, RentalDTO>();

            CreateMap<RegisterDTO, UserCredentials>();
            CreateMap<RegisterDTO, User>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateTime.ParseExact(src.DateOfBirth, "dd.MM.yyyy", null)));

            CreateMap<UserCredentialsDTO, UserCredentials>();
            CreateMap<UserCredentials, UserCredentialsDTO>();
        }
    }
}
