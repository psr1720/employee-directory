using EmployeeDirectory.Models;
using EmployeeDirectory.Models.Entity;
using EmployeeDirectory.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Entity = EmployeeDirectory.Models.Entity;

namespace EmployeeDirectory.Repository.Concerns;

public class EmployeeRepository : BaseRepository<Entity.Employee, Entity.EmployeeDetails>, IEmployeeRepository
{
    private EmployeeDbContext _dbContext;

    public EmployeeRepository(EmployeeDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Entity.Employee GetEmployee(string employeeId)
    {
        var employee = _dbContext.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
        if (employee == null)
        {
            throw new Exception("Employee not Found");
        }
        return employee;
    }

    public Entity.Employee UpdateEmployeeJobTitle(int id, int jobTitleId)
    {
        var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == id);
        if (employee != null)
        {
            employee.JobTitleId = jobTitleId;
            _dbContext.SaveChanges();

            var updatedemp = _dbContext.Employees.FirstOrDefault(e => e.Id == id);
            if (updatedemp != null)
            {
                return updatedemp;
            }
            throw new Exception("Employee not updated");
        }
        else
        {
            throw new Exception("Employee not found");
        }
    }

    public IQueryable<Entity.EmployeeDetails> BuildQuery(string? employeeId = null, Filters? filters = null, int? roleId = null)
    {
        IQueryable<Employee> query = _dbContext.Employees;
        if (filters != null)
        {
            if (filters.Departments != null && filters.Departments.Count > 0)
            {
                query = query.Where(e => filters.Departments.Contains((int)e.JobTitle.DepartmentId));
            }
            if (filters.Locations != null && filters.Locations.Count > 0)
            {
                query = query.Where(e => filters.Locations.Contains(e.LocationId));
            }
        }
        if (roleId != null)
        {
            query = query.Where(e => e.JobTitleId == roleId);
        }

        var result = query
            .Include(e => e.JobTitle)
                .ThenInclude(r => r.Department)
            .Include(e => e.Project)
            .Include(e => e.Location)
            .Include(e => e.Manager)
            .Select(e => new Entity.EmployeeDetails
            {
                Id = e.Id,
                EmployeeId = e.EmployeeId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                DOB = e.DOB,
                EmailID = e.EmailID,
                PhoneNo = e.PhoneNo,
                JoinDate = e.JoinDate,
                Location = e.Location.Name,
                JobTitle = e.JobTitle.Name,
                Department = e.JobTitle.Department.Name,
                Manager = e.Manager.FirstName + " " + e.Manager.LastName,
                Project = e.Project.Name,
                ProfilePicture = e.ProfilePicture

            });

        if (employeeId != null)
        {
            result = result.Where(e => e.EmployeeId == employeeId);
        }

        return result;
    }
}
