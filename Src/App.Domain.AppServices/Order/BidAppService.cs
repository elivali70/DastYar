using App.Domain.Core.Order.Contracts.AppServices;
using App.Domain.Core.Order.Contracts.Services;
using App.Domain.Core.Order.Dtos;
using App.Domain.Core.Order.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.AppServices.Order
{
    public class BidAppService : IBidAppService
    {
        private readonly IBidService _bidService;

        public BidAppService(IBidService bidService)
        {
            _bidService = bidService;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
           await _bidService.Delete(id, cancellationToken);
        }

        public async Task<List<BidDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _bidService.GetAll(cancellationToken);
        }
        public async Task<BidDto> Get(int id,CancellationToken cancellationToken)
        {
            return await _bidService.Get(id,cancellationToken);
        }
        public async Task<List<BidDto>> Search(string keyWord, CancellationToken cancellationToken)
        {
            return await _bidService.Search(keyWord, cancellationToken);
        }
        public async Task Update(BidDto bidDto, CancellationToken cancellationToken)
        {
             await _bidService.Update(bidDto, cancellationToken);
        }

        public async Task<List<BidDto>> GetAllByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await _bidService.GetAllByOrderId(orderId, cancellationToken);
        }

        public async Task<List<BidDto>> GetMyBids(int expertId, CancellationToken cancellationToken)
        {
            return await _bidService.GetMyBids(expertId, cancellationToken);
        }

        public async Task DeleteSuggestedBid(int expertId, int bidId, int orderId,CancellationToken cancellationToken)
        {
            var bid = await _bidService.Get(bidId, cancellationToken);
            if (bid == null)
                throw new Exception("Bid does not exist.");
            if (bid.ExpertId != expertId)
                throw new UnauthorizedAccessException( "You cannot withdraw another expert's bid.");
            if (bid.OrderId != orderId)
                throw new Exception("Invalid order.");
            if (bid.ProposalStatus != ProposalStatusEnum.Pending)
                throw new Exception("Only pending bids can be withdrawn.");

            bid.ProposalStatus = ProposalStatusEnum.Withdrawn;
            await _bidService.Update(bid, cancellationToken);

        }
    }
}
