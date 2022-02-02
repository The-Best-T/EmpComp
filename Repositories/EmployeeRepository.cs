using EmpComp.Models;
using EmpComp.Repositories.Base;
namespace EmpComp.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>,IEmployeeRepository
    {
        public EmployeeRepository(ApplicationContext context): base(context)
        {
        }
    }
}
