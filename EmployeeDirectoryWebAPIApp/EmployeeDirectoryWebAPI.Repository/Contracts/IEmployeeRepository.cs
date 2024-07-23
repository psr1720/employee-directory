using EmployeeDirectory.Models;
using EmployeeDirectory.Models.Entity;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using Entity = EmployeeDirectory.Models.Entity;


namespace EmployeeDirectory.Repository.Contracts;

public interface IEmployeeRepository
{
    public Entity.Employee GetEmployee(string employeeId);
    public Pagination<List<Entity.EmployeeDetails>> GetAll(int page, int limit, IQueryable<Entity.EmployeeDetails> query);
    public Entity.EmployeeDetails Get(IQueryable<Entity.EmployeeDetails> query);
    public Entity.Employee Insert(Entity.Employee employee);
    public Entity.Employee Update(Entity.Employee employee, List<string>? excludeColumns = null);
    public Entity.Employee UpdateEmployeeJobTitle(int id, int jobTitleId);
    public bool Delete(Expression<Func<Entity.Employee, bool>> expression);
    public IQueryable<Entity.EmployeeDetails> BuildQuery(string? Id = null, Filters? filters = null, int? roleId = null);

}
