using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Estate.Presentation.Controllers
{
    [Route("api/companies/{companyId}/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IServiceManager _service;

        public EmployeeController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetEmployeesForCompany(Guid companyId)
        {
            var employees = _service.EmployeeService.getEmployees(companyId,trackChanges:false);

            return Ok(employees);
        }

        [HttpGet("{id:guid}")]

        public IActionResult GetEmployeeForCompany(Guid companyId,Guid id)
        {
            var employee = _service.EmployeeService.GetEmployee(companyId, id, trackChanges: false);

            return Ok(employee);
        }
    }
}
