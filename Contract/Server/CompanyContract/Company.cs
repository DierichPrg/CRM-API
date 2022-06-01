using Domain.Interfaces;

namespace Contract.Server.CompanyContract
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Identification { get; set; } = null!;
    }
}
