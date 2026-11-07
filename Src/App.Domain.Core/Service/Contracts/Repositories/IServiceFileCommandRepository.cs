using App.Domain.Core.Service.Dtos;

namespace App.Domain.Core.Service.Contracts.Repositories;

public interface IServiceFileCommandRepository
{
    Task<int> Add(ServiceFileDto serviceFileDto, CancellationToken cancellationToken);
    Task Update(ServiceFileDto serviceFileDto, CancellationToken cancellationToken);
    Task Delete(int id, CancellationToken cancellationToken);
}

