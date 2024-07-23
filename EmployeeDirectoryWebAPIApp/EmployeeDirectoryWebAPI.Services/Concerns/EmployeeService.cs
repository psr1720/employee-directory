using AutoMapper;
using EmployeeDirectory.Models;
using EmployeeDirectory.Models.ResponseDTO;
using EmployeeDirectory.Repository.Contracts;
using EmployeeDirectory.Services.Contracts;
using System.Collections.Generic;
using System.Linq.Expressions;
using Entity = EmployeeDirectory.Models.Entity;
using RequestDTO = EmployeeDirectory.Models.RequestDTO;
using ResponseDTO = EmployeeDirectory.Models.ResponseDTO;

namespace EmployeeDirectory.Services.Concerns;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;
    private readonly RequestContext _requestContext;

    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, RequestContext requestContext)
    {
        _employeeRepository = employeeRepository;
        _requestContext = requestContext;
        _mapper = mapper;
    }

    public Pagination<List<ResponseDTO.Employee>> GetAll(int page, int limit, Filters? filters)
    {
        IQueryable<Entity.EmployeeDetails> query = _employeeRepository.BuildQuery(filters: filters);

        Pagination<List<Entity.EmployeeDetails>> result = _employeeRepository.GetAll(page, limit ,query);
        List<ResponseDTO.Employee> employeeDTOs = _mapper.Map<List<ResponseDTO.Employee>>(result.Data);

        Pagination<List<ResponseDTO.Employee>> pagination = new Pagination<List<ResponseDTO.Employee>>
        {
            Data = employeeDTOs,
            TotalRecords = result.TotalRecords
        };
        
        return pagination;
    }

    public ResponseDTO.Employee Get(string empID)
    {
        IQueryable<Entity.EmployeeDetails> query = _employeeRepository.BuildQuery(Id: empID);
        Entity.EmployeeDetails employee = _employeeRepository.Get(query);
        ResponseDTO.Employee employeeDTO = _mapper.Map<ResponseDTO.Employee>(employee);
        return employeeDTO;
    }

    public Entity.Employee GetEmployee(string  empID)
    {
        return _employeeRepository.GetEmployee(empID);
    }

    public RequestDTO.Employee Insert(RequestDTO.Employee emp)
    {
        string creatorId = _requestContext.Id;

        Entity.Employee newEmp = _mapper.Map<Entity.Employee>(emp);

        newEmp.SetCreatedFields(creatorId);

        Entity.Employee insertedEmployee = _employeeRepository.Insert(newEmp);

        RequestDTO.Employee employee = _mapper.Map<RequestDTO.Employee>(insertedEmployee);

        return employee;
    }

    public RequestDTO.Employee Update(RequestDTO.Employee emp)
    {
        string updatorId = _requestContext.Id;

        Entity.Employee newEmp = _mapper.Map<Entity.Employee>(emp);
        
        newEmp.SetUpdatedFields(updatorId);

        List<string> excludeColumns = ["EmployeeId", "EmailID"];

        Entity.Employee updatedEmployee = _employeeRepository.Update(newEmp, excludeColumns);
        RequestDTO.Employee employee = _mapper.Map<RequestDTO.Employee>(updatedEmployee);
        return employee;
    }

    public RequestDTO.Employee UpdateEmployeeJobTitleId(int id, int jobTitleId)
    {
        var employee = _employeeRepository.UpdateEmployeeJobTitle(id, jobTitleId);
        var employeeDTO = _mapper.Map<RequestDTO.Employee>(employee);
        return employeeDTO;
    }

    public bool Delete(string empID)
    {
        Expression<Func<Entity.Employee, bool>> expression = x=> x.EmployeeId == empID;
        return _employeeRepository.Delete(expression);
    }

    public Pagination<List<ResponseDTO.Employee>> GetEmployeeByRole(int roleId)
    {
        IQueryable<Entity.EmployeeDetails> query = _employeeRepository.BuildQuery(roleId: roleId);

        Pagination<List<Entity.EmployeeDetails>> result = _employeeRepository.GetAll(1, 100, query);
        List<ResponseDTO.Employee> employeeDTOs = _mapper.Map<List<ResponseDTO.Employee>>(result.Data);

        Pagination<List<ResponseDTO.Employee>> pagination = new Pagination<List<ResponseDTO.Employee>>
        {
            Data = employeeDTOs,
            TotalRecords = result.TotalRecords
        };

        return pagination;

    }
}
