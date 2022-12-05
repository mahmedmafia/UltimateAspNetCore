
using AutoMapper;
using Contracts;
using Entities.Exeptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTO;
using System.ComponentModel.Design;

namespace Services
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;


        public CompanyService(IRepositoryManager repositoryManager,ILoggerManager logger,IMapper mapper)
        {
            _repository = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<CompanyDto> CreateCompanyAsync(CompanyCreationDto companyDto)
        {
            var company = _mapper.Map<Company>(companyDto);
            _repository.Company.CreateCompany(company);
            await _repository.SaveAsync();
            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<(IEnumerable<CompanyDto> companies, string ids)> createCompanyCollectionAsync(IEnumerable<CompanyCreationDto> companyCollection)
        {
            if (companyCollection is null) throw new CompanyCollectionBadRequest();
            var companiesEntitites = _mapper.Map<IEnumerable<Company>>(companyCollection);
            foreach(var company in companiesEntitites)
            {
                _repository.Company.CreateCompany(company);
            }
            await _repository.SaveAsync();
            var createdCompanies = _mapper.Map<IEnumerable<CompanyDto>>(companiesEntitites);
            var ids =string.Join(",", companiesEntitites.Select(c => c.Id));
            return (createdCompanies, ids);
        }

    
        public async Task DeleteCompanyAsync(Guid companyId, bool trackChanges=false)
        {
            var company = await GetCompanyAndCheckIfItExists(companyId, trackChanges);
            _repository.Company.DeleteCompany(company);
            await _repository.SaveAsync();
        }
     


        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool trackChanges)
        {
            var companies = await _repository.Company.GetAllCompaniesAsync(trackChanges);

            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            return companiesDto;
        }

     

        public async Task<IEnumerable<CompanyDto>> GetByIdsAsnc(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null) throw new IdParametersBadRequestException();
            var companies =await _repository.Company.GetByIdsAsync(ids, trackChanges);
            if (ids.Count() != companies.Count()) throw new CollectionByIdsBadRequestException();
            return _mapper.Map<IEnumerable<CompanyDto>>(companies);
        }

        public async Task<CompanyDto> GetCompanyAsync(Guid companyId, bool trackChanges)
        {
            var company = await GetCompanyAndCheckIfItExists(companyId, trackChanges);

            return _mapper.Map<CompanyDto>(company);
        }

      
        public async Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDto comapnyDto, bool trackChanges)
        {
            var company = await GetCompanyAndCheckIfItExists(companyId, trackChanges);
            _mapper.Map(comapnyDto, company);
            _repository.SaveAsync();
        }
        private async Task<Company> GetCompanyAndCheckIfItExists(Guid companyId, bool trackChanges)
        {
            var company = await _repository.Company.GetCopmpanyAsync(companyId, trackChanges);
            if (company is null) throw new CompanyNotFoundException(companyId);
            return company;

        }
    }
}