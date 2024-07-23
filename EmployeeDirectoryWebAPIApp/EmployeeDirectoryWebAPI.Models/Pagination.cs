namespace EmployeeDirectory.Models;

public class Pagination<T>
{
    public int TotalRecords { get; set; }
    public T Data { get; set; }
}
