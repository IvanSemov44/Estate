using Microsoft.AspNetCore.JsonPatch;
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
            var employees = _service.EmployeeService.getEmployees(companyId, trackChanges: false);

            return Ok(employees);
        }

        [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
        public IActionResult GetEmployeeForCompany(Guid companyId, Guid id)
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
        public IActionResult DeleteEmployeeForCompany(Guid companyId, Guid id)
        {
            _service.EmployeeService.DeleteEmployeeForCompany(companyId, id, trackChanges: false);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateEmployeeForCompany(Guid companyId, Guid id,
            [FromBody] EmployeeForUpdateDto employeeForUpdate)
        {
            if (employeeForUpdate is null)
                return BadRequest("Employee object is null");

            _service.EmployeeService.UpdateEmployeeForCompany(companyId,
                id,
                employeeForUpdate,
                compTrackChanges: false,
                empTrackChanges: true);

            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public IActionResult PartiallyUpdateEmployeeForCompany(Guid companyId,Guid id,
            [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("PachDoc object sent from client is null");

            var (employeeToPatch, empoyeeEntity) = _service.EmployeeService.GetEmployeeForPatch(
                companyId, id, compTrackChanges: false, empTrackChanges: true);

            patchDoc.ApplyTo(employeeToPatch);

            _service.EmployeeService.SaveChangesForPatch(employeeToPatch, empoyeeEntity);

            return NoContent();
        }


    }
}