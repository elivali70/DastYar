using App.Domain.Core.Order.Dtos;

public interface IBidQueryRepository
{
    Task<List<BidDto>> GetAll(CancellationToken cancellationToken);
    Task<BidDto?> Get(int id, CancellationToken cancellationToken);
    Task<List<BidDto>> GetAll(string? keyWord, CancellationToken cancellationToken);
}
