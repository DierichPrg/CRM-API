using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Client.CustomerAgregate;
using Lamar;

namespace DomainDependencyInjection
{
    public class DomainServiceRegister
    {
        public static ServiceRegistry GetRegister()
        {
            ServiceRegistry registry = new ServiceRegistry();
            registry.For<CustomerDomain>().Use<CustomerDomain>();

            return registry;
        }
    }
}
