using System.ComponentModel.DataAnnotations;

namespace EmployeeDirectory.Models.RequestDTO;

public class ChangePassword
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string OldPassword { get; set; }

    [Required]
    public string NewPassword { get; set; }

}
