using Microsoft.AspNetCore.Mvc;
using EmpComp.Repositories;
using EmpComp.ReqRes.EmployeesService.Request;
using EmpComp.ReqRes.EmployeesService.Response;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<GetAllEmployeesResponse>> Get()
        {
            var employees = await _mainRepository.EmployeeRepository.GetAll().ToListAsync();
            if (employees == null || employees.Count==0)
                return NotFound("There are no employees.");

            var employeesResponse = new List<GetOneEmployeeResponse>();
            foreach (var employee in employees)
                employeesResponse.Add(new GetOneEmployeeResponse
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    SurName = employee.SurName,
                    Age = employee.Age,
                    CompanyId = employee.CompanyId,
                    CompanyName=employee.Company?.Name
                });

            return Ok(new GetAllEmployeesResponse
            {
                Employees = employeesResponse
            });
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<GetOneEmployeeResponse>> Get(Guid id)
        {
            var employee = await _mainRepository.EmployeeRepository
                                                .Find(e => e.Id == id).FirstOrDefaultAsync();
            if (employee == null)
                return NotFound($"There is no employee with {id} id.");

            return Ok(new GetOneEmployeeResponse
            {
                Id = employee.Id,
                Name = employee.Name,
                SurName = employee.SurName,
                Age = employee.Age,
                CompanyId=employee.CompanyId,
                CompanyName = employee.Company?.Name
            });

        }

        [HttpPost]
        public async Task<ActionResult<CreateEmployeeResponse>> Create([FromBody] CreateEmployeeRequest request)
        {
            var employee = await _mainRepository.EmployeeRepository
                                        .Find(e => e.Name == request.Name && e.SurName == request.SurName
                                                && e.Age == request.Age).FirstOrDefaultAsync();
            if (employee != null) return Problem("Employee with this data already exists.");
            Company? company = await _mainRepository.CompanyRepository
                                                   .Find(c => c.Id == request.CompanyId).FirstOrDefaultAsync();
            Employee createdEmployee = new()
            {
                Name = request.Name,
                SurName = request.SurName,
                Age = request.Age,
                Company = company
            };
            await _mainRepository.EmployeeRepository.CreateAsync(createdEmployee);
            await _mainRepository.SaveChangesAsync();
            
            return Ok(new CreateEmployeeResponse
            {
                Employee = new GetOneEmployeeResponse
                {
                    Id=createdEmployee.Id,
                    Name = createdEmployee.Name,
                    SurName= createdEmployee.SurName,
                    Age=createdEmployee.Age,
                    CompanyId = company?.Id,
                    CompanyName= company?.Name
                }
            });
        }

        [HttpPut]
        public async Task<ActionResult<UpdateEmployeeResponse>> Update([FromBody] UpdateEmployeeRequest request)
        {
            var employee = await _mainRepository.EmployeeRepository.Find(e => e.Id == request.Id).FirstOrDefaultAsync();
            if (employee == null)
                return Problem($"Employee with {request.Id} id does not exist.");

            Company? company = await _mainRepository.CompanyRepository
                                                   .Find(c => c.Id == request.CompanyId).FirstOrDefaultAsync();

            employee.Name = request.Name;
            employee.SurName = request.SurName;
            employee.Age = request.Age;
            employee.Company = company;
            

            await _mainRepository.EmployeeRepository.UpdateAsync(employee);
            await _mainRepository.SaveChangesAsync();

            return Ok(new UpdateEmployeeResponse 
            {
                Employee= new GetOneEmployeeResponse
                {
                    Id=employee.Id,
                    Name=employee.Name,
                    SurName=employee.SurName,
                    Age=employee.Age,
                    CompanyId=company?.Id,
                    CompanyName = company?.Name
                }
            });
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var employee = _mainRepository.EmployeeRepository.Find(e => e.Id == id)
                                                             .FirstOrDefault();
            if (employee==null) return NotFound($"Employee with {id} id not found.");

            await _mainRepository.EmployeeRepository.DeleteAsync(employee);
            await _mainRepository.SaveChangesAsync();

            return Ok();
        }
    }
}
