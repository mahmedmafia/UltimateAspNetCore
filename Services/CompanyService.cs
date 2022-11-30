
using AutoMapper;
using Contracts;
using Entities.Exeptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTO;

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

        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {
                var companies = _repository.Company.GetAllCompanies(trackChanges);

                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

                return companiesDto;
           
        }

        public CompanyDto GetCompany(Guid companyId, bool trackChanges)
        {
            var company = _repository.Company.GetCopmpany(companyId, trackChanges);
            if (company is null) throw new CompanyNotFoundException(companyId);
            return _mapper.Map<CompanyDto>(company);
        }
    }
}