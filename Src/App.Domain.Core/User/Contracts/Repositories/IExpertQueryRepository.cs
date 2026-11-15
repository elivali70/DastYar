using App.Domain.Core.User.Dtos;

namespace App.Domain.Core.User.Contracts.Repositories;

public interface IExpertQueryRepository
{
    Task<List<ExpertDto>> GetAll(CancellationToken cancellationToken);
    Task<ExpertDto?> Get(int userId, CancellationToken cancellationToken);
    Task<ExpertDto?> Get(string userName, CancellationToken cancellationToken);
}
