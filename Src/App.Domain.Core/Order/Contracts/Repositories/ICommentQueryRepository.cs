using App.Domain.Core.Order.Enities;

namespace App.Domain.Core.Order.Contracts.Repositories;

public interface ICommentQueryRepository
{
    Task<List<CommentDto>> GetAll(CancellationToken cancellationToken);
    Task<CommentDto?> Get(int orderId, CancellationToken cancellationToken);
}
