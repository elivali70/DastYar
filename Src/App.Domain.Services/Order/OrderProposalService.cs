using App.Domain.Core.Order.Contracts.Services;
using App.Domain.Core.Order.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Services.Order
{
    public class OrderProposalService : IOrderProposalService
    {
        private readonly IOrderProposalQueryRepository _orderProposalQueryRepository;
        private readonly IOrderProposalCommandRepository _orderProposalCommandRepository;

        public OrderProposalService(IOrderProposalQueryRepository orderProposalQueryRepository,
            IOrderProposalCommandRepository orderProposalCommandRepository)
        {
            _orderProposalQueryRepository = orderProposalQueryRepository;
            _orderProposalCommandRepository = orderProposalCommandRepository;
        }

        public async Task<OrderProposalDto?> GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await _orderProposalQueryRepository.GetByOrderId(orderId, cancellationToken);
        }

        public async Task<List<OrderProposalDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _orderProposalQueryRepository.GetAll(cancellationToken);
        }

        public async Task Update(OrderProposalDto orderProposalDto, CancellationToken cancellationToken)
        {
            await _orderProposalCommandRepository.Update(orderProposalDto, cancellationToken);
        }
    }
}
