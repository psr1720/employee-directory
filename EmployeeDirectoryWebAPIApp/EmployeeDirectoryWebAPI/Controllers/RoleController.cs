using RequestDTO = EmployeeDirectory.Models.RequestDTO;
using ResponseDTO = EmployeeDirectory.Models.ResponseDTO;
using EmployeeDirectory.Models;
using EmployeeDirectory.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EmployeeDirectory.UI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly IEmployeeService _employeeService;

    public RoleController(IRoleService roleService, IEmployeeService employeeService)
    {
        _roleService = roleService;
        _employeeService = employeeService;
    }

    [HttpPost("GetAll")]
    public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10, [FromBody] Filters? filters = null)
    {
        var roles = _roleService.GetAll(page, limit, filters);
        roles.Data.ForEach(r =>
        {
            r.ImgArray = _employeeService.GetEmployeeByRole(r.Id).Data;
        });

        ApiResponse<Pagination<List<ResponseDTO.Role>>> response = new ApiResponse<Pagination<List<ResponseDTO.Role>>>(
            isSuccess: true,
            statusCode: (int)HttpStatusCode.OK,
            data: roles
            );

        return Ok(response);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var role = _roleService.Get(id);
        role.ImgArray = _employeeService.GetEmployeeByRole(role.Id).Data;

        ApiResponse<ResponseDTO.Role> response = new ApiResponse<ResponseDTO.Role>(
            isSuccess: true,
            statusCode: (int)HttpStatusCode.OK,
            data: role
            );

        return Ok(response);
    }

    [HttpPost]
    public IActionResult Insert(RequestDTO.Role role)
    {
        var insertedRole = _roleService.Insert(role);

        ApiResponse<RequestDTO.Role> response = new ApiResponse<RequestDTO.Role>(
            isSuccess: true,
            statusCode: (int)HttpStatusCode.OK,
            data: insertedRole
            );

        return Ok(response);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id,RequestDTO.Role role) 
    {
        var updatedRole = _roleService.Update(role);

        ApiResponse<RequestDTO.Role> response = new ApiResponse<RequestDTO.Role>(
            isSuccess: true,
            statusCode: (int)HttpStatusCode.OK,
            data: updatedRole
            );

        return Ok(response);
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        bool isDeleted = _roleService.Delete(id);

        ApiResponse<bool> response = new ApiResponse<bool>(
            isSuccess: isDeleted,
            statusCode: (int)HttpStatusCode.OK,
            data: isDeleted
            );

        return Ok(response);
    }
}
