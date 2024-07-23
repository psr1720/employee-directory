using AutoMapper;
using EmployeeDirectory.Repository.Contracts;
using EmployeeDirectory.Services.Contracts;
using Entity = EmployeeDirectory.Models.Entity;
using RequestDTO = EmployeeDirectory.Models.RequestDTO;
using ResponseDTO = EmployeeDirectory.Models.ResponseDTO;
using EmployeeDirectory.Models;
using System.Linq.Expressions;

namespace EmployeeDirectory.Services.Concerns;

public class DepartmentService: IDepartmentService
{
    private IDepartmentRepository _departmentRepository;
    private readonly RequestContext _requestContext;
    private IMapper _mapper;

    public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper, RequestContext requestContext)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
        _requestContext = requestContext;
    }

    public Pagination<List<ResponseDTO.Department>> GetAll(int page, int limit)
    {
        IQueryable<Entity.DepartmentDetails> query = _departmentRepository.BuildQuery();

        Pagination<List<Entity.DepartmentDetails>> result = _departmentRepository.GetAll(page, limit, query);
        List<ResponseDTO.Department> departmentDTOs = _mapper.Map<List<ResponseDTO.Department>>(result.Data);

        Pagination<List<ResponseDTO.Department>> pagination = new Pagination<List<ResponseDTO.Department>>
        {
            Data = departmentDTOs,
            TotalRecords = result.TotalRecords
        };
        
        return pagination;  
    }

    public RequestDTO.Department Insert(RequestDTO.Department dept)
    {
        string creatorId = _requestContext.Id;

        Entity.Department newDept = _mapper.Map<Entity.Department>(dept);

        newDept.SetCreatedFields(creatorId);

        Entity.Department insertedDepartment = _departmentRepository.Insert(newDept);
        RequestDTO.Department department = _mapper.Map<RequestDTO.Department>(insertedDepartment);
        return department;
    }

    public RequestDTO.Department Update(RequestDTO.Department dept)
    {
        string updatorId = _requestContext.Id;

        Entity.Department newDept = _mapper.Map<Entity.Department>(dept);

        newDept.SetUpdatedFields(updatorId);

        Entity.Department updatedDepartment = _departmentRepository.Update(newDept);
        RequestDTO.Department department = _mapper.Map<RequestDTO.Department>(updatedDepartment);
        return department;
    }

    public bool Delete(int id)
    {
        Expression<Func<Entity.Department, bool>> expression = x => x.Id == id;
        return _departmentRepository.Delete(expression);
    }
}

