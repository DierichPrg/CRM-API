using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Server.UserAgregate.Data;

namespace Contract.Server.LoginContract
{
    public static class LoginAdpter
    {
        public static User ToDomain(this Login login)
        {
            return new User()
            {
                Username = login.Username,
                Password = login.Password,
            };
        }

        public static Login ToContract(this User user)
        {
            return new Login()
            {
                Username = user.Username,
                Password = user.Password,
            };
        }
    }
}
