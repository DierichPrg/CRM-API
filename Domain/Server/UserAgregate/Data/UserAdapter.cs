using System.Collections.Immutable;
using UserRepositoryModel = Data.ModelsCrm.User;
using UserAgregateModel = Domain.Server.UserAgregate.Data.User;

namespace Domain.Server.UserAgregate.Data
{
    public static class UserAdapter
    {
        public static UserRepositoryModel ToRepository(this UserAgregateModel user)
        {
            return new UserRepositoryModel()
            {
                Id = user.Id,
                Name = user.Name,
                IdCompany = user.IdCompany,
                Password = user.Password,
                Username = user.Username
            };
        }

        public static UserAgregateModel ToAgregate(this UserRepositoryModel user)
        {
            return new UserAgregateModel()
            {
                Id = user.Id,
                Name = user.Name,
                IdCompany = user.IdCompany,
                Password = user.Password,
                Username = user.Username,
            };
        }

        public static IEnumerable<UserRepositoryModel> ToListRepository(this IEnumerable<UserAgregateModel> users)
        {
            ImmutableArray<UserRepositoryModel> immutableArray = ImmutableArray.Create<UserRepositoryModel>();

            foreach (var schedule in users)
            {
                immutableArray = immutableArray.Add(schedule.ToRepository());
            }

            return immutableArray;
        }

        public static IEnumerable<UserAgregateModel> ToListAgregate(this IEnumerable<UserRepositoryModel> users)
        {
            ImmutableArray<UserAgregateModel> immutableArray = ImmutableArray.Create<UserAgregateModel>();

            foreach (var schedule in users)
            {
                immutableArray = immutableArray.Add(schedule.ToAgregate());
            }

            return immutableArray;
        }
    }
}
