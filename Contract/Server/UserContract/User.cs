using Domain.Interfaces;

namespace Contract.Server.UserContract
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int IdCompany { get; set; }
    }
}
