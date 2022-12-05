using Shared.DTO;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        public Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool trackChanges);
        public Task<CompanyDto> GetCompanyAsync(Guid companyId,bool trackChanges);
        public Task<CompanyDto> CreateCompanyAsync(CompanyCreationDto company);
        public Task<IEnumerable<CompanyDto>> GetByIdsAsnc(IEnumerable<Guid> ids, bool trackChanges);
        Task<(IEnumerable<CompanyDto> companies,string ids)> createCompanyCollectionAsync(IEnumerable<CompanyCreationDto> compainyCollection);
        public Task DeleteCompanyAsync(Guid compId,bool trackChanges=false);
        public Task UpdateCompanyAsync(Guid compId, CompanyForUpdateDto comapnyDto,bool trackChanges);

    }
}