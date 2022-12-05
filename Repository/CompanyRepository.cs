
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{

    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateCompany(Company company)
        {
            Create(company);
        }

        public void DeleteCompany(Company company)
        {
            Delete(company);
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c=> c.Name).ToListAsync();  
        }

        public async Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            return await FindByCondition((x) => ids.Contains(x.Id), trackChanges).ToListAsync();
        }

        public async Task<Company> GetCopmpanyAsync(Guid CompanyId, bool trackChanges)
        {
            return await FindByCondition((x) => x.Id.Equals(CompanyId), trackChanges).SingleOrDefaultAsync() ;
        }
    }
}