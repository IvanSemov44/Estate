using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> getEmployees(Guid companyId, bool trackChanges);   

        EmployeeDto GetEmployee(Guid companyId, Guid id, bool trackChanges);
    }
}
