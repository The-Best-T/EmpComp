using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EmpComp.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                    new Employee {Id=Guid.NewGuid(),Name = "Tom",SurName="Averen",Patronymic="Hugon",Age=28},
                    new Employee {Id=Guid.NewGuid(),Name = "Alice",SurName="Marol",Patronymic="Markes", Age = 26 },
                    new Employee {Id=Guid.NewGuid(),Name = "Sam",SurName="Rafian",Patronymic="Jameson",Age = 28 }
            );
        }
    }
}
