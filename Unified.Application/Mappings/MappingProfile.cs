using AutoMapper;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Application.DTOs.Admin;
using Unified.Application.DTOs.Book;
using Unified.Application.DTOs.Department;
using Unified.Application.DTOs.Designation;
using Unified.Application.DTOs.Leave;
using Unified.Application.DTOs.Ticket;
using Unified.Domain.Entities;

namespace Unified.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, ViewEmployeeDto>()
            .ForMember(dest => dest.IsLocked,
                opt => opt.MapFrom(src => false)) 
            .ForMember(dest => dest.Roles,
                opt => opt.Ignore()); 

            CreateMap<Employee, UpdateEmployeeDto>()
            .ForMember(dest => dest.Roles, opt => opt.Ignore()); 

            CreateMap<UpdateEmployeeDto, Employee>()
           .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName.ToLower()))
           .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName.ToLower()))
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName.ToLower()))
           .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => true));

            // Department Mappings
            CreateMap<Department, DepartmentDto>()
               .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.DepartmentId))
               .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status));

            // CreateDepartment Mappings
            CreateMap<CreateDepartmentDto, Department>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status))
                .ForMember(dest => dest.DepartmentId, opt => opt.Ignore());

            // Destination Mappings
            CreateMap<Designation, DesignationDto>()
                .ForMember(dest => dest.designationId, opt => opt.MapFrom(src => src.DesignationId))
                .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.departmentId, opt => opt.MapFrom(src => src.DepartmentId));

            // CreateDesignation Mappings
            CreateMap<CreateDesignationDto, Designation>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.title))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.departmentId))
                .ForMember(dest => dest.DesignationId, opt => opt.Ignore());

            // BookRequest Mappings
            CreateMap<BookRequest, BookRequestDto>();
            CreateMap<CreateBookRequestDto, BookRequest>()
                .ForMember(dest => dest.BookRequestId, opt => opt.Ignore());

            // Book Mappings
            CreateMap<Book, BookDto>();
            CreateMap<CreateBookDto, Book>()
                .ForMember(dest => dest.BookId, opt => opt.Ignore());

            // Book Category Mappings
            CreateMap<BookCategory, BookCategoryDto>();
            CreateMap<CreateBookCategoryDto, BookCategory>()
                .ForMember(dest => dest.BookCategoryId, opt => opt.Ignore());

            // Ticket Mappings
            CreateMap<Ticket, TicketDto>();
            CreateMap<CreateTicketDto, Ticket>()
                .ForMember(dest => dest.TicketId, opt => opt.Ignore());

            // Ticket Category Mappings
            CreateMap<TicketCategory, TicketCategoryDto>();
            CreateMap<CreateTicketCategoryDto, TicketCategory>()
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore());

            // Ticket Subcategory Mappings
            CreateMap<TicketSubcategory, TicketSubcategoryDto>();
            CreateMap<CreateTicketSubcategoryDto, TicketSubcategory>()
                .ForMember(dest => dest.SubcategoryId, opt => opt.Ignore());

            // Leave Request Mappings
            CreateMap<LeaveRequest, LeaveRequestDto>();
            CreateMap<CreateLeaveRequestDto, LeaveRequest>()
                .ForMember(dest => dest.LeaveRequestId, opt => opt.Ignore());
        }
    }
}
