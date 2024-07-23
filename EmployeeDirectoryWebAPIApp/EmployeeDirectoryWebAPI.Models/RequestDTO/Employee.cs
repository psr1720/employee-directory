using System.ComponentModel.DataAnnotations;

namespace EmployeeDirectory.Models.RequestDTO;

public class Employee
{
    public int Id { get; set; }

    [Required(ErrorMessage = "This is a Required Field")]
    [RegularExpression(@"^TZ\d{4}$", ErrorMessage = "Invalid Input!!: enter the Valid Emp ID format TZxxxx")]
    [MaxLength(10)]
    public string EmployeeId { get; set; }

    public string? Password { get; set; } = "password";

    [Required(ErrorMessage = "This is a Required Field")]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "This is a Required Field")]
    [MaxLength(50)]
    public string LastName { get; set; }

    public DateTime? DOB { get; set; } = null;

    [Required(ErrorMessage = "This is a Required Field")]
    [MaxLength(50)]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid Input!!: enter valid email format")]
    public string EmailID { get; set; }

    [MaxLength(10)]
    [RegularExpression(@"^[6789]\d{9}$", ErrorMessage = "Invalid Input!!: enter a valid phone number")]
    public string? PhoneNo { get; set; }

    [Required(ErrorMessage = "This is a Required Field")]
    public DateTime JoinDate { get; set; }

    public int LocationId { get; set; }

    public int? JobTitleId { get; set; }

    public int? ManagerId { get; set; }

    public int? ProjectId { get; set; }

    public string? ProfilePicture { get; set; }
}
