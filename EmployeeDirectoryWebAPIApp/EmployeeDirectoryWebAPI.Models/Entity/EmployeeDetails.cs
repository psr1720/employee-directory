namespace EmployeeDirectory.Models.Entity;

public class EmployeeDetails
{
    public int Id { get; set; }

    public string EmployeeId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime? DOB { get; set; } = null;

    public string EmailID { get; set; }

    public string? PhoneNo { get; set; } = null;

    public DateTime JoinDate { get; set; }

    public string Location { get; set; }

    public string JobTitle { get; set; }

    public string Department { get; set; }

    public string? Manager { get; set; } = null;

    public string? Project { get; set; } = null;

    public string? ProfilePicture { get; set; } = null;
}
