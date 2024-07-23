using EmployeeDirectory.Models;
using EmployeeDirectory.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using RequestDTO = EmployeeDirectory.Models.RequestDTO;
using ResponseDTO = EmployeeDirectory.Models.ResponseDTO;

namespace EmployeeDirectory.UI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet]
    public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10)
    {
        var locations = _locationService.GetAll(page, limit);

        ApiResponse<Pagination<List<ResponseDTO.Location>>> response = new ApiResponse<Pagination<List<ResponseDTO.Location>>>(
            isSuccess: true,
            statusCode: (int)HttpStatusCode.OK,
            data: locations
            );

        return Ok(response);
    }

    [HttpPost]
    public IActionResult Insert(RequestDTO.Location location)
    {
        var insertedLocation = _locationService.Insert(location);

        ApiResponse<RequestDTO.Location> response = new ApiResponse<RequestDTO.Location>(
            isSuccess: true,
            statusCode: (int)HttpStatusCode.OK,
            data: insertedLocation
            );

        return Ok(response);
    }

    [HttpPut]
    public IActionResult Update(RequestDTO.Location location)
    {
        var updatedLocation = _locationService.Update(location);

        ApiResponse<RequestDTO.Location> response = new ApiResponse<RequestDTO.Location>(
            isSuccess: true,
            statusCode: (int)HttpStatusCode.OK,
            data: updatedLocation
            );

        return Ok(response);
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var isDeleted = _locationService.Delete(id);

        ApiResponse<bool> response = new ApiResponse<bool>(
            isSuccess: isDeleted,
            statusCode: (int)HttpStatusCode.OK,
            data: isDeleted
            );

        return Ok(response);
    }
}
