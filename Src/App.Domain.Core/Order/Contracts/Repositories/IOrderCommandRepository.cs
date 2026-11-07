using App.Domain.Core.Order.Dtos;

public interface IOrderCommandRepository
{
    Task<int> Add(OrderDto orderDto,CancellationToken cancellationToken);
    Task Update(OrderDto orderDto,CancellationToken cancellationToken);
    Task Delete(int id,CancellationToken cancellationToken);
}