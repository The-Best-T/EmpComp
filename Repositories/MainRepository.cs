namespace EmpComp.Repositories
{
    public class MainRepository : IMainRepository, IDisposable
    {
        private bool _disposed;
        private readonly ApplicationContext _context;
        public IEmployeeRepository EmployeeRepository { get; set; }
        public ICompanyRepository CompanyRepository { get; set; }
        public MainRepository(ApplicationContext context,IEmployeeRepository employeeRepository,
                              ICompanyRepository companyRepository)
        {
            _context = context;
            EmployeeRepository = employeeRepository;
            CompanyRepository = companyRepository;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing) _context.Dispose();

            _disposed = true;
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
