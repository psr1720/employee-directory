using RequestDTO = EmployeeDirectory.Models.RequestDTO;
using ResponseDTO = EmployeeDirectory.Models.ResponseDTO;
using Entity = EmployeeDirectory.Models.Entity;
using EmployeeDirectory.Repository.Contracts;
using EmployeeDirectory.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using EmployeeDirectory.Models;

namespace EmployeeDirectory.Services.Concerns;

public class RoleService: IRoleService
{
    private IRoleRepository _roleRepository; 
    private RequestContext _requestContext;
    private IMapper _mapper;

    public RoleService(IRoleRepository roleRepository,IMapper mapper, RequestContext requestContext)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
        _requestContext = requestContext;
    }

    public Pagination<List<ResponseDTO.Role>> GetAll(int page, int limit, Filters? filters)
    {
        IQueryable<Entity.RoleDetails> query = _roleRepository.BuildQuery(filters: filters);

        Pagination<List<Entity.RoleDetails>> result = _roleRepository.GetAll(page, limit, query);
        List<ResponseDTO.Role> roleDTOs = _mapper.Map<List<ResponseDTO.Role>>(result.Data);

        Pagination<List<ResponseDTO.Role>> pagination = new Pagination<List<ResponseDTO.Role>>
        {
            Data = roleDTOs,
            TotalRecords = result.TotalRecords
        };
        return pagination;
    }

    public ResponseDTO.Role Get(int id)
    {
        IQueryable<Entity.RoleDetails> query = _roleRepository.BuildQuery(Id: id);

        Entity.RoleDetails roleDetails = _roleRepository.Get(query);
        ResponseDTO.Role role = _mapper.Map<ResponseDTO.Role>(roleDetails);
        return role;
    }
    
    public RequestDTO.Role Insert(RequestDTO.Role roleDto)
    {
        string creatorId = _requestContext.Id;

        Entity.Role newRole = _mapper.Map<Entity.Role>(roleDto);

        newRole.SetCreatedFields(creatorId);

        Entity.Role insertedRole = _roleRepository.Insert(newRole);

        IdentityRole identityRole = new IdentityRole
        {
            Id = insertedRole.Id.ToString(),
            Name = insertedRole.Name,
        };

        RequestDTO.Role role = _mapper.Map<RequestDTO.Role>(insertedRole);
        return role;
    }

    public RequestDTO.Role Update(RequestDTO.Role roleDto)
    {
        string updatorId = _requestContext.Id;

        Entity.Role newRole = _mapper.Map<Entity.Role>(roleDto);

        newRole.SetUpdatedFields(updatorId);

        Entity.Role updatedEmployee = _roleRepository.Update(newRole);
        RequestDTO.Role role = _mapper.Map<RequestDTO.Role>(updatedEmployee);
        return role;
    }

    public bool Delete(int id) 
    {
        Expression<Func<Entity.Role, bool>> expression = x => x.Id == id;
        return _roleRepository.Delete(expression);
    }
}


