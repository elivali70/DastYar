using App.Domain.Core.User.Dtos;

namespace App.Domain.Core.User.Contracts.Repositories;

public interface IAdminQueryRepository
{
    Task<List<AppUserDto>> GetAll(CancellationToken cancellationToken);
    Task<AppUserDto?> Get(int Id, CancellationToken cancellationToken);
    Task<AppUserDto?> Get(string userName, CancellationToken cancellationToken);
}
