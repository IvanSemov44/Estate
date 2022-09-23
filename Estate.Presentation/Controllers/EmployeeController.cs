using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.Text.RegularExpressions;

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

        [HttpGet("{id:guid}",Name = "GetEmployeeForCompany")]

        public IActionResult GetEmployeeForCompany(Guid companyId,Guid id)
        {
            var employee = _service.EmployeeService.GetEmployee(companyId, id, trackChanges: false);

            return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employeeForCreation)
        {
            if (employeeForCreation is null)
                return BadRequest("EmployeeForCreation object is null");

            var employeeForReturn = _service.EmployeeService
                .CreateEmployeeForCompany(companyId, employeeForCreation, trackChanges: false);

            return CreatedAtRoute("GetEmployeeForCompany",
                                   new { companyId, id = employeeForReturn.Id },
                                   employeeForReturn);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteEmployeeForCompany(Guid companyId,Guid id)
        {
            _service.EmployeeService.DeleteEmployeeForCompany(companyId,id,trackChanges:false);

            return NoContent();
        }
    }
}