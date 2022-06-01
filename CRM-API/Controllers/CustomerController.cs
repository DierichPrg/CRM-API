using System.Collections.Concurrent;
using CRM_API.Sessions.Models;
using Microsoft.AspNetCore.Mvc;
using UseCase.Contract.Client.CustomerContract;
using UseCase.Interfaces.Client;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : CrmController
    {
        public CustomerController(ConcurrentDictionary<string, Session> sessions) : base(sessions)
        {
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (!this.UserAuthenticated)
                return Unauthorized($"Invalid token");

            var customerUseCase = this.Container.GetService<ICustomerUseCase>();


            var selectAllAsync = await customerUseCase!.SelectAllCustomersAsync();
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
            if (!this.UserAuthenticated)
                return Unauthorized($"Invalid token");

            var customerUseCase = this.Container.GetService<ICustomerUseCase>();

            var selectAsync = await customerUseCase!.SelectCustomerAsync(id);

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
            if (!this.UserAuthenticated)
                return Unauthorized($"Invalid token");

            var customerUseCase = this.Container.GetService<ICustomerUseCase>();

            var insertAsync = await customerUseCase!.CreateCustomerAsync(value);

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
            if (!this.UserAuthenticated)
                return Unauthorized($"Invalid token");

            var customerUseCase = this.Container.GetService<ICustomerUseCase>();

            var updateAsync = await customerUseCase!.UpdateCustomerAsync(value);

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
            if (!this.UserAuthenticated)
                return Unauthorized($"Invalid token");

            var customerUseCase = this.Container.GetService<ICustomerUseCase>();

            var selectAsync = await customerUseCase!.SelectCustomerAsync(id);

            if (selectAsync.TryGetValue(out var customer, out var alert))
            {
                var deleteAsync = await customerUseCase.DeleteCustomerAsync(customer);

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
