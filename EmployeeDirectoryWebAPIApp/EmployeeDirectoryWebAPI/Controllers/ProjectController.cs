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
 public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10)
    {
        var projects = _projectService.GetAll(page, limit);

        ApiResponse<Pagination<List<ResponseDTO.Project>>> response = new ApiResponse<Pagination<List<ResponseDTO.Project>>>(
            isSuccess: true,
            statusCode: (int)HttpStatusCode.OK,
            data: projects
            );

        return Ok(response);
    }

    [HttpPost]
    public IActionResult Insert(RequestDTO.Project project)
    {
        RequestDTO.Project insertedProject = _projectService.Insert(project);

        ApiResponse<RequestDTO.Project> response = new ApiResponse<RequestDTO.Project>(
            isSuccess: true,
            statusCode: (int)HttpStatusCode.OK,
            data: insertedProject
            );

        return Ok(response);
    }

    [HttpPut]
    public IActionResult Update(RequestDTO.Project project)
    {
        RequestDTO.Project updatedProject = _projectService.Update(project);

        ApiResponse<RequestDTO.Project> response = new ApiResponse<RequestDTO.Project>(
            isSuccess: true,
            statusCode: (int)HttpStatusCode.OK,
            data: updatedProject);

        return Ok(response);
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        bool isDeleted = _projectService.Delete(id);

        ApiResponse<bool> response = new ApiResponse<bool>(
            isSuccess: isDeleted,
            statusCode: (int)HttpStatusCode.OK,
            data: isDeleted
            );

        return Ok(response);
    }
}
