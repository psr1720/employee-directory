using EmployeeDirectory.Models.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.Repository;

public class EmployeeDbContext: IdentityDbContext
{
    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
    {

    }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Location> Locations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Employee>().
            HasIndex(e =>  e.EmployeeId)
            .IsUnique();
        modelBuilder.Entity<Employee>().
            HasIndex(e => e.EmailID)
            .IsUnique();
        modelBuilder.Entity<Employee>().
            HasIndex(e => e.PhoneNo)
            .IsUnique();
    }
}
