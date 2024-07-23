using EmployeeDirectory.Models;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EmployeeDirectory.Services;

public class RequestContextBuilder
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RequestContextBuilder(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public RequestContext Build()
    {
        string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
        if (token == null)
        {
            return new RequestContext();
        }
        token = token.Substring(6).Trim();
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        string id = jwtToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        string name  = jwtToken.Claims.First(c => c.Type == ClaimTypes.Name).Value;
        string role = jwtToken.Claims.First(c => c.Type == ClaimTypes.Role).Value;
        RequestContext requestContext = new RequestContext
        {
            Id = id,
            Name = name,
            Role = role,
        };
        return requestContext;
    }
}
