
using Entities.Models;

namespace Contracts
{
    public interface ICompanyRepository
    {
        public IEnumerable<Company> GetAllCompanies(bool trackChanges);
        public Company GetCopmpany(Guid CompanyId,bool trackChanges);

    }
}
