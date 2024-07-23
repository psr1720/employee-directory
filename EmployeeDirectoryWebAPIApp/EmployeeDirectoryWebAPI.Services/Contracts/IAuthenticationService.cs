using EmployeeDirectory.Models.Entity;
using Microsoft.AspNetCore.Identity;

namespace EmployeeDirectory.Services.Contracts;

public interface IAuthenticationService
{
    public string GenerateToken(Employee employee);

}
