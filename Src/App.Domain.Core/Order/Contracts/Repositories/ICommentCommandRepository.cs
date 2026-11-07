using App.Domain.Core.Order.Enities;

namespace App.Domain.Core.Order.Contracts.Repositories;

public interface ICommentCommandRepository
{
    Task<int> Add(CommentDto commentDto, CancellationToken cancellationToken);
    Task Delete(int id, CancellationToken cancellationToken);
    Task Update(CommentDto commentDto, CancellationToken cancellationToken);
}