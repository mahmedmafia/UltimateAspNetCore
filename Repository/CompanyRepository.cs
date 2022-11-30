
using Contracts;
using Entities.Models;

namespace Repository
{

    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public IEnumerable<Company> GetAllCompanies(bool trackChanges)
        {
            return FindAll(trackChanges).OrderBy(c=> c.Name).ToList();  
        }

        public Company GetCopmpany(Guid CompanyId, bool trackChanges)
        {
            return FindByCondition((x) => x.Id.Equals(CompanyId), trackChanges).SingleOrDefault() ;
        }
    }
}