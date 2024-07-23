using Entity = EmployeeDirectory.Models.Entity;
using EmployeeDirectory.Models;
using System.Linq.Expressions;

namespace EmployeeDirectory.Repository.Contracts;

public interface IDepartmentRepository
{
    public Pagination<List<Entity.DepartmentDetails>> GetAll(int page, int limit, IQueryable<Entity.DepartmentDetails> query);
    public Entity.Department Insert(Entity.Department department); 
    public Entity.Department Update(Entity.Department department, List<string>? excludeColumns = null);
    public bool Delete(Expression<Func<Entity.Department, bool>> expression);
    public IQueryable<Entity.DepartmentDetails> BuildQuery(int? Id = null);

}
