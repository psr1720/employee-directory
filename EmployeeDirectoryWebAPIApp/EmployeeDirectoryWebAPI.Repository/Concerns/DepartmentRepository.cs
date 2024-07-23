using EmployeeDirectory.Repository.Contracts;
using Entity = EmployeeDirectory.Models.Entity;

namespace EmployeeDirectory.Repository.Concerns;

public class DepartmentRepository : BaseRepository<Entity.Department, Entity.DepartmentDetails>, IDepartmentRepository
{
    private EmployeeDbContext _dbContext;

    public DepartmentRepository(EmployeeDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Entity.DepartmentDetails> BuildQuery(int? id = null)
    {
        var query = _dbContext.Departments
                              .Select(d => new Entity.DepartmentDetails
                              {
                                  Id = d.Id,
                                  Name = d.Name,
                              });
        if (id != null)
        {
            query = query.Where(d => d.Id == id);
        }
        return query;
    }

}
