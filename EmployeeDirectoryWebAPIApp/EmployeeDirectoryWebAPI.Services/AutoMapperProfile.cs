using AutoMapper;
using RequestDTO = EmployeeDirectory.Models.RequestDTO;
using ResponseDTO = EmployeeDirectory.Models.ResponseDTO;
using Entity = EmployeeDirectory.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectory.Services;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<RequestDTO.Employee, Entity.Employee>();
            //.ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
        CreateMap<RequestDTO.Role, Entity.Role>();
        CreateMap<RequestDTO.Department, Entity.Department>();
        CreateMap<RequestDTO.Project, Entity.Project>();
        CreateMap<RequestDTO.Location, Entity.Location>();
        

        CreateMap<Entity.EmployeeDetails, ResponseDTO.Employee>();
        CreateMap<Entity.RoleDetails, ResponseDTO.Role>();
        CreateMap<Entity.DepartmentDetails, ResponseDTO.Department>();
        CreateMap<Entity.ProjectDetails, ResponseDTO.Project>();
        CreateMap<Entity.LocationDetails, ResponseDTO.Location>();


        CreateMap<Entity.Employee, RequestDTO.Employee>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        CreateMap<Entity.Role, RequestDTO.Role>();
        CreateMap<Entity.Department, RequestDTO.Department>();
        CreateMap<Entity.Project, RequestDTO.Project>();
        CreateMap<Entity.Location, RequestDTO.Location>();
    }
}
