using App.Domain.Core.Order.Dtos;

public interface IOrderFileCommandRepository
{
    Task<int> Add(OrderFileDto orderFileDto,CancellationToken cancellationToken);
    Task Update(OrderFileDto orderFileDto,CancellationToken cancellationToken) ;
    Task Delete(int id,CancellationToken cancellationToken);
}