
using Entities.Models;

namespace Contracts
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges);
        public Task<Company> GetCopmpanyAsync(Guid CompanyId,bool trackChanges);
        public void CreateCompany(Company company);
        public Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        public void DeleteCompany(Company company);

    }
}
