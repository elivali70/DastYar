using App.Domain.Core.Service.Dtos;

namespace App.Domain.Core.Service.Contracts.Repositories;

public interface IServiceCommandRepository
{
    Task<int> Add(ServiceDto serviceDto, CancellationToken cancellationToken);
    Task Update(ServiceDto serviceDto, CancellationToken cancellationToken);
    Task Delete(int id, CancellationToken cancellationToken);
}
