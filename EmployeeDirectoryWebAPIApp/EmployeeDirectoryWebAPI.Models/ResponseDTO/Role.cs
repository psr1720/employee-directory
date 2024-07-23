using RequestDTO = EmployeeDirectory.Models.RequestDTO;
namespace EmployeeDirectory.Models.ResponseDTO;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public string Location { get; set; }
    public string? Description { get; set; } = null;
    public List<ResponseDTO.Employee> ImgArray { get; set; } = [];

}
