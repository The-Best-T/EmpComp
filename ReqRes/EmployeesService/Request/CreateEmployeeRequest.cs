using System.ComponentModel.DataAnnotations;
namespace EmpComp.ReqRes.EmployeesService.Request
{
    public class CreateEmployeeRequest
    {
        [Required] public string Name { get; set; }
        [Required] public string SurName { get; set; }
        [Required] public int Age { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
