using Microsoft.EntityFrameworkCore;
using EmpComp.Repositories;
namespace EmpComp.Extensions
{
    public static class ServiceExtension
    {
        public static void AddDbContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));         
        }
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IMainRepository,MainRepository>();
        }
    }
}
