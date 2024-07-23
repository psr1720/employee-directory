using ResponseDTO = EmployeeDirectory.Models.ResponseDTO;
using RequestDTO = EmployeeDirectory.Models.RequestDTO;
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
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10)
    {
        var departments = _departmentService.GetAll(page, limit);

        ApiResponse<Pagination<List<ResponseDTO.Department>>> response = new ApiResponse<Pagination<List<ResponseDTO.Department>>>(
            isSuccess: true,
            statusCode: (int)HttpStatusCode.OK,
            data: departments
            );

        return Ok(response);
    }

    [HttpPost]
    public IActionResult Insert(RequestDTO.Department department) 
    {
        var insertedDepartment = _departmentService.Insert(department);

        ApiResponse<RequestDTO.Department> response = new ApiResponse<RequestDTO.Department>(
            isSuccess: true,
            statusCode: (int)HttpStatusCode.OK,
            data: insertedDepartment
            );
        
        return Ok(response);
    }

    [HttpPut]
    public IActionResult Update(RequestDTO.Department department)
    {
        var updatedDepartment = _departmentService.Update(department);

        ApiResponse<RequestDTO.Department> response = new ApiResponse<RequestDTO.Department>(
            isSuccess: true,
            statusCode: (int)HttpStatusCode.OK,
            data: updatedDepartment
            );

        return Ok(response);
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        bool isDeleted = _departmentService.Delete(id);

        ApiResponse<bool> response = new ApiResponse<bool>(
            isSuccess: isDeleted,
            statusCode: (int)HttpStatusCode.OK,
            data: isDeleted);

        return Ok(response);
    }
}
