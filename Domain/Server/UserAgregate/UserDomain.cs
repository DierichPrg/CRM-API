using System.Collections.Immutable;
using Data.ModelsCrm;
using Domain.Interfaces;
using Domain.Server.UserAgregate.Data;
using Microsoft.EntityFrameworkCore;
using Utils;
using User = Domain.Server.UserAgregate.Data.User;

namespace Domain.Server.UserAgregate
{
    public class UserDomain : IDomainServerAgregate<User, ReturnFlag>, IDomainLogin
    {
        private readonly CrmContext context;

        public UserDomain(CrmContext context)
        {
            this.context = context;
        }

        public async Task<Result<User, ReturnFlag>> InsertAsync(User value)
        {
            if (await this.ExistsAsync(value))
                return ReturnFlag.AlreadyExist;

            var entity = value.ToRepository();

            await this.context.Users.AddAsync(entity);

            await this.context.SaveChangesAsync();

            return entity.ToAgregate();
        }

        public async Task<Result<IEnumerable<User>, ReturnFlag>> InsertRangeAsync(IEnumerable<User> values)
        {
            foreach (var value in values)
            {
                if (await this.ExistsAsync(value))
                    return ReturnFlag.AlreadyExist;
            }

            var listRepository = values.ToListRepository();

            await this.context.Users.AddRangeAsync(listRepository);
            await this.context.SaveChangesAsync();

            ImmutableArray<User> immutableArray = ImmutableArray.Create<User>().AddRange(listRepository.ToListAgregate());

            return immutableArray;
        }

        public async Task<Result<User, ReturnFlag>> UpdateAsync(User value)
        {
            if (!await this.ExistsAsync(value))
                return ReturnFlag.InvalidData;

            var entity = value.ToRepository();

            this.context.Entry(entity).State = EntityState.Modified;

            await this.context.SaveChangesAsync();

            return entity.ToAgregate();
        }

        public async Task<Result<IEnumerable<User>, ReturnFlag>> UpdateRangeAsync(IEnumerable<User> values)
        {
            foreach (var value in values)
            {
                if (!await this.ExistsAsync(value))
                    return ReturnFlag.InvalidData;
            }

            var listRepository = values.ToListRepository();

            this.context.Users.UpdateRange(listRepository);
            await this.context.SaveChangesAsync();

            ImmutableArray<User> immutableArray = ImmutableArray.Create<User>().AddRange(listRepository.ToListAgregate());

            return immutableArray;
        }

        public async Task<Result<bool, ReturnFlag>> DeleteAsync(User value)
        {
            if (!await this.ExistsAsync(value))
                return ReturnFlag.InvalidOperation;

            var entity = await this.context.Users.FirstAsync(x => x.Id == value.Id);

            this.context.Users.Remove(entity);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<Result<bool, ReturnFlag>> DeleteRangeAsync(IEnumerable<User> values)
        {

            foreach (var value in values)
            {
                if (!await this.ExistsAsync(value))
                    return ReturnFlag.InvalidOperation;
            }

            this.context.Users.RemoveRange(values.ToListRepository());
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<Result<User, ReturnFlag>> SelectAsync(int id)
        {
            if (!await this.ExistsAsync(id))
                return ReturnFlag.NoExists;

            return (await this.context.Users.FirstAsync(x => x.Id == id)).ToAgregate();
        }

        public async Task<Result<IEnumerable<User>, ReturnFlag>> SelectAllAsync()
        {
            var listAsync = await this.context.Users.ToListAsync();

            return ImmutableArray.Create<User>().AddRange(listAsync.ToListAgregate()); ;
        }

        public async Task<bool> ExistsAsync(User value)
        {
            return await this.context.Users.AnyAsync(x => x.Id == value.Id || x.Username == value.Username);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await this.context.Users.AnyAsync(x => x.Id == id);
        }

        public async Task<Result<User,bool>> LoginAsync(User value)
        {
            value.Username = value.Username.Trim().ToLower();
            var userLogin = await this.context.Users.FirstOrDefaultAsync(x => x.Username.Equals(value.Username) && x.Password.Equals(value.Password));

            if (userLogin is null)
                return false;

            return userLogin.ToAgregate();
        }
    }
}
