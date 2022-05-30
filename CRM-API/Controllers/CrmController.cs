using System.Collections.Concurrent;
using CRM_API.Sessions.Models;
using Domain.Interfaces;
using Domain.Server.CompanyAgregate.Data;
using Domain.Server.UserAgregate.Data;
using Lamar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Primitives;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrmController : ControllerBase
    {
        private readonly ConcurrentDictionary<string, Session> sessions;
        private const string token = nameof(token);

        public CrmController(ConcurrentDictionary<string, Session> sessions)
        {
            this.sessions = sessions;
        }

        private string GetToken()
        {
            if (!Request.Headers.Authorization.Contains(token))
                return String.Empty;

            return Request.Headers.Authorization.First(x => x.Equals(token));
        }

        public bool UserAuthenticated => this.sessions.Any(x => x.Key.Equals(this.GetToken()));

        public Session Session => this.sessions[this.GetToken()];

        public Container Container => this.Session.Container;

        public User User => this.Session.User;

        public Company Company => this.Session.Company;
    }
}
