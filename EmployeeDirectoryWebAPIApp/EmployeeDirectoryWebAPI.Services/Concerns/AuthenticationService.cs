using EmployeeDirectory.Models.Entity;
using EmployeeDirectory.Repository.Contracts;
using EmployeeDirectory.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeDirectory.Services.Concerns;

public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;
    private readonly IRoleRepository _roleRepository;
    public AuthenticationService(IConfiguration configuration, IRoleRepository roleRepository)
    {
        _configuration = configuration;
        _roleRepository = roleRepository;
    }

    public string GenerateToken(Employee employee)
    {
        string roleName = _roleRepository.GetById(employee.JobTitleId).Name;
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier , employee.EmployeeId),
            new Claim(ClaimTypes.Name ,  employee.LastName),
            new Claim(ClaimTypes.Role, roleName)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration["Jwt:Key"]));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            
            signingCredentials: creds);

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        return jwtToken;
    }
}
