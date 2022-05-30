using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Server.UserAgregate.Data;

namespace Domain.Interfaces
{
    public interface IDomainLogin
    {
        public Task<Result<User, bool>> LoginAsync(User value);
    }
}
