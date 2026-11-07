using App.Domain.Core.Order.Contracts.AppServices;
using App.Domain.Core.Order.Contracts.Services;
using App.Domain.Core.Order.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.AppServices.Order
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IOrderService _orderService;

        public OrderAppService(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<int> Add(OrderDto orderDto, CancellationToken cancellationToken)
        {
            return await _orderService.Add(orderDto, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _orderService.Delete(id, cancellationToken);
        }

        public async Task<OrderDto?> Get(int id, CancellationToken cancellationToken)
        {
            return await _orderService.Get(id, cancellationToken);
        }
      
        public async Task<List<OrderDto>?> GetAll(string? keyWord, CancellationToken cancellationToken)
        {
            return await _orderService.GetAll(keyWord, cancellationToken);
        }

        public Task<List<OrderDto>> GetAll(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

      

        public async Task Update(OrderDto orderDto, CancellationToken cancellationToken)
        {
            await _orderService.Update(orderDto, cancellationToken);
        }
    }
}
