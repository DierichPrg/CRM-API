using Domain.Interfaces;
using Domain.Server.UserAgregate.Data;
using Microsoft.AspNetCore.Mvc;
using ReturnUserFlag = Domain.Server.UserAgregate.Data.ReturnFlag;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDomainServerAgregate<User, ReturnFlag> domainUserService;

        public UserController(IDomainServerAgregate<User, ReturnUserFlag> domainUserService)
        {
            this.domainUserService = domainUserService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var selectAllAsync = await this.domainUserService.SelectAllAsync();

            if (!selectAllAsync.TryGetValue(out var users, out var alert))
                return BadRequest(alert.ToString());
            
            return Ok(users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var selectAsync = await this.domainUserService.SelectAsync(id);

            if (!selectAsync.TryGetValue(out var user, out var alert))
                return BadRequest(alert.ToString());

            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User value)
        {
            var insertAsync = await this.domainUserService.InsertAsync(value);

            if (insertAsync.TryGetValue(out var company, out var alert))
            {
                return Ok(company);
            }
            else
            {
                return BadRequest(alert.ToString());
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User value)
        {
            var updateAsync = await this.domainUserService.UpdateAsync(value);

            if (updateAsync.TryGetValue(out var company, out var alert))
            {
                return Ok(company);
            }
            else
            {
                return BadRequest(alert.ToString());
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var selectAsync = await this.domainUserService.SelectAsync(id);

            if (selectAsync.TryGetValue(out var company, out var alert))
            {
                var deleteAsync = await this.domainUserService.DeleteAsync(company);

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
