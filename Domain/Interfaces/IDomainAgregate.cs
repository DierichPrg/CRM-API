using Utils;

namespace Domain.Interfaces
{
    public interface IDomainAgregate<TAgregateModel, TFlag>
    {
        public Task<Result<TAgregateModel, TFlag>> InsertAsync(TAgregateModel value);
        public Task<Result<IEnumerable<TAgregateModel>, TFlag>> InsertRangeAsync(IEnumerable<TAgregateModel> values);
        public Task<Result<TAgregateModel, TFlag>> UpdateAsync(TAgregateModel value);
        public Task<Result<IEnumerable<TAgregateModel>, TFlag>> UpdateRangeAsync(IEnumerable<TAgregateModel> values);
        public Task<Result<bool, TFlag>> DeleteAsync(TAgregateModel value);
        public Task<Result<bool, TFlag>> DeleteRangeAsync(IEnumerable<TAgregateModel> values);
        public Task<Result<TAgregateModel, TFlag>> SelectAsync(int id);
        public Task<Result<IEnumerable<TAgregateModel>, TFlag>> SelectAllAsync();
        public Task<bool> ExistsAsync(TAgregateModel value);
        public Task<bool> ExistsAsync(int id);

    }
}
