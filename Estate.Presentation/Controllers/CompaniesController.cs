using Microsoft.AspNetCore.Mvc;

using Estate.Presentation.ModelBinders;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Estate.Presentation.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CompaniesController(IServiceManager service)
        {
            this._service = service;
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {

            var companies = _service.CompanyService.GetAllCompanies(trackChanges: false);

            return Ok(companies);
        }

        [HttpGet("{id:guid}", Name = "CompanyById")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _service.CompanyService.GetCompany(id, trackChanges: false);
            return Ok(company);
        }

        [HttpGet("collection/({ids})", Name = "CompanyCollection")]
        public IActionResult GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            var companies = _service.CompanyService.GetByIds(ids, trackChanges: false);

            return Ok(companies);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company is null)
                return BadRequest("CompanyForCreationDto object is null");

            var createdCompany = _service.CompanyService.CreateCompany(company);

            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
        }

        [HttpPost("collection")]
        public IActionResult CreateCompanyColletion([FromBody] IEnumerable<CompanyForCreationDto> companyCollection)
        {
            var (companies, ids) = _service.CompanyService.CreateCompanyCollection(companyCollection);

            return CreatedAtRoute("companyCollection", new { ids }, companies);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteCompany(Guid id)
        {
            _service.CompanyService.DeleteCompany(id,trackChanges:false);

            return NoContent();
        }

        [HttpPut("{id:guid}")]

        public IActionResult UpdateCompany(Guid id, [FromBody] CompanyForUpdateDto companyForUpdate)
        {
            if (companyForUpdate is null)
                return BadRequest("CompanyForUpdateDto object is null");

            _service.CompanyService.UpdateCompany(id,companyForUpdate,trackChanges:true);

            return NoContent();
        }
    }
}
