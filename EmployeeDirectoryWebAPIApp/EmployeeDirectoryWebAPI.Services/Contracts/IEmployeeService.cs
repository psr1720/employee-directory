using EmployeeDirectory.Models;
using Entity = EmployeeDirectory.Models.Entity;
using RequestDTO = EmployeeDirectory.Models.RequestDTO;
using ResponseDTO = EmployeeDirectory.Models.ResponseDTO;


namespace EmployeeDirectory.Services.Contracts;

public interface IEmployeeService
{
    public Pagination<List<ResponseDTO.Employee>> GetAll(int page, int limit, Filters? filters);
    public ResponseDTO.Employee Get(string employeeID);
    public Entity.Employee GetEmployee(string employeeID);
    public RequestDTO.Employee Insert(RequestDTO.Employee employee);
    public RequestDTO.Employee Update(RequestDTO.Employee employee);
    public RequestDTO.Employee UpdateEmployeeJobTitleId(int id, int jobTitleId);
    public Pagination<List<ResponseDTO.Employee>> GetEmployeeByRole(int roleId);
    public bool Delete(string employeeID);
}
