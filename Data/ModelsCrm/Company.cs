namespace Data.ModelsCrm
{
    public partial class Company
    {
        public Company()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Identification { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
