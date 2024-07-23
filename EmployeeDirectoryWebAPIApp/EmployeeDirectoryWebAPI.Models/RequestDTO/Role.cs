using System.ComponentModel.DataAnnotations;

namespace EmployeeDirectory.Models.RequestDTO;

public class Role
{
    public int Id { get; set; }

    [Required(ErrorMessage = "This is a Required Field")]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required(ErrorMessage = "This is a Required Field")]
    public int? DepartmentId { get; set; }

    public int? LocationId { get; set; }

    [MaxLength(200)]
    public string? Description { get; set; }
}
