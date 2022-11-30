namespace Service.Contracts
{
    public interface IServiceManager
    {
        IEmployeeService EmployeeService { get; }
        ICompanyService CompanyService { get; }

    }
}