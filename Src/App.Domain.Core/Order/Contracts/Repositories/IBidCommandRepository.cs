using App.Domain.Core.Order.Dtos;
using App.Domain.Core.Order.Enums;

public interface IBidCommandRepository
{
    Task<int> Add(BidDto bidDto, CancellationToken cancellationToken);
    Task Delete(int id, CancellationToken cancellationToken);
    Task Update(BidDto bidDto, CancellationToken cancellationToken);

}