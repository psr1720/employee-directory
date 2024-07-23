using System.Linq.Expressions;
using Entity = EmployeeDirectory.Models.Entity;
using EmployeeDirectory.Models;

namespace EmployeeDirectory.Repository.Contracts;

public interface IProjectRepository
{
    public Pagination<List<Entity.ProjectDetails>> GetAll(int page, int limit, IQueryable<Entity.ProjectDetails> query);
    public Entity.Project Insert(Entity.Project project);
    public Entity.Project Update(Entity.Project project, List<string>? excludeColumns = null);
    public bool Delete(Expression<Func<Entity.Project, bool>> expression);
    public IQueryable<Entity.ProjectDetails> BuildQuery(int? Id = null);

}
