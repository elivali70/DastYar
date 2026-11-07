using App.Domain.Core.Order.Dtos;

public interface IOrderProposalQueryRepository
{
    Task<List<OrderProposalDto>> GetAll(CancellationToken cancellationToken);
    Task<OrderProposalDto?> Get(int id, CancellationToken cancellationToken);
    Task<OrderProposalDto?> GetByOrderId(int orderId, CancellationToken cancellationToken);
}