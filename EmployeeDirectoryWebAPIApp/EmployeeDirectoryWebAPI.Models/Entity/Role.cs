using System.ComponentModel.DataAnnotations;

namespace EmployeeDirectory.Models.Entity;

public class Role : Audit
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "This is a Required Field")]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required(ErrorMessage = "This is a Required Field")]
    public int DepartmentId { get; set; }

    public int LocationId { get; set; }

    [MaxLength(200)]
    public string? Description { get; set; }

    public Department? Department { get; set; }

    public Location? Location { get; set; }

}
