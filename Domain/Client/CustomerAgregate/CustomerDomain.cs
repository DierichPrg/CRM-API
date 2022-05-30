using System.Collections.Immutable;
using Data.ModelsCrmClient;
using Domain.Client.CustomerAgregate.Data;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Customer = Domain.Client.CustomerAgregate.Data.Customer;

namespace Domain.Client.CustomerAgregate
{
    public class CustomerDomain : IDomainClientAgregate<Customer, ReturnFlag>
    {
        private readonly CrmClientContext context;

        public CustomerDomain(CrmClientContext context)
        {
            this.context = context;
        }

        public async Task<Result<Customer, ReturnFlag>> InsertAsync(Customer value)
        {
            if (await this.ExistsAsync(value))
                return ReturnFlag.AlreadyExist;

            value.Created = DateTime.Now;

            var entity = value.ToRepository();

            await this.context.Customers.AddAsync(entity);

            await this.context.SaveChangesAsync();

            return entity.ToAgregate();
        }

        public async Task<Result<IEnumerable<Customer>, ReturnFlag>> InsertRangeAsync(IEnumerable<Customer> values)
        {
            foreach (var value in values)
            {
                if (await this.ExistsAsync(value))
                    return ReturnFlag.AlreadyExist;
            }

            foreach (var value in values)
                value.Created = DateTime.Now;

            var listRepository = values.ToListRepository();

            await this.context.Customers.AddRangeAsync(listRepository);
            await this.context.SaveChangesAsync();

            ImmutableArray<Customer> immutableArray = ImmutableArray.Create<Customer>().AddRange(listRepository.ToListAgregate());

            return immutableArray;
        }

        public async Task<Result<Customer, ReturnFlag>> UpdateAsync(Customer value)
        {
            if (!await this.ExistsAsync(value))
                return ReturnFlag.InvalidData;

            var entity = value.ToRepository();

            this.context.Entry(entity).State = EntityState.Modified;

            await this.context.SaveChangesAsync();

            return entity.ToAgregate();
        }

        public async Task<Result<IEnumerable<Customer>, ReturnFlag>> UpdateRangeAsync(IEnumerable<Customer> values)
        {
            foreach (var value in values)
            {
                if (!await this.ExistsAsync(value))
                    return ReturnFlag.InvalidData;
            }

            var listRepository = values.ToListRepository();

            this.context.Customers.UpdateRange(listRepository);
            await this.context.SaveChangesAsync();

            ImmutableArray<Customer> immutableArray = ImmutableArray.Create<Customer>().AddRange(listRepository.ToListAgregate());

            return immutableArray;
        }

        public async Task<Result<bool, ReturnFlag>> DeleteAsync(Customer value)
        {
            if (!await this.ExistsAsync(value))
                return ReturnFlag.InvalidOperation;

            var entity = await this.context.Customers.Include(x => x.Schedules).FirstAsync(x => x.Id == value.Id);

            if (entity.Schedules.Count > 0)
                return ReturnFlag.HasDependency;

            this.context.Customers.Remove(entity);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<Result<bool, ReturnFlag>> DeleteRangeAsync(IEnumerable<Customer> values)
        {

            foreach (var value in values)
            {
                if (!await this.ExistsAsync(value))
                    return ReturnFlag.InvalidOperation;

                var entity = await this.context.Customers.Include(x => x.Schedules).FirstAsync(x => x.Id == value.Id);

                if (entity.Schedules.Count > 0)
                    return ReturnFlag.HasDependency;
            }

            this.context.Customers.RemoveRange(values.ToListRepository());
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<Result<Customer, ReturnFlag>> SelectAsync(int id)
        {
            if (!await this.ExistsAsync(id))
                return ReturnFlag.NoExists;

            return (await this.context.Customers.FirstAsync(x => x.Id == id)).ToAgregate();
        }

        public async Task<Result<IEnumerable<Customer>, ReturnFlag>> SelectAllAsync()
        {
            var listAsync = await this.context.Customers.ToListAsync();

            return ImmutableArray.Create<Customer>().AddRange(listAsync.ToListAgregate()); ;
        }

        public async Task<bool> ExistsAsync(Customer value)
        {
            return await this.context.Customers.AnyAsync(x => x.Id == value.Id || x.Identification == value.Identification);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await this.context.Customers.AnyAsync(x => x.Id == id);
        }
    }
}
