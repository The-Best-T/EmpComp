using System.ComponentModel.DataAnnotations;
namespace EmpComp.ReqRes.CompaniesService.Request
{
    public class UpdateCompanyRequest
    {
        [Required] public Guid Id { get; set; }
        [Required] public string Name { get; set; }
    }
}
