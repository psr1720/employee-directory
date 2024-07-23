using ResponseDTO = EmployeeDirectory.Models.ResponseDTO;
using RequestDTO = EmployeeDirectory.Models.RequestDTO;
using EmployeeDirectory.Models;

namespace EmployeeDirectory.Services.Contracts;

public interface IProjectService
{
    public Pagination<List<ResponseDTO.Project>> GetAll(int page, int limit);
    public RequestDTO.Project Insert(RequestDTO.Project project);
    public RequestDTO.Project Update(RequestDTO.Project project);
    public bool Delete(int id);
}
