using EmpComp.ReqRes.EmployeesService.Response;
namespace EmpComp.ReqRes.CompaniesService.Response
{
    public class GetOneCompanyResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<GetOneEmployeeResponse> Employees { get; set; } = new();
    }
}
