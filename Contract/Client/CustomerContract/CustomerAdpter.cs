using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerDomainModel = Domain.Client.CustomerAgregate.Data.Customer;
using CustomerContractModel = Contract.Client.CustomerContract.Customer;

namespace Contract.Client.CustomerContract
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
    }
}
