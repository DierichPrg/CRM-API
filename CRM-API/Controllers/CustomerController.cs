using Domain.Client.CustomerAgregate;
using Domain.Client.CustomerAgregate.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDomain customerDomain;

        public CustomerController(CustomerDomain customerDomain)
        {
            this.customerDomain = customerDomain;
        }


        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var selectAllAsync = await this.customerDomain.SelectAllAsync();
            if (selectAllAsync.TryGetValue(out var list, out var alert))
            {
                return Ok(list);
            }
            else
            {
                return BadRequest(alert.ToString());
            }
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var selectAsync = await this.customerDomain.SelectAsync(id);

            if (selectAsync.TryGetValue(out var customer, out var alert))
            {
                return Ok(customer);
            }
            else
            {
                return BadRequest(alert.ToString());
            }
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer value)
        {
            var insertAsync = await this.customerDomain.InsertAsync(value);

            if (insertAsync.TryGetValue(out var customer, out var alert))
            {
                return Ok(customer);
            }
            else
            {
                return BadRequest(alert.ToString());
            }
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer value)
        {
            var updateAsync = await this.customerDomain.UpdateAsync(value);

            if (updateAsync.TryGetValue(out var customer, out var alert))
            {
                return Ok(customer);
            }
            else
            {
                return BadRequest(alert.ToString());
            }
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var selectAsync = await this.customerDomain.SelectAsync(id);

            if (selectAsync.TryGetValue(out var customer, out var alert))
            {
                var deleteAsync = await this.customerDomain.DeleteAsync(customer);

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
