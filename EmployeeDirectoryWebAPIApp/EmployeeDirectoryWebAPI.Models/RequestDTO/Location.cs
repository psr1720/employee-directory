using System.ComponentModel.DataAnnotations;

namespace EmployeeDirectory.Models.RequestDTO;

public class Location
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
}
