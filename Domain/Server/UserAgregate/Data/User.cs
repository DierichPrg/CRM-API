using Domain.Interfaces;

namespace Domain.Server.UserAgregate.Data
{
    public class User : IDomainServerModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int IdCompany { get; set; }
    }
}
