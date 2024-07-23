using EmployeeDirectory.Repository.Contracts;
using Entity = EmployeeDirectory.Models.Entity;

namespace EmployeeDirectory.Repository.Concerns;

public class ProjectRepository : BaseRepository<Entity.Project, Entity.ProjectDetails>,IProjectRepository
{
    private EmployeeDbContext _dbContext;

    public ProjectRepository(EmployeeDbContext dbContext): base(dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Entity.ProjectDetails> BuildQuery(int? id = null)
    {
        var query = _dbContext.Projects
                  .Select(p => new Entity.ProjectDetails
                  {
                      Id = p.Id,
                      Name = p.Name,
                  });
        if (id != null)
        {
            query = query.Where(p => p.Id == id);
        }
        return query;
    }
}
