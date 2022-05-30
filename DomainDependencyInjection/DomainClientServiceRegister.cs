using Domain.Client.CustomerAgregate;
using Domain.Client.CustomerAgregate.Data;
using Domain.Client.ScheduleAgregate;
using Domain.Client.ScheduleAgregate.Data;
using Domain.Interfaces;
using Lamar;
using ReturnCustomerFlag = Domain.Client.CustomerAgregate.Data.ReturnFlag;
using ReturnScheduleFlag = Domain.Client.ScheduleAgregate.Data.ReturnFlag;

namespace DomainDependencyInjection
{
    public class DomainClientServiceRegister
    {
        public static ServiceRegistry GetRegister()
        {
            ServiceRegistry registry = new ServiceRegistry();
            registry.For<IDomainClientAgregate<Customer, ReturnCustomerFlag>>().Use<CustomerDomain>();
            registry.For<IDomainClientAgregate<Schedule, ReturnScheduleFlag>>().Use<ScheduleDomain>();

            return registry;
        }
    }
}
