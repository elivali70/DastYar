using App.Domain.Core.Order.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.Order.Contracts.AppServices
{
    public interface IOrderProposalAppService
    {
        Task<List<OrderProposalDto>> GetAll(CancellationToken cancellationToken);
        Task<OrderProposalDto?> GetByOrderId(int orderId, CancellationToken cancellationToken);
    }
}
