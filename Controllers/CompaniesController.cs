using Microsoft.AspNetCore.Mvc;
using EmpComp.Repositories;
using EmpComp.ReqRes.CompaniesService.Request;
using EmpComp.ReqRes.CompaniesService.Response;
using Microsoft.EntityFrameworkCore;
namespace EmpComp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IMainRepository _mainRepository;
        public CompaniesController(IMainRepository mainRepository)
        {
            _mainRepository = mainRepository;
        }

        [HttpGet]
        public async Task<ActionResult<GetAllCompaniesResponse>> Get()
        {
            var companies = await _mainRepository.CompanyRepository.GetAll().ToListAsync();
            if (companies == null || companies.Count == 0) 
                return NotFound("There are no companies.");

            var companiesResponse = new List<GetOneCompanyResponse>();
            foreach (var company in companies)
                companiesResponse.Add(new GetOneCompanyResponse
                {
                    Id = company.Id,
                    Name = company.Name
                });

            return Ok(new GetAllCompaniesResponse
            {
                Companies = companiesResponse
            });
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetOneCompanyResponse>>Get(Guid id)
        {
            var company = await _mainRepository.CompanyRepository
                                               .Find(c => c.Id == id).FirstOrDefaultAsync();
            if (company == null) 
                return NotFound($"There is no company with { id } id.");

            return Ok(new GetOneCompanyResponse
            {
                Id=company.Id,
                Name=company.Name
            });
        }

        [HttpPost]
        public async Task<ActionResult<CreateCompanyResponse>> Create([FromBody] CreateCompanyRequest request)
        {
            var company = await _mainRepository.CompanyRepository
                                               .Find(c=>c.Name==request.Name).FirstOrDefaultAsync();
            if (company != null) return Problem("Company with this name already exists.");

            Company createdCompany = new()
            {
                Name = request.Name
            };
            await _mainRepository.CompanyRepository.CreateAsync(createdCompany);
            await _mainRepository.SaveChangesAsync();

            return Ok(new CreateCompanyResponse
            {
                Company = new GetOneCompanyResponse()
                {
                    Id = createdCompany.Id,
                    Name = createdCompany.Name,
                }
            });
        }

        [HttpPut]
        public async Task<ActionResult<UpdateCompanyResponse>> Update([FromBody] UpdateCompanyRequest request)
        {
            var company = await _mainRepository.CompanyRepository
                                               .Find(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (company == null) return Problem($"Company with {request.Id} id does not exist.");

            company.Name = request.Name;
            await _mainRepository.CompanyRepository.UpdateAsync(company);
            await _mainRepository.SaveChangesAsync();

            return Ok(new UpdateCompanyResponse()
            {
                Company=new GetOneCompanyResponse()
                {
                    Id=company.Id,
                    Name=company.Name
                }
            });
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var company = _mainRepository.CompanyRepository.Find(c => c.Id == id).FirstOrDefault();
            if (company == null) return NotFound($"Company with {id} id not found.");

            await _mainRepository.CompanyRepository.DeleteAsync(company);
            await _mainRepository.SaveChangesAsync();

            return Ok();
        }
    }
}
