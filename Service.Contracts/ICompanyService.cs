
using Shared.DataTransferObject;
using System.Diagnostics.SymbolStore;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
        CompanyDto GetCompany(Guid companyId,bool trackChanges);
    }
}
