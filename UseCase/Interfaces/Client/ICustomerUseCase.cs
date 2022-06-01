using UseCase.Contract.Client.CustomerContract;
using Utils;

namespace UseCase.Interfaces.Client
{
    public interface ICustomerUseCase
    {
        Task<Result<Customer, ReturnFlag>> CreateCustomerAsync(Customer customer);
        Task<Result<Customer, ReturnFlag>> UpdateCustomerAsync(Customer customer);
        Task<Result<Customer, ReturnFlag>> SelectCustomerAsync(Customer customer);
        Task<Result<Customer, ReturnFlag>> SelectCustomerAsync(int id);
        Task<Result<IEnumerable<Customer>, ReturnFlag>> SelectAllCustomersAsync();
        Task<Result<bool, ReturnFlag>> DeleteCustomerAsync(Customer customer);
    }
}
