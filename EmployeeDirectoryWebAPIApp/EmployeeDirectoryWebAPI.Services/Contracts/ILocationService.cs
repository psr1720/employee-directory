using ResponseDTO = EmployeeDirectory.Models.ResponseDTO;
using RequestDTO = EmployeeDirectory.Models.RequestDTO;
using EmployeeDirectory.Models;

namespace EmployeeDirectory.Services.Contracts;

public interface ILocationService
{
    public Pagination<List<ResponseDTO.Location>> GetAll(int page, int limit);
    public RequestDTO.Location Insert(RequestDTO.Location location);
    public RequestDTO.Location Update(RequestDTO.Location location);
    public bool Delete(int id);
}
