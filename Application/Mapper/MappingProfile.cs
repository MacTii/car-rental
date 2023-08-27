﻿using Application.Mapper.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Helpers;
using Domain.Enums;

namespace Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // UserDTO -> User
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateTime.ParseExact(src.DateOfBirth, "yyyy-MM-dd", null)));

            // User -> UserDTO
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("yyyy-MM-dd")));

            // CarDTO -> Car
            CreateMap<CarDTO, Car>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Base64ImageConverter.RemoveBase64Prefix(src.Image)));

            // Car -> CarDTO
            CreateMap<Car, CarDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Base64ImageConverter.AddBase64Prefix(src.Image)));

            // RentalDTO -> Rental
            CreateMap<RentalDTO, Rental>();

            // Rental -> RentalDTO
            CreateMap<Rental, RentalDTO>();

            // RegisterDTO -> UserCredentials
            CreateMap<RegisterDTO, UserCredentials>();

            // RegisterDTO -> User
            CreateMap<RegisterDTO, User>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateTime.ParseExact(src.DateOfBirth, "yyyy-MM-dd", null)));

            // UserCredentialsDTO -> UserCredentials
            CreateMap<UserCredentialsDTO, UserCredentials>();

            // UserCredentials -> UserCredentialsDTO
            CreateMap<UserCredentials, UserCredentialsDTO>();

            // BlogDTO -> Blog
            CreateMap<BlogDTO, Blog>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Base64ImageConverter.RemoveBase64Prefix(src.Image)));

            // Blog -> BlogDTO
            CreateMap<Blog, BlogDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Base64ImageConverter.AddBase64Prefix(src.Image)));

            // CommentDTO -> Comment
            CreateMap<CommentDTO, Comment>();

            // Comment -> CommentDTO
            CreateMap<Comment, CommentDTO>();
        }
    }
}
