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
        private readonly IOrderProposalService _orderProposalService;

        public BidAppService(IBidService bidService,
            IOrderProposalService orderProposalService)
        {
            _bidService = bidService;
            _orderProposalService = orderProposalService;
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

            var bidDto = new BidDto()
            {
                CreatedAt = bid.CreatedAt,
                ExpertId = bid.ExpertId,
                Id = bid.Id,
                OrderId = bid.OrderId,
                ProposalStatus = ProposalStatusEnum.Withdrawn,
                SuggestedPrice = bid.SuggestedPrice,
            };
            await _bidService.Update(bidDto, cancellationToken);
            var orderProposal= await _orderProposalService.GetByOrderId(orderId, cancellationToken);
            var orderProposalDto = new OrderProposalDto()
            {
                CreatedAt = orderProposal.CreatedAt,
                Discription = orderProposal.Discription,
                DurationTime = orderProposal.DurationTime,
                ExpertId = orderProposal.ExpertId,
                OrderId = orderProposal.OrderId,
                StatrtedDateTime = orderProposal.StatrtedDateTime,
                Id = orderProposal.Id,
                ProposalStatus = ProposalStatusEnum.Withdrawn,
                SuggestedPrice = orderProposal.SuggestedPrice,
            };
            await _orderProposalService.Update(orderProposalDto, cancellationToken);

        }
    }
}
