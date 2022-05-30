using System.Collections.Concurrent;
using CRM_API.RestModels;
using CRM_API.Sessions.Models;
using Domain.Interfaces;
using Domain.Server.CompanyAgregate.Data;
using Domain.Server.UserAgregate.Data;
using Microsoft.AspNetCore.Mvc;
using ReturnCompanyFlag = Domain.Server.CompanyAgregate.Data.ReturnFlag;
using ReturnUserFlag = Domain.Server.UserAgregate.Data.ReturnFlag;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ConcurrentDictionary<string, Session> sessions;
        private readonly IDomainServerAgregate<User, ReturnUserFlag> domainUserService;
        private readonly IDomainServerAgregate<Company, ReturnCompanyFlag> domainCompanyService;
        private readonly IDomainLogin loginService;

        public LoginController(ConcurrentDictionary<string, Session> sessions, IDomainLogin loginService, IDomainServerAgregate<User, ReturnUserFlag> domainUserService, IDomainServerAgregate<Company, ReturnCompanyFlag> domainCompanyService)
        {
            this.sessions = sessions;
            this.domainUserService = domainUserService;
            this.domainCompanyService = domainCompanyService;
            this.loginService = loginService;
        }

        // POST api/<LoginController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Login value)
        {
            var tryLogin = await this.loginService.LoginAsync(new User() { Username = value.Username, Password = value.Password });

            if (!tryLogin.TryGetValue(out var userLogged, out _))
                return BadRequest("Invalid username or password");

            var sessionExists = this.sessions.Values.FirstOrDefault(x => x.User.Id == userLogged.Id);

            if (sessionExists is null)
            {
                var company = (await this.domainCompanyService.SelectAsync(userLogged.IdCompany)).GetValue();

                var session = new Session(this.sessions, userLogged, company);

                return Ok(session.Token);
            }
            else
            {
                sessionExists.UpdateLastRequest();

                return Ok(sessionExists.Token);
            }
        }
    }
}
