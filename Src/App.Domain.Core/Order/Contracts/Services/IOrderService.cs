using App.Domain.Core.Order.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Order.Contracts.Services
{
    public interface IOrderService
    {
        Task<int> Add(OrderDto orderDto, CancellationToken cancellationToken);
        Task Update(OrderDto orderDto, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<List<OrderDto>> GetAll(CancellationToken cancellationToken);
        Task<OrderDto?> Get(int id, CancellationToken cancellationToken);
        Task<List<OrderDto?>> GetAllForCustomer(int customerId, CancellationToken cancellationToken);
        Task<List<OrderDto?>> GetAccepted(int customerId, CancellationToken cancellationToken);
        Task<OrderDto?> GetForCustomer(int customerId, int orderId, CancellationToken cancellationToken);
        Task<List<OrderDto>?> GetAll(string? keyWord, CancellationToken cancellationToken);
        Task<int> Set(int CustomerId, string Discription, DateTime PrefferedDateTime, int ServiceId, CancellationToken cancellationToken);
    }
}
