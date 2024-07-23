using Entity = EmployeeDirectory.Models.Entity;
using EmployeeDirectory.Models;
using System.Linq.Expressions;

namespace EmployeeDirectory.Repository.Contracts;

public interface ILocationRepository
{
    public Pagination<List<Entity.LocationDetails>> GetAll(int page, int limit, IQueryable<Entity.LocationDetails> query);
    public Entity.Location Insert(Entity.Location location);
    public Entity.Location Update(Entity.Location location, List<string>? excludeColumns = null);
    public bool Delete(Expression<Func<Entity.Location,bool>> expression);
    public IQueryable<Entity.LocationDetails> BuildQuery(int? Id = null);

}
