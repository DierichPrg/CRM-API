namespace UseCase.Contract.Client.CustomerContract
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime BrithDay { get; set; }
        public DateTime Created { get; set; }
        public string Identification { get; set; } = null!;
    }
}
