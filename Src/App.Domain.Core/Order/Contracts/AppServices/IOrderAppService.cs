using App.Domain.Core.Order.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.Order.Contracts.AppServices
{
    public interface IOrderAppService
    {
        Task<int> Add(OrderDto orderDto, CancellationToken cancellationToken);
        Task Update(OrderDto orderDto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<List<OrderDto>> GetAll(CancellationToken cancellationToken);
        Task<OrderDto?> Get(int id, CancellationToken cancellationToken);
        Task<List<OrderDto?>> GetAll(string? keyWord, CancellationToken cancellationToken);
    }
}
