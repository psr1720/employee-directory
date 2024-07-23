using EmployeeDirectory.Repository.Contracts;
using Entity = EmployeeDirectory.Models.Entity;

namespace EmployeeDirectory.Repository.Concerns;

public class LocationRepository : BaseRepository<Entity.Location, Entity.LocationDetails>, ILocationRepository
{
    private readonly EmployeeDbContext _dbContext;

    public LocationRepository(EmployeeDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Entity.LocationDetails> BuildQuery(int? id = null)
    {
        var query = _dbContext.Locations
                              .Select(d => new Entity.LocationDetails
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
