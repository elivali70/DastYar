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

        public OrderProposalService(IOrderProposalQueryRepository orderProposalQueryRepository)
        {
            _orderProposalQueryRepository = orderProposalQueryRepository;
        }

        public async Task<OrderProposalDto?> GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await _orderProposalQueryRepository.GetByOrderId(orderId, cancellationToken);
        }

        public async Task<List<OrderProposalDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _orderProposalQueryRepository.GetAll(cancellationToken);
        }
    }
}
