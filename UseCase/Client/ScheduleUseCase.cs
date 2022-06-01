using System.Collections.Immutable;
using Domain.Interfaces;
using UseCase.Contract.Client.ScheduleContract;
using UseCase.Interfaces.Client;
using Utils;
using ScheduleDomainModel = Domain.Client.ScheduleAgregate.Data.Schedule;
using ScheduleDomainReturnFlag = Domain.Client.ScheduleAgregate.Data.ReturnFlag;

namespace UseCase.Client
{
    // same as CustomerUseCase
    public class ScheduleUseCase : IScheduleUseCase
    {
        private readonly IDomainClientAgregate<ScheduleDomainModel, ScheduleDomainReturnFlag> scheduleService;

        public ScheduleUseCase(IDomainClientAgregate<ScheduleDomainModel, ScheduleDomainReturnFlag> scheduleService)
        {
            this.scheduleService = scheduleService;
        }

        public async Task<Result<Schedule, ReturnFlag>> CreateScheduleAsync(Schedule schedule)
        {
            var insertAsync = await this.scheduleService.InsertAsync(schedule.ToDomain());

            if (!insertAsync.TryGetValue(out var scheduleDomain, out var returnFlag))
                return returnFlag.ToContract();

            return scheduleDomain.ToContract();
        }

        public async Task<Result<Schedule, ReturnFlag>> UpdateScheduleAsync(Schedule schedule)
        {
            var updateAsync = await this.scheduleService.UpdateAsync(schedule.ToDomain());

            if (!updateAsync.TryGetValue(out var scheduleDomain, out var returnFlag))
                return returnFlag.ToContract();

            return scheduleDomain.ToContract();
        }

        public async Task<Result<Schedule, ReturnFlag>> SelectScheduleAsync(Schedule schedule)
        {
            var selectAsync = await this.scheduleService.SelectAsync(schedule.Id);

            if (!selectAsync.TryGetValue(out var scheduleDomain, out var returnFlag))
                return returnFlag.ToContract();

            return scheduleDomain.ToContract();
        }

        public async Task<Result<Schedule, ReturnFlag>> SelectScheduleAsync(int id)
        {
            var selectAsync = await this.scheduleService.SelectAsync(id);

            if (!selectAsync.TryGetValue(out var scheduleDomain, out var returnFlag))
                return returnFlag.ToContract();

            return scheduleDomain.ToContract();
        }

        public async Task<Result<IEnumerable<Schedule>, ReturnFlag>> SelectAllSchedulesAsync()
        {
            var selectAllAsync = await this.scheduleService.SelectAllAsync();

            if (!selectAllAsync.TryGetValue(out var domainSchedules, out var returnFlag))
                return returnFlag.ToContract();

            return ImmutableArray.Create<Schedule>().AddRange(domainSchedules.ToListContract());
        }

        public async Task<Result<bool, ReturnFlag>> DeleteScheduleAsync(Schedule schedule)
        {
            var deleteAsync = await this.scheduleService.DeleteAsync(schedule.ToDomain());

            return deleteAsync.IsOk;
        }
    }
}
