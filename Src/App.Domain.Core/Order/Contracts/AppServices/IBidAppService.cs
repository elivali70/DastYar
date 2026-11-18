using App.Domain.Core.Order.Dtos;
using App.Domain.Core.Order.Enities;
using App.Domain.Core.User.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace App.Domain.Core.Order.Contracts.AppServices
{
    public interface IBidAppService
    {
        Task<List<BidDto>> GetAll(CancellationToken cancellationToken);
        Task<List<BidDto>> GetMyBids(int expertId,CancellationToken cancellationToken);
        Task<List<BidDto>> Search(string keyWord, CancellationToken cancellationToken);
        Task Delete(int  id,CancellationToken cancellationToken);
        Task<BidDto> Get(int id, CancellationToken cancellationToken);
        Task Update(BidDto bidDto, CancellationToken cancellationToken);
        Task<List<BidDto>> GetAllByOrderId(int orderId, CancellationToken cancellationToken);
        Task DeleteSuggestedBid(int expertId,int bidId,int orderId,CancellationToken cancellationToken);
    }
}
