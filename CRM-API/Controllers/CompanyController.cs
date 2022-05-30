using Domain.Interfaces;
using Domain.Server.CompanyAgregate.Data;
using Microsoft.AspNetCore.Mvc;
using ReturnCompanyFlag = Domain.Server.CompanyAgregate.Data.ReturnFlag;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IDomainServerAgregate<Company, ReturnFlag> domainCompanyService;

        public CompanyController(IDomainServerAgregate<Company, ReturnCompanyFlag> domainCompanyService)
        {
            this.domainCompanyService = domainCompanyService;
        }

        // GET: api/<CompanyController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var selectAllAsync = await this.domainCompanyService.SelectAllAsync();

            if (!selectAllAsync.TryGetValue(out var companies, out var alert))
                return BadRequest(alert.ToString());

            return Ok(companies);
        }

        // GET api/<CompanyController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var selectAsync = await this.domainCompanyService.SelectAsync(id);

            if (!selectAsync.TryGetValue(out var company, out var alert))
                return BadRequest(alert.ToString());

            return Ok(company);
        }

        // POST api/<CompanyController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Company value)
        {
            var insertAsync = await this.domainCompanyService.InsertAsync(value);

            if (insertAsync.TryGetValue(out var company, out var alert))
            {
                return Ok(company);
            }
            else
            {
                return BadRequest(alert.ToString());
            }
        }

        // PUT api/<CompanyController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Company value)
        {
            var updateAsync = await this.domainCompanyService.UpdateAsync(value);

            if (updateAsync.TryGetValue(out var company, out var alert))
            {
                return Ok(company);
            }
            else
            {
                return BadRequest(alert.ToString());
            }
        }

        // DELETE api/<CompanyController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var selectAsync = await this.domainCompanyService.SelectAsync(id);

            if (selectAsync.TryGetValue(out var company, out var alert))
            {
                var deleteAsync = await this.domainCompanyService.DeleteAsync(company);

                if (deleteAsync.TryGetValue(out var deleted, out alert))
                {
                    return Ok(deleted);
                }
                else
                {
                    return BadRequest(alert.ToString());
                }
            }
            else
            {
                return BadRequest(alert.ToString());
            }
        }
    }
}
