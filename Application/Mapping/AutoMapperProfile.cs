using Application.Dtos.Complaint;
using Application.Dtos.User;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserLoginDto>();
            CreateMap<UserRegistrationDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<Complaint, ComplaintDto>();
            CreateMap<ComplaintDto, Complaint>();
            CreateMap<Complaint, UpdateComplaintDto>();
            CreateMap<UpdateComplaintDto, Complaint>();
        }
    }
}
