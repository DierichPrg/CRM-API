using System.Collections.Immutable;
using Data.ModelsCrm;
using CompanyRepositoryModel = Data.ModelsCrm.Company;
using CompanyAgregateModel = Domain.Server.CompanyAgregate.Data.Company;

namespace Domain.Server.CompanyAgregate.Data
{
    public static class CompanyAdpter
    {
        public static CompanyRepositoryModel ToRepository(this CompanyAgregateModel company)
        {
            return new CompanyRepositoryModel()
            {
                Id = company.Id,
                Identification = company.Identification,
                Name = company.Name,
                Users = Array.Empty<User>()
            };
        }

        public static CompanyAgregateModel ToAgregate(this CompanyRepositoryModel company)
        {
            return new CompanyAgregateModel()
            {
                Id = company.Id,
                Identification = company.Identification,
                Name = company.Name,
            };
        }

        public static IEnumerable<CompanyRepositoryModel> ToListRepository(this IEnumerable<CompanyAgregateModel> companys)
        {
            ImmutableArray<CompanyRepositoryModel> immutableArray = ImmutableArray.Create<CompanyRepositoryModel>();

            foreach (var schedule in companys)
            {
                immutableArray = immutableArray.Add(schedule.ToRepository());
            }

            return immutableArray;
        }

        public static IEnumerable<CompanyAgregateModel> ToListAgregate(this IEnumerable<CompanyRepositoryModel> companys)
        {
            ImmutableArray<CompanyAgregateModel> immutableArray = ImmutableArray.Create<CompanyAgregateModel>();

            foreach (var schedule in companys)
            {
                immutableArray = immutableArray.Add(schedule.ToAgregate());
            }

            return immutableArray;
        }
    }
}
