using System.Collections.Concurrent;
using CRM_API.Sessions.Models;
using Domain.Client.ScheduleAgregate.Data;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : CrmController
    {
        public ScheduleController(ConcurrentDictionary<string, Session> sessions) : base(sessions)
        {
        }


        // GET: api/<ScheduleController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (!this.UserAuthenticated)
                return Unauthorized($"Invalid token");

            var scheduleDomain = this.Container.GetService<IDomainClientAgregate<Schedule, ReturnFlag>>();

            var selectAllAsync = await scheduleDomain!.SelectAllAsync();
            if (selectAllAsync.TryGetValue(out var list, out var alert))
            {
                return Ok(list);
            }
            else
            {
                return BadRequest(alert.ToString());
            }
        }

        // GET api/<ScheduleController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!this.UserAuthenticated)
                return Unauthorized($"Invalid token");

            var scheduleDomain = this.Container.GetService<IDomainClientAgregate<Schedule, ReturnFlag>>();

            var selectAsync = await scheduleDomain!.SelectAsync(id);

            if (selectAsync.TryGetValue(out var customer, out var alert))
            {
                return Ok(customer);
            }
            else
            {
                return BadRequest(alert.ToString());
            }
        }

        // POST api/<ScheduleController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Schedule value)
        {
            if (!this.UserAuthenticated)
                return Unauthorized($"Invalid token");

            var scheduleDomain = this.Container.GetService<IDomainClientAgregate<Schedule, ReturnFlag>>();

            var insertAsync = await scheduleDomain!.InsertAsync(value);

            if (insertAsync.TryGetValue(out var customer, out var alert))
            {
                return Ok(customer);
            }
            else
            {
                return BadRequest(alert.ToString());
            }
        }

        // PUT api/<ScheduleController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Schedule value)
        {
            if (!this.UserAuthenticated)
                return Unauthorized($"Invalid token");

            var scheduleDomain = this.Container.GetService<IDomainClientAgregate<Schedule, ReturnFlag>>();

            var updateAsync = await scheduleDomain!.UpdateAsync(value);

            if (updateAsync.TryGetValue(out var customer, out var alert))
            {
                return Ok(customer);
            }
            else
            {
                return BadRequest(alert.ToString());
            }
        }

        // DELETE api/<ScheduleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!this.UserAuthenticated)
                return Unauthorized($"Invalid token");

            var scheduleDomain = this.Container.GetService<IDomainClientAgregate<Schedule, ReturnFlag>>();

            var selectAsync = await scheduleDomain!.SelectAsync(id);

            if (selectAsync.TryGetValue(out var customer, out var alert))
            {
                var deleteAsync = await scheduleDomain.DeleteAsync(customer);

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
