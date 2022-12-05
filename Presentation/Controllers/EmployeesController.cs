

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.ActionFilters;
using Service.Contracts;
using Shared.DTO;
using Shared.RequestFeatures;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Presentation.Controllers
{
    [Route("api/companies/{companyId}/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public EmployeesController(IServiceManager serviceManager)
        {
            _service = serviceManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeesForCompany(Guid companyId, [FromQuery] EmployeeParameters employeeParameters)
        {
            var result = await _service.EmployeeService.GetEmployeesAsync(companyId,employeeParameters ,false);
            Response.Headers.Add("x-Pagination", JsonSerializer.Serialize(result.metaData));
            return Ok(result.employees);
        }
    

        [HttpGet("{empId:Guid}",Name ="EmployeeById")]
        public async Task<IActionResult> GetEmployee(Guid companyId,Guid empId)
        {
            var employee = await _service.EmployeeService.GetEmployeeAsync(companyId, empId, false);
            return Ok(employee);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEmployee(Guid companyId, [FromBody] EmployeeCreationDto employeeDto)
        {
            var createdEmployee =await _service.EmployeeService.CreateEmployeeAsync(companyId,employeeDto);
            return CreatedAtRoute("EmployeeById",new {companyId,empId= createdEmployee.Id},createdEmployee);
        }
        [HttpDelete("{empId:guid}")]
        public async Task<IActionResult> DeleteEmployeeForCompany(Guid CompanyId,Guid empId)
        {
            await _service.EmployeeService.DeleteEmployeeAsync(CompanyId, empId);
            return NoContent();
        }
        [HttpPut("{empId:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateEmployee(Guid companyId,Guid empId, [FromBody] EmployeeForUpdateDto employeeDto)
        {
         
            await _service.EmployeeService.UpdateEmployeeAsync(companyId, empId,employeeDto);
            return NoContent();
        }
        [HttpPatch("{empId:guid}")]
        public async Task<IActionResult> PatchEmployee(Guid companyId, Guid empId, [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest("Invalid Request Body");
            var result = await _service.EmployeeService.GetEmployeeForPatchAsync(companyId, empId, false, true);
            patchDoc.ApplyTo(result.employeeToPatch,ModelState);
            TryValidateModel(result.employeeToPatch);
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);
            await _service.EmployeeService.SaveChangesForPatchAsync(result.employeeToPatch,result.employeeEntitiy);
            return NoContent();
        }
    }
}
