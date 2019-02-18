using employee.Models;
using Microsoft.EntityFrameworkCore;

namespace employee.Data
{
    public class EmployeeDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set;}

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>( entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.firstName);
                entity.Property(e => e.lastName);
                entity.Property(e => e.email);

            });
        }
    }
}