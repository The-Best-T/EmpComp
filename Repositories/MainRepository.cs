using EmpComp.Models;
namespace EmpComp.Repositories
{
    public class MainRepository : IMainRepository
    {
        private readonly ApplicationContext _context;
        public IEmployeeRepository EmployeeRepository { get; set; }
        public MainRepository(ApplicationContext context,IEmployeeRepository employeeRepository)
        {
            _context = context;
            EmployeeRepository = employeeRepository;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
