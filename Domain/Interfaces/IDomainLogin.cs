using Domain.Server.UserAgregate.Data;
using Utils;

namespace Domain.Interfaces
{
    public interface IDomainLogin
    {
        public Task<Result<User, bool>> LoginAsync(User value);
    }
}
