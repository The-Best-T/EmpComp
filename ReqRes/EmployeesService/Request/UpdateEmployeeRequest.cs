using System.ComponentModel.DataAnnotations;
namespace EmpComp.ReqRes.EmployeesService.Request
{
    public class UpdateEmployeeRequest
    { 
        [Required] public Guid Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string SurName { get; set; }
        [Required] public int Age { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
