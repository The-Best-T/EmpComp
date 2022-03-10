namespace EmpComp.Repositories
{
    public interface IMainRepository 
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public ICompanyRepository CompanyRepository { get; set; }
        public int SaveChanges();
        public Task<int> SaveChangesAsync();
    }
}
