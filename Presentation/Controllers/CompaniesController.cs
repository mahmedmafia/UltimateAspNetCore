

using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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
        public IActionResult GetCompanies()
        {
            var companies =_service.CompanyService.GetAllCompanies(false);
                return Ok(companies);
           
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _service.CompanyService.GetCompany(id,false);
            return Ok(company);
        }
    }
}
