using App.Domain.Core.Service.Dtos;

namespace App.Domain.Core.Service.Contracts.Repositories;

public interface IServiceQueryRepository
{
    Task<List<ServiceDto>> GetAll(CancellationToken cancellationToken);
    Task<ServiceDto?> Get(int id, CancellationToken cancellationToken);
    Task<ServiceDto?> Get(string name, CancellationToken cancellationToken);
}
