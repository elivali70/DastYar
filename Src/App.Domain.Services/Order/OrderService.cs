using App.Domain.Core.Order.Contracts.Services;
using App.Domain.Core.Order.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Services.Order
{
    public class OrderService:IOrderService
    {
        private readonly IOrderCommandRepository _orderCommandRepository;
        private readonly IOrderQueryRepository _orderQueryRepository;

        public OrderService(IOrderCommandRepository orderCommandRepository,IOrderQueryRepository orderQueryRepository)
        {
            _orderCommandRepository = orderCommandRepository;
            _orderQueryRepository = orderQueryRepository;
        }

        public async Task<int> Add(OrderDto orderDto, CancellationToken cancellationToken)
        {
           return await _orderCommandRepository.Add(orderDto, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _orderCommandRepository.Delete(id, cancellationToken);
        }

        public async Task<OrderDto?> Get(int id, CancellationToken cancellationToken)
        {
            return await _orderQueryRepository.Get(id, cancellationToken);
        }

        public async Task<List<OrderDto>?> GetAll(string? keyWord, CancellationToken cancellationToken)
        {
            return await _orderQueryRepository.GetAll(keyWord, cancellationToken);
        }

        public Task<List<OrderDto>> GetAll(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public async Task Update(OrderDto orderDto, CancellationToken cancellationToken)
        {
            await _orderCommandRepository.Update(orderDto, cancellationToken);
        }
    }
}
