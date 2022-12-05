

using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Presentation.ModelBinders;
using Service.Contracts;
using Shared.DTO;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public CompaniesController(IServiceManager serviceManager)
        {
            _service = serviceManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies =await _service.CompanyService.GetAllCompaniesAsync(false);
            return Ok(companies);

        }
        [HttpGet("{id:guid}", Name = "CompanyById")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var company =await _service.CompanyService.GetCompanyAsync(id, false);
            return Ok(company);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyCreationDto company)
        {
            var createdCompany = await _service.CompanyService.CreateCompanyAsync(company);
            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
        }
        [HttpGet("collection/({ids})", Name = "GetCompanyCollection")]
        public async  Task<IActionResult> GetCompanyCollection([ModelBinder(typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            return Ok(await _service.CompanyService.GetByIdsAsnc(ids, false));
        }
        [HttpPost("collection")]
        public async  Task<IActionResult> createCompanyCollection([FromBody] IEnumerable<CompanyCreationDto> companyColletion)
        {
            var result = await _service.CompanyService.createCompanyCollectionAsync(companyColletion);
            return CreatedAtRoute("GetCompanyCollection", new { result.ids }, result.companies);
        }
        [HttpDelete("{compId}")]
        public async  Task<IActionResult> DeleteCompany(Guid compId)
        {
            await _service.CompanyService.DeleteCompanyAsync(compId);
            return NoContent();
        }
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async  Task<IActionResult> UpdateCompany(Guid compId, CompanyForUpdateDto company)
        {
            await _service.CompanyService.UpdateCompanyAsync(compId, company, true);
            return NoContent();
        }

       
    }
}
