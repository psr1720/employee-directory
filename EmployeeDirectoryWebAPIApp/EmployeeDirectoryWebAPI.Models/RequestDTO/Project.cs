using System.ComponentModel.DataAnnotations;

namespace EmployeeDirectory.Models.RequestDTO;

public class Project
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
}
