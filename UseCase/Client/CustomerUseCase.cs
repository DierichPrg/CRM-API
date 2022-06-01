using System.Collections.Immutable;
using Domain.Interfaces;
using UseCase.Contract.Client.CustomerContract;
using UseCase.Interfaces.Client;
using Utils;
using CustomerDomainModel = Domain.Client.CustomerAgregate.Data.Customer;
using ScheduleDomainModel = Domain.Client.ScheduleAgregate.Data.Schedule;
using CustomerDomainReturnFlag = Domain.Client.CustomerAgregate.Data.ReturnFlag;
using ScheduleDomainReturnFlag = Domain.Client.ScheduleAgregate.Data.ReturnFlag;

namespace UseCase.Client
{
    public class CustomerUseCase : ICustomerUseCase
    {
        private readonly IDomainClientAgregate<CustomerDomainModel, CustomerDomainReturnFlag> customerService;
        private readonly IDomainClientAgregate<ScheduleDomainModel, ScheduleDomainReturnFlag> scheduleService;

        public CustomerUseCase(IDomainClientAgregate<CustomerDomainModel, CustomerDomainReturnFlag> customerService, IDomainClientAgregate<ScheduleDomainModel, ScheduleDomainReturnFlag> scheduleService)
        {
            this.customerService = customerService;
            this.scheduleService = scheduleService;
        }

        public async Task<Result<Customer, ReturnFlag>> CreateCustomerAsync(Customer customer)
        {
            var insertAsync = await this.customerService.InsertAsync(customer.ToDomain());

            if (!insertAsync.TryGetValue(out var domainCustomer, out var returnFlag))
                return returnFlag.ToContract();

            return domainCustomer.ToContract();
        }

        public async Task<Result<Customer, ReturnFlag>> UpdateCustomerAsync(Customer customer)
        {
            var updateAsync = await this.customerService.UpdateAsync(customer.ToDomain());

            if (!updateAsync.TryGetValue(out var domainCustomer, out var returnFlag))
                return returnFlag.ToContract();

            return domainCustomer.ToContract();
        }

        public async Task<Result<Customer, ReturnFlag>> SelectCustomerAsync(Customer customer)
        {
            var selectAsync = await this.customerService.SelectAsync(customer.Id);

            if (!selectAsync.TryGetValue(out var domainCustomer, out var returnFlag))
                return returnFlag.ToContract();

            return domainCustomer.ToContract();
        }

        public async Task<Result<Customer, ReturnFlag>> SelectCustomerAsync(int id)
        {
            var selectAsync = await this.customerService.SelectAsync(id);

            if (!selectAsync.TryGetValue(out var domainCustomer, out var returnFlag))
                return returnFlag.ToContract();

            return domainCustomer.ToContract();
        }

        public async Task<Result<IEnumerable<Customer>, ReturnFlag>> SelectAllCustomersAsync()
        {
            var selectAllAsync = await this.customerService.SelectAllAsync();

            if (!selectAllAsync.TryGetValue(out var domainCustomers, out var returnFlag))
                return returnFlag.ToContract();

            return ImmutableArray.Create<Customer>().AddRange(domainCustomers.ToListContract());
        }

        public async Task<Result<bool, ReturnFlag>> DeleteCustomerAsync(Customer customer)
        {
            var allSchedules = await this.scheduleService.SelectAllAsync();

            if (!allSchedules.TryGetValue(out var schedules, out var alert))
                return ReturnFlag.Error;

            schedules = schedules.Where(x => x.IdCustomer != customer.Id).ToList();

            var deleteRangeAsync = await this.scheduleService.DeleteRangeAsync(schedules);
            if (deleteRangeAsync.IsError)
                return ReturnFlag.Error;

            var deleteAsync = await this.customerService.DeleteAsync(customer.ToDomain());

            return deleteAsync.IsOk;
        }
    }
}
