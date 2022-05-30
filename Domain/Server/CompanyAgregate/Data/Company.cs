using Domain.Interfaces;

namespace Domain.Server.CompanyAgregate.Data
{
    public class Company : IDomainServerModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Identification { get; set; } = null!;
    }
}
