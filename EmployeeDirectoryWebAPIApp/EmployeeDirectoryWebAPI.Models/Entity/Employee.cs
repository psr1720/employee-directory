using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeDirectory.Models.Entity;


public class Employee : Audit
{
    [Key]
    public int Id { get; set; }

    [Required]
    [RegularExpression(@"^TZ\d{4}$", ErrorMessage = "Invalid Input!!: enter the Valid Emp ID format TZxxxx")]
    [MaxLength(10)]
    [Column(TypeName = "varchar(10)")]
    public string EmployeeId { get; set; }

    [Required]
    [MaxLength(50)]
    [Column(TypeName = "varchar(50)")]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    [Column(TypeName = "varchar(50)")]
    public string LastName { get; set; }

    [Column(TypeName = "date")]
    public DateTime? DOB { get; set; } = null;

    [Required]
    [MaxLength(50)]
    [Column(TypeName = "varchar(50)")]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
    public string EmailID { get; set; }

    [MaxLength(10)]
    [Column(TypeName = "varchar(10)")]
    [RegularExpression(@"^[6789]\d{9}$")]
    public string? PhoneNo { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime JoinDate { get; set; }

    public int LocationId { get; set; }

    public int JobTitleId { get; set; }

    public int? ManagerId { get; set; }

    public int? ProjectId { get; set; }

    public Location Location { get; set; }

    public Role JobTitle { get; set; }

    public Employee Manager { get; set; }

    public Project Project { get; set; }

    public string? ProfilePicture { get; set; }

}
