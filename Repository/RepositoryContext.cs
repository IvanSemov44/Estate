using Microsoft.EntityFrameworkCore;
using Repository.Configuration;
using System.Drawing;

namespace Entities.Models
{
    public class RepositoryContext : DbContext
    {

        public RepositoryContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }

        public DbSet<Company>? Companies { get; set; }

        public DbSet<Employee>? Employees { get; set; }
    }
}
