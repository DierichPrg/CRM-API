using System.Collections.Immutable;
using Data.ModelsCrmClient;
using Domain.Client.ScheduleAgregate.Data;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Utils;
using Customer = Domain.Client.CustomerAgregate.Data.Customer;
using Schedule = Domain.Client.ScheduleAgregate.Data.Schedule;

namespace Domain.Client.ScheduleAgregate
{
    public class ScheduleDomain : IDomainClientAgregate<Schedule, ReturnFlag>
    {
        private readonly CrmClientContext context;
        private readonly IDomainAgregate<Customer, CustomerAgregate.Data.ReturnFlag> customerDomain;

        public ScheduleDomain(CrmClientContext context, IDomainAgregate<Customer, CustomerAgregate.Data.ReturnFlag> customerDomain)
        {
            this.context = context;
            this.customerDomain = customerDomain;
        }
        public async Task<Result<Schedule, ReturnFlag>> InsertAsync(Schedule value)
        {
            if (await this.ExistsAsync(value))
                return ReturnFlag.AlreadyExist;

            if (!await this.customerDomain.ExistsAsync(value.IdCustomer))
                return ReturnFlag.InvalidData;

            var entity = value.ToRepository();

            await this.context.Schedules.AddAsync(entity);

            await this.context.SaveChangesAsync();

            return entity.ToAgregate();
        }

        public async Task<Result<IEnumerable<Schedule>, ReturnFlag>> InsertRangeAsync(IEnumerable<Schedule> values)
        {
            foreach (var value in values)
            {
                if (await this.ExistsAsync(value))
                    return ReturnFlag.AlreadyExist;

                if (!await this.customerDomain.ExistsAsync(value.IdCustomer))
                    return ReturnFlag.InvalidData;
            }

            var listRepository = values.ToListRepository();

            await this.context.Schedules.AddRangeAsync(listRepository);
            await this.context.SaveChangesAsync();

            ImmutableArray<Schedule> immutableArray = ImmutableArray.Create<Schedule>().AddRange(listRepository.ToListAgregate());

            return immutableArray;
        }

        public async Task<Result<Schedule, ReturnFlag>> UpdateAsync(Schedule value)
        {
            if (!await this.ExistsAsync(value))
                return ReturnFlag.InvalidData;

            if (!await this.customerDomain.ExistsAsync(value.IdCustomer))
                return ReturnFlag.InvalidData;

            var entity = value.ToRepository();

            this.context.Entry(entity).State = EntityState.Modified;

            await this.context.SaveChangesAsync();

            return entity.ToAgregate();
        }

        public async Task<Result<IEnumerable<Schedule>, ReturnFlag>> UpdateRangeAsync(IEnumerable<Schedule> values)
        {
            foreach (var value in values)
            {
                if (!await this.ExistsAsync(value))
                    return ReturnFlag.InvalidData;

                if (!await this.customerDomain.ExistsAsync(value.IdCustomer))
                    return ReturnFlag.InvalidData;
            }

            var listRepository = values.ToListRepository();

            this.context.Schedules.UpdateRange(listRepository);
            await this.context.SaveChangesAsync();

            ImmutableArray<Schedule> immutableArray = ImmutableArray.Create<Schedule>().AddRange(listRepository.ToListAgregate());

            return immutableArray;
        }

        public async Task<Result<bool, ReturnFlag>> DeleteAsync(Schedule value)
        {
            if (!await this.ExistsAsync(value))
                return ReturnFlag.InvalidOperation;

            if (!await this.customerDomain.ExistsAsync(value.IdCustomer))
                return ReturnFlag.InvalidData;

            var entity = await this.context.Schedules.FirstAsync(x => x.Id == value.Id);

            this.context.Schedules.Remove(entity);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<Result<bool, ReturnFlag>> DeleteRangeAsync(IEnumerable<Schedule> values)
        {

            foreach (var value in values)
            {
                if (!await this.ExistsAsync(value))
                    return ReturnFlag.InvalidOperation;

                if (!await this.customerDomain.ExistsAsync(value.IdCustomer))
                    return ReturnFlag.InvalidData;
            }

            this.context.Schedules.RemoveRange(values.ToListRepository());
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<Result<Schedule, ReturnFlag>> SelectAsync(int id)
        {
            if (!await this.ExistsAsync(id))
                return ReturnFlag.NoExists;

            return (await this.context.Schedules.AsNoTracking().FirstAsync(x => x.Id == id)).ToAgregate();
        }

        public async Task<Result<IEnumerable<Schedule>, ReturnFlag>> SelectAllAsync()
        {
            var listAsync = await this.context.Schedules.AsNoTracking().ToListAsync();

            return ImmutableArray.Create<Schedule>().AddRange(listAsync.ToListAgregate()); ;
        }

        public async Task<bool> ExistsAsync(Schedule value)
        {
            return await this.context.Schedules.AnyAsync(x => x.Id == value.Id || x.Date == value.Date);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await this.context.Schedules.AnyAsync(x => x.Id == id);
        }
    }
}
