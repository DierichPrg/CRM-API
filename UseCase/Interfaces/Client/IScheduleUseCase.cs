using UseCase.Contract.Client.ScheduleContract;
using Utils;

namespace UseCase.Interfaces.Client
{
    public interface IScheduleUseCase
    {
        Task<Result<Schedule, ReturnFlag>> CreateScheduleAsync(Schedule schedule);
        Task<Result<Schedule, ReturnFlag>> UpdateScheduleAsync(Schedule schedule);
        Task<Result<Schedule, ReturnFlag>> SelectScheduleAsync(Schedule schedule);
        Task<Result<Schedule, ReturnFlag>> SelectScheduleAsync(int id);
        Task<Result<IEnumerable<Schedule>, ReturnFlag>> SelectAllSchedulesAsync();
        Task<Result<bool, ReturnFlag>> DeleteScheduleAsync(Schedule schedule);
    }
}
