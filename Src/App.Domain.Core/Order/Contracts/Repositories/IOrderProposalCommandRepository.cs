using App.Domain.Core.Order.Dtos;

public interface IOrderProposalCommandRepository
{
    Task<int> Add(OrderProposalDto orderProposalDto,CancellationToken cancellationToken);
    Task Update(OrderProposalDto orderProposalDto, CancellationToken cancellationToken);
    Task Delete(int id,CancellationToken cancellationToken);
}
