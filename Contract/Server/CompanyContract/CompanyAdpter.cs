using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyDomainModel = Domain.Server.CompanyAgregate.Data.Company;
using CompanyContractModel = Contract.Server.CompanyContract.Company;

namespace Contract.Server.CompanyContract
{
    public static class CompanyAdpter
    {
        public static CompanyDomainModel ToDomain(this CompanyContractModel company)
        {
            return new CompanyDomainModel()
            {
                Id = company.Id,
                Identification = company.Identification,
                Name = company.Name
            };
        }

        public static CompanyContractModel ToContract(this CompanyDomainModel company)
        {
            return new CompanyContractModel()
            {
                Id = company.Id,
                Identification = company.Identification,
                Name = company.Name
            };
        }
    }
}
