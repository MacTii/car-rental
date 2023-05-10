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
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();

            CreateMap<CarDTO, Car>();
            CreateMap<Car, CarDTO>();

            CreateMap<RentalDTO, Rental>();
            CreateMap<Rental, RentalDTO>();

            CreateMap<RegisterDTO, UserCredentials>();
            CreateMap<RegisterDTO, User>();

            CreateMap<UserCredentialsDTO, UserCredentials>();
            CreateMap<UserCredentials, UserCredentialsDTO>();
        }
    }
}
