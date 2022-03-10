using System.ComponentModel.DataAnnotations;
namespace EmpComp.ReqRes.CompaniesService.Request
{
    public class CreateCompanyRequest
    {
        [Required] public string Name { get; set; }
    }
}
