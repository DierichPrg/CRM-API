using System.Collections.Immutable;
using Data.ModelsCrm;
using Domain.Interfaces;
using Domain.Server.CompanyAgregate.Data;
using Microsoft.EntityFrameworkCore;
using Utils;
using Company = Domain.Server.CompanyAgregate.Data.Company;

namespace Domain.Server.CompanyAgregate
{
    public class CompanyDomain : IDomainServerAgregate<Company, ReturnFlag>
    {
        private readonly CrmContext context;

        public CompanyDomain(CrmContext context)
        {
            this.context = context;
        }

        public async Task<Result<Company, ReturnFlag>> InsertAsync(Company value)
        {
            if (await this.ExistsAsync(value))
                return ReturnFlag.AlreadyExist;

            var entity = value.ToRepository();

            await this.context.Companies.AddAsync(entity);

            await this.context.SaveChangesAsync();

            return entity.ToAgregate();
        }

        public async Task<Result<IEnumerable<Company>, ReturnFlag>> InsertRangeAsync(IEnumerable<Company> values)
        {
            foreach (var value in values)
            {
                if (await this.ExistsAsync(value))
                    return ReturnFlag.AlreadyExist;
            }

            var listRepository = values.ToListRepository();

            await this.context.Companies.AddRangeAsync(listRepository);
            await this.context.SaveChangesAsync();

            ImmutableArray<Company> immutableArray = ImmutableArray.Create<Company>().AddRange(listRepository.ToListAgregate());

            return immutableArray;
        }

        public async Task<Result<Company, ReturnFlag>> UpdateAsync(Company value)
        {
            if (!await this.ExistsAsync(value))
                return ReturnFlag.InvalidData;

            var entity = value.ToRepository();

            this.context.Entry(entity).State = EntityState.Modified;

            await this.context.SaveChangesAsync();

            return entity.ToAgregate();
        }

        public async Task<Result<IEnumerable<Company>, ReturnFlag>> UpdateRangeAsync(IEnumerable<Company> values)
        {
            foreach (var value in values)
            {
                if (!await this.ExistsAsync(value))
                    return ReturnFlag.InvalidData;
            }

            var listRepository = values.ToListRepository();

            this.context.Companies.UpdateRange(listRepository);
            await this.context.SaveChangesAsync();

            ImmutableArray<Company> immutableArray = ImmutableArray.Create<Company>().AddRange(listRepository.ToListAgregate());

            return immutableArray;
        }

        public async Task<Result<bool, ReturnFlag>> DeleteAsync(Company value)
        {
            if (!await this.ExistsAsync(value))
                return ReturnFlag.InvalidOperation;

            var entity = await this.context.Companies.Include(x => x.Users).FirstAsync(x => x.Id == value.Id);

            if (entity.Users.Count > 0)
                return ReturnFlag.HasDependency;

            this.context.Companies.Remove(entity);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<Result<bool, ReturnFlag>> DeleteRangeAsync(IEnumerable<Company> values)
        {

            foreach (var value in values)
            {
                if (!await this.ExistsAsync(value))
                    return ReturnFlag.InvalidOperation;

                var entity = await this.context.Companies.Include(x => x.Users).FirstAsync(x => x.Id == value.Id);

                if (entity.Users.Count > 0)
                    return ReturnFlag.HasDependency;
            }

            this.context.Companies.RemoveRange(values.ToListRepository());
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<Result<Company, ReturnFlag>> SelectAsync(int id)
        {
            if (!await this.ExistsAsync(id))
                return ReturnFlag.NoExists;

            return (await this.context.Companies.FirstAsync(x => x.Id == id)).ToAgregate();
        }

        public async Task<Result<IEnumerable<Company>, ReturnFlag>> SelectAllAsync()
        {
            var listAsync = await this.context.Companies.ToListAsync();

            return ImmutableArray.Create<Company>().AddRange(listAsync.ToListAgregate()); ;
        }

        public async Task<bool> ExistsAsync(Company value)
        {
            return await this.context.Companies.AnyAsync(x => x.Id == value.Id || x.Identification == value.Identification);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await this.context.Companies.AnyAsync(x => x.Id == id);
        }
    }
}
