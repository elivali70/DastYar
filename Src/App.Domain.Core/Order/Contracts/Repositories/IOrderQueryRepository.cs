using App.Domain.Core.Order.Dtos;

public interface IOrderQueryRepository
{
Task<List<OrderDto>> GetAll(CancellationToken cancellationToken);
    Task<OrderDto?> Get(int  id,CancellationToken cancellationToken);
    Task<List<OrderDto>?> GetAll(string? keyWord, CancellationToken cancellationToken);
}
