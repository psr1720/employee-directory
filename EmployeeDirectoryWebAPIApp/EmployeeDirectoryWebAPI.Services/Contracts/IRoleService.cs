using EmployeeDirectory.Models;
using RequestDTO = EmployeeDirectory.Models.RequestDTO;
using ResponseDTO = EmployeeDirectory.Models.ResponseDTO;

namespace EmployeeDirectory.Services.Contracts;

public interface IRoleService
{
    public Pagination<List<ResponseDTO.Role>> GetAll(int page, int limit, Filters? filters);
    public ResponseDTO.Role Get(int id);
    public RequestDTO.Role Insert(RequestDTO.Role role);
    public RequestDTO.Role Update(RequestDTO.Role role);
    public bool Delete(int id); 


}
