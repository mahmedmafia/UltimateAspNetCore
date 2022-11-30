﻿
using AutoMapper;
using Contracts;
using Service.Contracts;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<ICompanyService> _companyService;

        public ServiceManager(IRepositoryManager repositoryManager,ILoggerManager logger,IMapper mapper)
        {
            _employeeService = new Lazy<IEmployeeService>(()=> new EmployeeService(repositoryManager, logger,mapper));
            _companyService = new Lazy<ICompanyService>(() => new CompanyService(repositoryManager, logger,mapper));

        }

        public IEmployeeService EmployeeService => _employeeService.Value;

        public ICompanyService CompanyService => _companyService.Value;
    }
}