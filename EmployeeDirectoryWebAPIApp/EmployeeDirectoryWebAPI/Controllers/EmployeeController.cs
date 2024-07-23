using EmployeeDirectory.Models;
using EmployeeDirectory.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Entity = EmployeeDirectory.Models.Entity;
using RequestDTO = EmployeeDirectory.Models.RequestDTO;
using ResponseDTO = EmployeeDirectory.Models.ResponseDTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EmployeeDirectory.UI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IAuthenticationService _authenticationService;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public EmployeeController(IEmployeeService employeeService, IAuthenticationService authenticationService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _employeeService = employeeService;
        _authenticationService = authenticationService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("Login")]
    [ProducesResponseType<ApiResponse<string>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ApiResponse<string>>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LoginUser(RequestDTO.Login login)
    {
        ApiResponse<string> response = new ApiResponse<string>();
        try
        {
            var existingUser = await _userManager.FindByNameAsync(login.UserId);
            if (existingUser == null)
            {
                throw new Exception("Invalid Login");
            }
            var result = await _signInManager.PasswordSignInAsync(existingUser, login.Password, false, false);
            if (result.Succeeded)
            {
                Entity.Employee employee = _employeeService.GetEmployee(login.UserId);
                string token = _authenticationService.GenerateToken(employee);
                response.IsSuccess = true;
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Data = token;
            }
            else
            {
                throw new Exception("Invaild Login");
            }
            return Ok(response);
        }
        catch
        {
            response.IsSuccess = false;
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            response.ErrorMessage = "Username or Password are Incorrect";

            return BadRequest(response);
        }
    }

    [HttpPost("ChangePassword")]
    [ProducesResponseType<ApiResponse<string>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ApiResponse<string>>(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType<ApiResponse<string>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword(RequestDTO.ChangePassword changePassword)
    {
        ApiResponse<string> response = new ApiResponse<string>();
        var user = await _userManager.FindByNameAsync(changePassword.UserName);
        if (user != null)
        {
            var result = await _userManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.NewPassword);
            if (result.Succeeded)
            {
                response.IsSuccess = true;
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Data = "Password has been successfully changed";
                return Ok(response);
            }
        }
        response.IsSuccess = false;
        response.StatusCode = (int)HttpStatusCode.BadRequest;
        response.ErrorMessage = "Failed to Change Password recheck values entered";


        return Ok(response);

    }

    [HttpPost("GetAll")]
    [ProducesResponseType<ApiResponse<Pagination<List<ResponseDTO.Employee>>>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ApiResponse<string>>(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType<ApiResponse<string>>(StatusCodes.Status404NotFound)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10, [FromBody] Filters? filters = null)
    {
        var employees = _employeeService.GetAll(page, limit, filters);

        ApiResponse<Pagination<List<ResponseDTO.Employee>>> response = new ApiResponse<Pagination<List<ResponseDTO.Employee>>>(
            isSuccess: true,
            statusCode: (int)HttpStatusCode.OK,
            data: employees
            );
        return Ok(response);
    }
    [HttpGet("role/{roleId}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult GetByRoleId(int roleId)
    {
        var employees = _employeeService.GetEmployeeByRole(roleId);

        ApiResponse<Pagination<List<ResponseDTO.Employee>>> response = new ApiResponse<Pagination<List<ResponseDTO.Employee>>>(
            isSuccess: true,
            statusCode: (int)HttpStatusCode.OK,
            data: employees
            );

        return Ok(response);

    }

    [HttpGet("{id}")]
    [ProducesResponseType<ApiResponse<ResponseDTO.Employee>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ApiResponse<string>>(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType<ApiResponse<string>>(StatusCodes.Status404NotFound)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult Get(string id)
    {
        var employee = _employeeService.Get(id);

        ApiResponse<ResponseDTO.Employee> response = new ApiResponse<ResponseDTO.Employee>(
            isSuccess: true,
            statusCode: (int)HttpStatusCode.OK,
            data: employee
            );

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType<ApiResponse<RequestDTO.Employee>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ApiResponse<string>>(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType<ApiResponse<string>>(StatusCodes.Status404NotFound)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Insert(RequestDTO.Employee employee)
    {
        var insertedEmployee = _employeeService.Insert(employee);

        var user = new IdentityUser
        {
            Id = insertedEmployee.Id.ToString(),
            UserName = insertedEmployee.EmployeeId,
            Email = insertedEmployee.EmailID,
            PhoneNumber = insertedEmployee.PhoneNo,
        };

        var result = await _userManager.CreateAsync(user, employee.Password);

        ApiResponse<RequestDTO.Employee> response = new ApiResponse<RequestDTO.Employee>(
            isSuccess: true,
            statusCode: (int)HttpStatusCode.OK,
            data: insertedEmployee
            );

        return Ok(response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType<ApiResponse<RequestDTO.Employee>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ApiResponse<string>>(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType<ApiResponse<string>>(StatusCodes.Status404NotFound)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update(int id, RequestDTO.Employee employee)
    {
        var updatedEmployee = _employeeService.Update(employee);

        var user = await _userManager.FindByNameAsync(updatedEmployee.EmployeeId);
        user.PhoneNumber = updatedEmployee.PhoneNo;
        user.Email = updatedEmployee.EmailID;
        var result = await _userManager.UpdateAsync(user);


        ApiResponse<RequestDTO.Employee> response = new ApiResponse<RequestDTO.Employee>(
            isSuccess: true,
            statusCode: (int)HttpStatusCode.OK,
            data: updatedEmployee
            );

        return Ok(response);
    }

    [HttpPatch("emp/{id}/jobTitle/{jobTitleId}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult UpdateEmployeeJobTitleId(int id, int jobTitleId)
    {
        var employee = _employeeService.UpdateEmployeeJobTitleId(id, jobTitleId);

        ApiResponse<RequestDTO.Employee> response = new ApiResponse<RequestDTO.Employee>(
            isSuccess: true,
            statusCode: (int)HttpStatusCode.OK,
            data: employee
            );

        return Ok(response);
    }

    [HttpDelete]
    [ProducesResponseType<ApiResponse<bool>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ApiResponse<string>>(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType<ApiResponse<string>>(StatusCodes.Status404NotFound)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(string id)
    {
        bool isDeleted = _employeeService.Delete(id);

        ApiResponse<bool> response = new ApiResponse<bool>(
            isSuccess: isDeleted,
            statusCode: (int)HttpStatusCode.OK,
            data: isDeleted
            );

        var user = await _userManager.FindByNameAsync(id);
        var result = await _userManager.DeleteAsync(user);

        return Ok(response);
    }
}
