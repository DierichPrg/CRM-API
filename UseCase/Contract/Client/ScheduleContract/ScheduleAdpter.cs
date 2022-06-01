using System.Collections.Immutable;
using ScheduleDomainModel = Domain.Client.ScheduleAgregate.Data.Schedule;
using ScheduleContractModel = UseCase.Contract.Client.ScheduleContract.Schedule;

namespace UseCase.Contract.Client.ScheduleContract
{
    public static class ScheduleAdpter
    {
        public static ScheduleDomainModel ToDomain(this ScheduleContractModel schedule)
        {
            return new ScheduleDomainModel()
            {
                Id = schedule.Id,
                Date = schedule.Date,
                Description = schedule.Description,
                IdCustomer = schedule.IdCustomer,
                Title = schedule.Title,
            };
        }

        public static ScheduleContractModel ToContract(this ScheduleDomainModel schedule)
        {
            return new ScheduleContractModel()
            {
                Id = schedule.Id,
                Date = schedule.Date,
                Description = schedule.Description,
                IdCustomer = schedule.IdCustomer,
                Title = schedule.Title,
            };
        }

        public static IEnumerable<ScheduleContractModel> ToListContract(this IEnumerable<ScheduleDomainModel> schedules)
        {
            ImmutableArray<ScheduleContractModel> immutableArray = ImmutableArray.Create<ScheduleContractModel>();

            foreach (var schedule in schedules)
            {
                immutableArray = immutableArray.Add(schedule.ToContract());
            }

            return immutableArray;
        }
    }
}
