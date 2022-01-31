using Microsoft.AspNetCore.Mvc;
using EmpComp.Repositories;
namespace EmpComp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMainRepository _mainRepository;

        public EmployeesController(IMainRepository mainRepository)
        {
            _mainRepository = mainRepository;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
           
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
