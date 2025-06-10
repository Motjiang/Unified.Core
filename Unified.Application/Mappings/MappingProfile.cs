using AutoMapper;
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
        }
    }
}
