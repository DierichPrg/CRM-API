using System.Collections.Immutable;
using ScheduleRepositoryModel = Data.ModelsCrmClient.Schedule;
using ScheduleAgregateModel = Domain.Client.ScheduleAgregate.Data.Schedule;

namespace Domain.Client.ScheduleAgregate.Data
{
    public static class ScheduleAdpter
    {
        public static ScheduleRepositoryModel ToRepository(this ScheduleAgregateModel schedule)
        {
            return new ScheduleRepositoryModel()
            {
                Id = schedule.Id,
                Date = schedule.Date,
                Description = schedule.Description,
                IdCustomer = schedule.IdCustomer,
                Title = schedule.Title
            };
        }

        public static ScheduleAgregateModel ToAgregate(this ScheduleRepositoryModel schedule)
        {
            return new ScheduleAgregateModel()
            {
                Id = schedule.Id,
                Date = schedule.Date,
                Description = schedule.Description,
                IdCustomer = schedule.IdCustomer,
                Title = schedule.Title
            };
        }

        public static IEnumerable<ScheduleRepositoryModel> ToListRepository(this IEnumerable<ScheduleAgregateModel> schedules)
        {
            ImmutableArray<ScheduleRepositoryModel> immutableArray = ImmutableArray.Create<ScheduleRepositoryModel>();

            foreach (var schedule in schedules)
            {
                immutableArray = immutableArray.Add(schedule.ToRepository());
            }

            return immutableArray;
        }

        public static IEnumerable<ScheduleAgregateModel> ToListAgregate(this IEnumerable<ScheduleRepositoryModel> schedules)
        {
            ImmutableArray<ScheduleAgregateModel> immutableArray = ImmutableArray.Create<ScheduleAgregateModel>();

            foreach (var schedule in schedules)
            {
                immutableArray = immutableArray.Add(schedule.ToAgregate());
            }

            return immutableArray;
        }
    }
}
