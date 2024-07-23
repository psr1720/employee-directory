namespace EmployeeDirectory.Models;

public class ApiResponse<T>
{
    public bool IsSuccess { get;  set; }
    public int StatusCode { get;  set; }
    public T? Data { get;  set; }
    public string? ErrorMessage { get;  set; }

    public ApiResponse(bool isSuccess, int statusCode, T? data, string? errorMessage = null)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Data = data;
        ErrorMessage = errorMessage;
    }

    public ApiResponse()
    {

    }
}
