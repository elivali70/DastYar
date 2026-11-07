using App.Domain.Core.Order.Dtos;

public interface IOrderFileQueryRepository
{
    Task<List<OrderFileDto>> GetAll(CancellationToken cancellationToken);
    Task<OrderFileDto?> Get(int id,CancellationToken cancellationToken);
}
