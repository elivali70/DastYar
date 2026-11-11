using App.Domain.Core.Order.Contracts.Services;
using App.Domain.Core.Order.Dtos;
using App.Domain.Core.Order.Enums;
using Microsoft.AspNetCore.Identity;
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

        public async Task<List<OrderDto?>> GetAccepted(int customerId, CancellationToken cancellationToken)
        {
            return await _orderQueryRepository.GetAccepted(customerId, cancellationToken);
        }

        public async Task<List<OrderDto>?> GetAll(string? keyWord, CancellationToken cancellationToken)
        {
            return await _orderQueryRepository.GetAll(keyWord, cancellationToken);
        }

        public async Task<List<OrderDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _orderQueryRepository.GetAll(cancellationToken);
        }

        public async Task<List<OrderDto?>> GetAllForCustomer(int customerId, CancellationToken cancellationToken)
        {
            return await _orderQueryRepository.GetAllForCustomer(customerId, cancellationToken);
        }

        public async Task<OrderDto?> GetForCustomer(int customerId, int orderId, CancellationToken cancellationToken)
        {
            return await _orderQueryRepository.GetForCustomer(customerId, orderId, cancellationToken);
        }

        public async Task<int> Set(int CustomerId, string Discription, DateTime PrefferedDateTime, int ServiceId, CancellationToken cancellationToken)
        {
            var orderDto= new OrderDto();
            orderDto.CustomerId=CustomerId;
            orderDto.BasePrice = (int)(OrderBasePriceEnum.BasePrice);
            orderDto.ServiceId=ServiceId;
            orderDto.Discription= Discription;
            orderDto.PrefferedDateTime= PrefferedDateTime;
            orderDto.ExpertId = null;
            orderDto.OrderStatus=OrderStatusEnum.WaitingForProposals;
            return await _orderCommandRepository.Add(orderDto, cancellationToken);
        }

        public async Task Update(OrderDto orderDto, CancellationToken cancellationToken)
        {
            await _orderCommandRepository.Update(orderDto, cancellationToken);
        }
    }
}
