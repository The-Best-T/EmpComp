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
                return NotFound("There is no employees.");

            var employeesResponse = new List<GetOneEmployeeResponse>();
            foreach (var employee in employees)
                employeesResponse.Add(new GetOneEmployeeResponse
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    SurName = employee.SurName,
                    Patronymic=employee.Patronymic,
                    Age = employee.Age
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
                Patronymic = employee.Patronymic,
                Age = employee.Age
            });

        }

        [HttpPost]
        public async Task<ActionResult<CreateEmployeeResponse>> Create([FromBody] CreateEmployeeRequest request)
        {
            var employee = await _mainRepository.EmployeeRepository
                                        .Find(e => e.Name == request.Name && e.SurName == request.SurName
                                              && e.Patronymic == request.Patronymic && e.Age == request.Age).FirstOrDefaultAsync();
            if (employee != null) return Problem("Employee with this data already exists.");

            Employee createdEmployee = new Employee
            {
                Name = request.Name,
                SurName = request.SurName,
                Patronymic = request.Patronymic,
                Age = request.Age
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
                    Patronymic= createdEmployee.Patronymic,
                    Age=createdEmployee.Age
                }
            });
        }

        [HttpPut]
        public async Task<ActionResult<UpdateEmployeeResponse>> Put([FromBody] UpdateEmployeeRequest request)
        {
            var employee = await _mainRepository.EmployeeRepository.Find(e => e.Id == request.Id).FirstOrDefaultAsync();
            if (employee == null) 
                return Problem($"Employee with {request.Id} id does not exist.");

            employee.Name=request.Name;
            employee.SurName=request.SurName;
            employee.Patronymic=request.Patronymic;
            employee.Age=request.Age;

            await _mainRepository.EmployeeRepository.UpdateAsync(employee);
            await _mainRepository.SaveChangesAsync();

            return Ok(new UpdateEmployeeResponse 
            {
                Employee= new GetOneEmployeeResponse
                {
                    Id=employee.Id,
                    Name=employee.Name,
                    SurName=employee.SurName,
                    Patronymic=employee.Patronymic,
                    Age=employee.Age
                }
            });
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var employee = _mainRepository.EmployeeRepository.Find(e => e.Id == id)
                                                             .FirstOrDefault();
            if (employee==null) return NotFound($"Employee with {id} id not found");

            await _mainRepository.EmployeeRepository.DeleteAsync(employee);
            await _mainRepository.SaveChangesAsync();

            return Ok();
        }
    }
}
