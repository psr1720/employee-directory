using EmployeeDirectory.Models;
using EmployeeDirectory.Models.Entity;
using EmployeeDirectory.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using Entity = EmployeeDirectory.Models.Entity;


namespace EmployeeDirectory.Repository.Concerns;

public class RoleRepository : BaseRepository<Entity.Role, Entity.RoleDetails>, IRoleRepository
{
    private EmployeeDbContext _dbContext;

    public RoleRepository(EmployeeDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Role GetById(int id)
    {
        var employee = _dbContext.Roles.FirstOrDefault(r => r.Id == id);
        if (employee == null)
        {
            throw new Exception("Role not Found");
        }
        return employee;
    }

    public IQueryable<Entity.RoleDetails> BuildQuery(int? id = null, Filters? filters = null)
    {
        IQueryable<Role> query = _dbContext.Roles;
        if (filters != null)
        {
            if (filters.Departments != null && filters.Departments.Count > 0)
            {
                query = query.Where(r => filters.Departments.Contains(r.DepartmentId));
            }
            if (filters.Locations != null && filters.Locations.Count > 0)
            {
                query = query.Where(e => filters.Locations.Contains(e.LocationId));
            }
        }
        var result = query
                        .Include(r => r.Department)
                        .Select(r => new Entity.RoleDetails
                        {
                            Id = r.Id,
                            Name = r.Name,
                            Department = r.Department.Name,
                            Description = r.Description,
                            Location = r.Location.Name,
                        });
        if (id != null)
        {
            result = result.Where(e => e.Id == id);
        }
        return result;
    }

}
