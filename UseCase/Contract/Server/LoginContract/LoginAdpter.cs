using Domain.Server.UserAgregate.Data;

namespace UseCase.Contract.Server.LoginContract
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
