using AutoMapper;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Application.DTOs.Admin;
using Unified.Domain.Entities;

namespace Unified.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, ViewEmployeeDto>()
            .ForMember(dest => dest.IsLocked,
                opt => opt.MapFrom(src => false)) // Default; will override in controller
            .ForMember(dest => dest.Roles,
                opt => opt.Ignore()); // Will be populated manually

            CreateMap<Employee, UpdateEmployeeDto>()
            .ForMember(dest => dest.Roles, opt => opt.Ignore()); // We'll set Roles manually

            CreateMap<UpdateEmployeeDto, Employee>()
           .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName.ToLower()))
           .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName.ToLower()))
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName.ToLower()))
           .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => true));
        }
    }
}
