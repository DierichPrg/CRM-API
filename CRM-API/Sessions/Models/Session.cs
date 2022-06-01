using System.Collections.Concurrent;
using System.Timers;
using Data.ModelsCrmClient;
using Domain.Server.CompanyAgregate.Data;
using Domain.Server.UserAgregate.Data;
using DomainDependencyInjection;
using Lamar;
using Microsoft.EntityFrameworkCore;
using UseCaseDependencyInjection;
using Timer = System.Timers.Timer;

namespace CRM_API.Sessions.Models
{
    public class Session : IDisposable
    {
        private readonly ConcurrentDictionary<string, Session> sessions;
        private const byte minutesToCheckSession = 1;
        private const byte minutesToDie = 15;
        private const ushort constMinuteToMilliSeconds = 60000;
        private readonly Timer sessionTimer;
        private DateTime lastRequest;
        private readonly string token;
        private readonly User user;
        private readonly Company company;
        private readonly Container container;

        private Session(ConcurrentDictionary<string, Session> sessions, string connectionString)
        {
            this.sessions = sessions;
            this.token = Guid.NewGuid().ToString();

            this.sessionTimer = new Timer(minutesToCheckSession * constMinuteToMilliSeconds);
            this.lastRequest = DateTime.Now;

            this.sessionTimer.Elapsed += SessionTimeOutCheck;
            this.container = new Container(x =>
            {
                x.Include(DomainClientServiceRegister.GetRegister());
                x.Include(UseCaseClienteServiceRegister.GetRegister());
                x.For<Session>().Use(this);

                x.AddDbContext<CrmClientContext>(opt =>
                {
                    opt.UseSqlite(connectionString, b => b.MigrationsAssembly("CRM-API"));
                });
            });

            this.sessionTimer.Start();

            if (!sessions.TryAdd(this.token, this))
            {
                this.Dispose();
                throw new InvalidOperationException("Exception to add session ou sessions");
            }
        }

        public Session(ConcurrentDictionary<string, Session> sessions, User user, Company company) : this(sessions, "Data Source=crmClientDataBase.db3")
        {
            this.user = user;
            this.company = company;
        }


        public string Token => this.token;
        public DateTime LastRequest => this.lastRequest;
        public User User => this.user;
        public Company Company => this.company;
        public Container Container
        {
            get
            {
                this.UpdateLastRequest(DateTime.Now);

                return this.container;
            }
        }

        private void UpdateLastRequest(DateTime date)
        {
            this.lastRequest = date;
        }

        public void UpdateLastRequest(DateTime? date = null)
        {
            this.UpdateLastRequest(date ?? DateTime.Now);
        }

        private void SessionTimeOutCheck(object? sender, ElapsedEventArgs e)
        {
            if ((DateTime.Now - this.lastRequest).Minutes >= minutesToDie)
                this.Dispose();
        }

        public void Dispose()
        {
            this.sessions.Remove(this.token, out _);
            this.sessionTimer.Stop();
            this.sessionTimer.Dispose();
            this.container.Dispose();
        }
    }
}
