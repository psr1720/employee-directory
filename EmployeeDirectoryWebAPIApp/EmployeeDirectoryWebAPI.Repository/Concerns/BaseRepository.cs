using EmployeeDirectory.Models;
using EmployeeDirectory.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeDirectory.Repository.Concerns;

public class BaseRepository<T, TDetails> : IBaseRepository<T, TDetails> where T : class
{
    private readonly EmployeeDbContext _dbContext;

    public BaseRepository(EmployeeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Pagination<List<TDetails>> GetAll(int page, int limit, IQueryable<TDetails> query)
    {
        int totalRecords = query.Count();
        List<TDetails> details = new List<TDetails>();
        if(totalRecords > 0)
        {
            details = query
                .Skip((page-1) * limit)
                .Take(limit)
                .ToList();
        }
        Pagination<List<TDetails>> result = new Pagination<List<TDetails>>
        {
            Data = details,
            TotalRecords = totalRecords
        };
        return result;
    }

    public TDetails Get(IQueryable<TDetails> query)
    {
        List<TDetails> details = query.ToList();
        if(details.Count > 0)
        {
            return details[0];
        }
        throw new Exception("Record not found");
    }

    public T Insert(T type)
    {
        var insertedType = _dbContext.Set<T>().Add(type);
        _dbContext.SaveChanges();
        return insertedType.Entity;
    }

    public T Update(T type, List<string>? excludeColumns)
    {
        var entry = _dbContext.Entry(type);
        entry.State = EntityState.Modified;
        _dbContext.Entry(type).Property("CreatedOn").IsModified = false;
        _dbContext.Entry(type).Property("CreatedBy").IsModified = false;
        if (excludeColumns != null)
        {
            foreach (var column in excludeColumns)
            {
                _dbContext.Entry(type).Property(column).IsModified = false;

            }
        }
        _dbContext.SaveChanges();
        _dbContext.Entry(type).Reload();
        return entry.Entity;
    }

    public bool Delete(Expression<Func<T,bool>> expression)
    {
        int executed = _dbContext.Set<T>().Where(expression).ExecuteDelete();
        if (executed > 0)
        {
            return true;
        }
        return false;
    }
}
