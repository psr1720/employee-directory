using EmployeeDirectory.Models;
using System.Linq.Expressions;
using Entity = EmployeeDirectory.Models.Entity;

namespace EmployeeDirectory.Repository.Contracts;

public interface IBaseRepository<T, TDetails>
{
    public Pagination<List<TDetails>> GetAll(int page, int limit, IQueryable<TDetails> query);
    public TDetails Get(IQueryable<TDetails> query);
    public T Insert(T type);
    public T Update(T type, List<string>? excludeColumns = null);    
    public bool Delete(Expression<Func<T, bool>> expression);

}
