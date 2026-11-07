using App.Domain.Core.Service.Dtos;

namespace App.Domain.Core.Service.Contracts.Repositories;

public interface IServiceFileQueryRepository
{
    Task<List<ServiceFileDto>> GetAll(CancellationToken cancellationToken);
    Task<ServiceFileDto?> Get(int id, CancellationToken cancellationToken);
}

