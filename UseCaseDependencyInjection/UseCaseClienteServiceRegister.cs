using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Client.ScheduleAgregate;
using Domain.Interfaces;
using Lamar;
using UseCase.Client;
using UseCase.Interfaces.Client;

namespace UseCaseDependencyInjection
{
    public class UseCaseClienteServiceRegister
    {
        public static ServiceRegistry GetRegister()
        {
            ServiceRegistry registry = new ServiceRegistry();
            registry.For<ICustomerUseCase>().Use<CustomerUseCase>();
            registry.For<IScheduleUseCase>().Use<ScheduleUseCase>();

            return registry;
        }
    }
}
