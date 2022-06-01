using UserDomainModel = Domain.Server.UserAgregate.Data.User;
using UserContractModel = UseCase.Contract.Server.UserContract.User;

namespace UseCase.Contract.Server.UserContract
{
    public static class UserAdpter
    {
        public static UserDomainModel ToDomain(this UserContractModel user)
        {
            return new UserDomainModel()
            {
                Id = user.Id,
                Name = user.Name,
                IdCompany = user.IdCompany,
                Password = user.Password,
                Username = user.Username,
            };
        }

        public static UserContractModel ToContract(this UserDomainModel user)
        {
            return new UserContractModel()
            {
                Id = user.Id,
                Name = user.Name,
                IdCompany = user.IdCompany,
                Password = user.Password,
                Username = user.Username,
            };
        }
    }
}
