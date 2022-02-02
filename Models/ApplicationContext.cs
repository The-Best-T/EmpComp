using Microsoft.EntityFrameworkCore;

namespace EmpComp.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().Property(c => c.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            modelBuilder.Entity<Employee>().Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Company>()
            .HasMany(c => c.Employees)
            .WithOne(e => e.Company)
            .OnDelete(DeleteBehavior.SetNull);

            Company company1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "Gogle"
            };
            Company company2 = new()
            {
                Id = Guid.NewGuid(),
                Name = "Mucrosoft"
            };
            modelBuilder.Entity<Company>().HasData(company1, company2);
            modelBuilder.Entity<Employee>().HasData(
                   new Employee { Id = Guid.NewGuid(), Name = "Tom", SurName = "Averen", Age = 28,CompanyId=company1.Id},
                   new Employee { Id = Guid.NewGuid(), Name = "Alice", SurName = "Marol", Age = 26,CompanyId=company2.Id},
                   new Employee { Id = Guid.NewGuid(), Name = "Sam", SurName = "Rafian", Age = 28}); 
            base.OnModelCreating(modelBuilder);
        }
    }
}
