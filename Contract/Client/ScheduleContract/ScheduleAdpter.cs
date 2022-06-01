using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleDomainModel = Domain.Client.ScheduleAgregate.Data.Schedule;
using ScheduleContractModel = Contract.Client.ScheduleContract.Schedule;

namespace Contract.Client.ScheduleContract
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
    }
}
