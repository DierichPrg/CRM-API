using Domain.Interfaces;

namespace Contract.Client.ScheduleContract
{
    public class Schedule
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date { get; set; }
        public int IdCustomer { get; set; }
    }
}
