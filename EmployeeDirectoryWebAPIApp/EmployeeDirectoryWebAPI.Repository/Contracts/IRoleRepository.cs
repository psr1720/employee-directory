using EmployeeDirectory.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using Entity = EmployeeDirectory.Models.Entity;
using ResponseDTO = EmployeeDirectory.Models.ResponseDTO;

namespace EmployeeDirectory.Repository.Contracts;

public interface IRoleRepository
{
    public Pagination<List<Entity.RoleDetails>> GetAll(int page, int limit,IQueryable<Entity.RoleDetails> query);
    public Entity.RoleDetails Get(IQueryable<Entity.RoleDetails> query);
    public Entity.Role GetById(int id);
    public Entity.Role Insert(Entity.Role role);
    public Entity.Role Update(Entity.Role role, List<string>? excludeColumns = null);
    public bool Delete(Expression<Func<Entity.Role,bool>> expression);
    public IQueryable<Entity.RoleDetails> BuildQuery(int? Id = null, Filters? filters = null);

}
