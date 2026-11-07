using App.Domain.Core.User.Dtos;

namespace App.Domain.Core.User.Contracts.Repositories;

public interface ICustomerQueryRepository
{
    Task<List<CustomerDto>> GetAll(CancellationToken cancellationToken);
    Task<CustomerDto?> Get(int userId, CancellationToken cancellationToken);
    Task<CustomerDto?> Get(string userName, CancellationToken cancellationToken);
    Task<CustomerDto?> GetByPhone(string phoneNumber, CancellationToken cancellationToken);
}
