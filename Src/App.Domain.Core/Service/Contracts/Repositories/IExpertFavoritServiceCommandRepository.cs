using App.Domain.Core.Service.Dtos;

namespace App.Domain.Core.Service.Contracts.Repositories;

public interface IExpertFavoritServiceCommandRepository
{
    Task<int> Add(ExpertFavoritServiceDto expertFavoritServiceDto, CancellationToken cancellationToken);
    Task Update(ExpertFavoritServiceDto expertFavoritServiceDto, CancellationToken cancellationToken);
    Task Delete(int id, CancellationToken cancellationToken);
}
