namespace Data.ModelsCrm
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int IdCompany { get; set; }

        public virtual Company IdCompanyNavigation { get; set; } = null!;
    }
}
