using Contracts;
using Entities.Models;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public Employee GetEmployee(Guid companyId, Guid id, bool trackChanges)
            => FindByCondition(c => c.CompanyId.Equals(companyId) && c.Id.Equals(id), trackChanges)
            .SingleOrDefault();


        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges)
            => FindByCondition(c => c.CompanyId.Equals(companyId), trackChanges)
            .OrderBy(c => c.Name)
            .ToList();
    }
}
