namespace Entities.Exeptions
{
    public class EmployeeNotFoundException : NotFoundException
    {
        public EmployeeNotFoundException(Guid id) : base($"The Employee with id {id} dosen't exist")
        {
        }
    }
}
