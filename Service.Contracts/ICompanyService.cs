using Shared.DTO;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
        public CompanyDto GetCompany(Guid companyId,bool trackChanges);

    }
}