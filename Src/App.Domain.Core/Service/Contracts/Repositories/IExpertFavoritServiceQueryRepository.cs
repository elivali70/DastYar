using App.Domain.Core.Service.Dtos;

namespace App.Domain.Core.Service.Contracts.Repositories;

public interface IExpertFavoritServiceQueryRepository
{
    Task<List<ExpertFavoritServiceDto>> GetAll(CancellationToken cancellationToken);
    Task<ExpertFavoritServiceDto?> Get(int id, CancellationToken cancellationToken);
}
