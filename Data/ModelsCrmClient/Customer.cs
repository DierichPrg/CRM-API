namespace Data.ModelsCrmClient
{
    public partial class Customer
    {
        public Customer()
        {
            Schedules = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime BrithDay { get; set; }
        public DateTime Created { get; set; }
        public string Identification { get; set; } = null!;

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
