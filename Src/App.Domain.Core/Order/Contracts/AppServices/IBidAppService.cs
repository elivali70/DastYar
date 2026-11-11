using App.Domain.Core.Order.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.Order.Contracts.AppServices
{
    public interface IBidAppService
    {
        Task<List<BidDto>> GetAll(CancellationToken cancellationToken);
        Task<List<BidDto>> Search(string keyWord, CancellationToken cancellationToken);
        Task Delete(int  id,CancellationToken cancellationToken);
        Task<BidDto> Get(int id, CancellationToken cancellationToken);
        Task Update(BidDto bidDto, CancellationToken cancellationToken);
        Task<List<BidDto>> GetAllByOrderId(int orderId, CancellationToken cancellationToken);
    }
}
