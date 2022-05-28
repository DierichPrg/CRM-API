using System.Collections.Immutable;
using CustomerRepositoryModel = Data.ModelsCrmClient.Customer;
using CustomerAgregateModel = Domain.Client.CustomerAgregate.Data.Customer;

namespace Domain.Client.CustomerAgregate.Data
{
    public static class CustomerAdpter
    {
        public static CustomerRepositoryModel ToRepository(this CustomerAgregateModel customer)
        {
            return new CustomerRepositoryModel()
            {
                BrithDay = customer.BrithDay,
                Created = customer.Created,
                Id = customer.Id,
                Identification = customer.Identification,
                Name = customer.Name,
            };
        }

        public static CustomerAgregateModel ToAgregate(this CustomerRepositoryModel customer)
        {
            return new CustomerAgregateModel()
            {
                BrithDay = customer.BrithDay,
                Created = customer.Created,
                Id = customer.Id,
                Identification = customer.Identification,
                Name = customer.Name,
            };
        }

        public static IEnumerable<CustomerRepositoryModel> ToListRepository(this IEnumerable<CustomerAgregateModel> customers)
        {
            ImmutableArray<CustomerRepositoryModel> immutableArray = ImmutableArray.Create<CustomerRepositoryModel>();

            foreach (var schedule in customers)
            {
                immutableArray = immutableArray.Add(schedule.ToRepository());
            }

            return immutableArray;
        }

        public static IEnumerable<CustomerAgregateModel> ToListAgregate(this IEnumerable<CustomerRepositoryModel> customers)
        {
            ImmutableArray<CustomerAgregateModel> immutableArray = ImmutableArray.Create<CustomerAgregateModel>();

            foreach (var schedule in customers)
            {
                immutableArray = immutableArray.Add(schedule.ToAgregate());
            }

            return immutableArray;
        }
    }
}
