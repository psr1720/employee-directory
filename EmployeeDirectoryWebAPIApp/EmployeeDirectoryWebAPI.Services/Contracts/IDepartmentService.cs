using RequestDTO = EmployeeDirectory.Models.RequestDTO;
using ResponseDTO = EmployeeDirectory.Models.ResponseDTO;
using EmployeeDirectory.Models;

namespace EmployeeDirectory.Services.Contracts;

public interface IDepartmentService
{
    public Pagination<List<ResponseDTO.Department>> GetAll(int page, int limit);
    public RequestDTO.Department Insert(RequestDTO.Department department);
    public RequestDTO.Department Update(RequestDTO.Department department);
    public bool Delete(int id);
}
