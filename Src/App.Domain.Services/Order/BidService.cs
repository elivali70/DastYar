using App.Domain.Core.Order.Contracts.Services;
using App.Domain.Core.Order.Dtos;
using App.Domain.Core.Order.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Services.Order
{
    public class BidService : IBidService
    {
        private readonly IBidCommandRepository _bidCommandRepository;
        private readonly IBidQueryRepository _bidQueryRepository;

        public BidService(IBidCommandRepository bidCommandRepository, IBidQueryRepository bidQueryRepository)
        {
            _bidCommandRepository = bidCommandRepository;
            _bidQueryRepository = bidQueryRepository;
        }

        public async Task<int> Add(int expertId, int orderId, int suggestedPrice, CancellationToken cancellationToken)
        {
            var bidDto = new BidDto()
            {
                CreatedAt = DateTime.Now,
                ExpertId = expertId,
                OrderId = orderId,
                SuggestedPrice = suggestedPrice,
                ProposalStatus = ProposalStatusEnum.Pending,
            };
             return await _bidCommandRepository.Add(bidDto, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _bidCommandRepository.Delete(id, cancellationToken);
        }

        public async Task<BidDto> Get(int id, CancellationToken cancellationToken)
        {
            return await _bidQueryRepository.Get(id, cancellationToken);
        }

        public async Task<List<BidDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _bidQueryRepository.GetAll(cancellationToken);
        }

        public async Task<List<BidDto>> GetAllByOrderId(int orderId, CancellationToken cancellationToken)
        {
           return await _bidQueryRepository.GetAllByOrderId(orderId, cancellationToken);
        }

        public async Task<List<BidDto>> GetMyBids(int expertId, CancellationToken cancellationToken)
        {
            return await _bidQueryRepository.GetAll(expertId, cancellationToken);
        }

        public async Task<List<BidDto>> Search(string keyWord, CancellationToken cancellationToken)
        {
            return await _bidQueryRepository.GetAll(keyWord, cancellationToken);
        }

        public async Task Update(BidDto bidDto, CancellationToken cancellationToken)
        {
            await _bidCommandRepository.Update(bidDto, cancellationToken);
        }
    }
}
