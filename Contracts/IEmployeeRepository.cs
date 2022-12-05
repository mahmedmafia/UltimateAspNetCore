
using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        public Task<PagedList<Employee>> GetEmployeesAsync(Guid CompanyId, EmployeeParameters employeeParameters, bool trackChanges);
        public Task<Employee> GetEmployeeAsync(Guid CompanyId, Guid EmpId, bool TrackChanges);
        public void CreateEmployee(Employee employee);  
        public void DeleteEmployee(Employee employee);
    }
}
