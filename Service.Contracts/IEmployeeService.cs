using Entities.Models;
using Shared.DTO;
using Shared.RequestFeatures;
using System.Dynamic;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        public Task<(IEnumerable<ExpandoObject> employees,MetaData metaData)> GetEmployeesAsync(Guid CompanyId,EmployeeParameters employeeParameters,bool trackChanges);
        public Task<EmployeeDto> GetEmployeeAsync(Guid CompId,Guid EmpId, bool TrackChanges);
        Task<EmployeeDto> CreateEmployeeAsync(Guid companyId, EmployeeCreationDto employeeDto);
        Task DeleteEmployeeAsync(Guid CompId,Guid employeeId);

        Task UpdateEmployeeAsync(Guid CompId,Guid employeeId,EmployeeForUpdateDto employeeDto,bool comptrackChanges=false,bool empTrackChanges=true);
        Task<(EmployeeForUpdateDto employeeToPatch,Employee employeeEntitiy)> 
            GetEmployeeForPatchAsync(Guid CompanyId,Guid employeeId,bool trackCompChanges,bool trackEmpChanges);
        Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity);
    }
}