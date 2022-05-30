using Domain.Interfaces;
using Domain.Server.CompanyAgregate;
using Domain.Server.CompanyAgregate.Data;
using Domain.Server.UserAgregate;
using Domain.Server.UserAgregate.Data;
using Lamar;
using ReturnCompanyFlag = Domain.Server.CompanyAgregate.Data.ReturnFlag;
using ReturnUserFlag = Domain.Server.UserAgregate.Data.ReturnFlag;

namespace DomainDependencyInjection
{
    public class DomainServerServiceRegister
    {
        public static ServiceRegistry GetRegister()
        {
            ServiceRegistry registry = new ServiceRegistry();
            registry.For<IDomainServerAgregate<Company, ReturnCompanyFlag>>().Use<CompanyDomain>();
            registry.For<IDomainServerAgregate<User, ReturnUserFlag>>().Use<UserDomain>();
            registry.For<IDomainLogin>().Use<UserDomain>();

            return registry;
        }
    }
}
