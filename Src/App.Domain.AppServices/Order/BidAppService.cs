using App.Domain.Core.Order.Contracts.AppServices;
using App.Domain.Core.Order.Contracts.Services;
using App.Domain.Core.Order.Dtos;
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
    }
}
