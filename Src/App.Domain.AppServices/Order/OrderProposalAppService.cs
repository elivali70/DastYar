using App.Domain.Core.Order.Contracts.AppServices;
using App.Domain.Core.Order.Contracts.Services;
using App.Domain.Core.Order.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.AppServices.Order
{
    public class OrderProposalAppService : IOrderProposalAppService
    {
        private readonly IOrderProposalService _orderProposalService;

        public OrderProposalAppService(IOrderProposalService orderProposalService)
        {
            _orderProposalService = orderProposalService;
        }
        public async Task<List<OrderProposalDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _orderProposalService.GetAll(cancellationToken);
        }
        public async Task<OrderProposalDto?> GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await _orderProposalService.GetByOrderId(orderId, cancellationToken);
        }
    }
}
