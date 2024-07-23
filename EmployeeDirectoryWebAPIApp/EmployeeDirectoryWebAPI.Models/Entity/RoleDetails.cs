namespace EmployeeDirectory.Models.Entity;

public class RoleDetails
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public string? Description { get; set; } = null;
    public string Location { get; set; }
}
