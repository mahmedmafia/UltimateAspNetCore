
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository
{

    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public async Task<PagedList<Employee>> GetEmployeesAsync(Guid CompanyId,EmployeeParameters employeeParameters ,bool trackChanges)
        {
            var items= await this.FindByCondition((x) => x.CompanyId.Equals(CompanyId),trackChanges)
                .FilterEmployees(employeeParameters.MinAge,employeeParameters.MaxAge)
                .Search(employeeParameters.SearchTerm)
                .Sort(employeeParameters.OrderBy)
                .Skip((employeeParameters.PageNumber-1)*employeeParameters.PageSize)
                .Take(employeeParameters.PageSize)
                .ToListAsync();
            var count = await this.FindByCondition(x => x.CompanyId.Equals(CompanyId),trackChanges).CountAsync();
            return PagedList<Employee>.
                ToPagedList(items,count ,employeeParameters.PageNumber, employeeParameters.PageSize);
              
        }
      
        public async Task<Employee> GetEmployeeAsync(Guid CompanyId, Guid EmpId, bool TrackChanges)
        {
            return await this.FindByCondition(x => x.Id.Equals(EmpId) && x.CompanyId.Equals(CompanyId), TrackChanges).SingleOrDefaultAsync();

        }

        public void CreateEmployee(Employee employee)
        {
            Create(employee);
        }

      
        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }
    }
}