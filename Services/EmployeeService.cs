
using AutoMapper;
using Contracts;
using Entities.Exeptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTO;
using Shared.RequestFeatures;
using System.ComponentModel.Design;
using System.Dynamic;

namespace Services
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<EmployeeDto> _dataShaper;



        public EmployeeService(IRepositoryManager repositoryManager,ILoggerManager logger,IMapper mapper,IDataShaper<EmployeeDto> dataShaper) 
        {
            _repository = repositoryManager;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;
        }

        public async Task<EmployeeDto> GetEmployeeAsync(Guid CompanyId, Guid employeeId, bool trackChanges)
        {
            await CheckIFCompanyExists(CompanyId);
            Employee employeeEntity = await GetEmployeeForCompanyAndCheckIfItExists(CompanyId, employeeId, trackChanges);
            return _mapper.Map<EmployeeDto>(employeeEntity);
        }
        public async Task<(IEnumerable<ExpandoObject> employees, MetaData metaData)> GetEmployeesAsync(Guid CompanyId, EmployeeParameters employeeParameters,bool trackChanges)
        {
            if (!employeeParameters.ValidAgeRange) throw new MaxAgeRangeBadRequestException();
            await CheckIFCompanyExists(CompanyId);
            var employeesWithMetaData = await _repository.Employee.GetEmployeesAsync(CompanyId, employeeParameters, trackChanges);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesWithMetaData);
            var shapedData = _dataShaper.ShapeData(employeesDto, employeeParameters.Fields);
            return (shapedData, employeesWithMetaData.MetaData);
        }
        public async Task<EmployeeDto> CreateEmployeeAsync(Guid CompanyId, EmployeeCreationDto employeeDto)
        {
            await CheckIFCompanyExists(CompanyId);
            var employeeEntity = _mapper.Map<Employee>(employeeDto);
            employeeEntity.CompanyId = CompanyId;
            _repository.Employee.CreateEmployee(employeeEntity);
            await _repository.SaveAsync();
            return _mapper.Map<EmployeeDto>(employeeEntity);
        }

        public async Task DeleteEmployeeAsync(Guid CompanyId, Guid employeeId)
        {
            await CheckIFCompanyExists(CompanyId);
            Employee employeeEntity = await GetEmployeeForCompanyAndCheckIfItExists(CompanyId, employeeId, false);
            _repository.Employee.DeleteEmployee(employeeEntity);
            await _repository.SaveAsync();
        }
      
        public async Task UpdateEmployeeAsync(Guid CompanyId, Guid employeeId, EmployeeForUpdateDto employeeDto, bool compTrackChanges = false, bool empTrackChanges = true)
        {
            Employee employeeEntity = await GetEmployeeForCompanyAndCheckIfItExists(CompanyId, employeeId, empTrackChanges);
            _mapper.Map(employeeDto, employeeEntity);
            await _repository.SaveAsync();
        }
        public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntitiy)> GetEmployeeForPatchAsync(Guid CompanyId, Guid employeeId, bool trackCompChanges, bool trackEmpChanges)
        {
            await CheckIFCompanyExists(CompanyId, trackCompChanges);
            Employee employeeEntity = await GetEmployeeForCompanyAndCheckIfItExists(CompanyId, employeeId, trackEmpChanges);
            var employeeDto = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);
            return (employeeDto, employeeEntity);

        }
        public async Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)
        {
            _mapper.Map(employeeToPatch,employeeEntity);
            await _repository.SaveAsync();
        }

     

      

      

        private async Task<Employee> GetEmployeeForCompanyAndCheckIfItExists(Guid CompanyId, Guid employeeId, bool empTrackChanges)
        {
            await CheckIFCompanyExists(CompanyId);
            var employeeEntity = await _repository.Employee.GetEmployeeAsync(CompanyId, employeeId, empTrackChanges);
            if (employeeEntity is null) throw new EmployeeNotFoundException(employeeId);
            return employeeEntity;
        }

        private async Task CheckIFCompanyExists(Guid CompanyId, bool trackChanges=false)
        {
            var company = await _repository.Company.GetCopmpanyAsync(CompanyId, trackChanges);
            if (company is null) throw new CompanyNotFoundException(CompanyId);
        }
    }
}