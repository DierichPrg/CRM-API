using System.Collections.Immutable;
using CustomerDomainModel = Domain.Client.CustomerAgregate.Data.Customer;
using CustomerContractModel = UseCase.Contract.Client.CustomerContract.Customer;

namespace UseCase.Contract.Client.CustomerContract
{
    public static class CustomerAdpter
    {
        public static CustomerDomainModel ToDomain(this CustomerContractModel customer)
        {
            return new CustomerDomainModel()
            {
                BrithDay = customer.BrithDay,
                Created = customer.Created,
                Identification = customer.Identification,
                Id = customer.Id,
                Name = customer.Name
            };
        }

        public static CustomerContractModel ToContract(this CustomerDomainModel customer)
        {
            return new CustomerContractModel()
            {
                BrithDay = customer.BrithDay,
                Created = customer.Created,
                Identification = customer.Identification,
                Id = customer.Id,
                Name = customer.Name
            };
        }

        public static IEnumerable<CustomerContractModel> ToListContract(this IEnumerable<CustomerDomainModel> customers)
        {
            ImmutableArray<CustomerContractModel> immutableArray = ImmutableArray.Create<CustomerContractModel>();

            foreach (var customer in customers)
            {
                immutableArray = immutableArray.Add(customer.ToContract());
            }

            return immutableArray;
        }
    }
}
