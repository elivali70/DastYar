using App.Domain.Core.Order.Dtos;

public interface IOrderQueryRepository
{
    Task<List<OrderDto>> GetAll(CancellationToken cancellationToken);
    Task<OrderDto?> Get(int id, CancellationToken cancellationToken);
    Task<List<OrderDto>?> Search(string? keyWord, CancellationToken cancellationToken);
    Task<List<OrderDto?>> GetAllForCustomer(int customerId, CancellationToken cancellationToken);
    Task<OrderDto?> GetForCustomer(int customerId, int orderId, CancellationToken cancellationToken);
    Task<List<OrderDto?>> GetAccepted(int customerId, CancellationToken cancellationToken);
    Task<List<OpenOrderOutPutDto>> GetOpenOrders(int expertId, int serviceId, CancellationToken cancellationToken);
}
