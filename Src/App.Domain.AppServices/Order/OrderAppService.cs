using App.Domain.Core.Order.Contracts.AppServices;
using App.Domain.Core.Order.Contracts.Services;
using App.Domain.Core.Order.Dtos;
using App.Domain.Core.Order.Enums;
using App.Domain.Core.Service.Contracts.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.AppServices.Order
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IOrderService _orderService;
        private readonly IBidService _bidService;
        private readonly IOrderProposalService _orderProposalService;
        private readonly IExpertFavoriteServiceService _expertFavoriteServiceService;

        public OrderAppService(IOrderService orderService,
            IBidService bidService,
            IOrderProposalService orderProposalService,
            IExpertFavoriteServiceService expertFavoriteServiceService
           )
        {
            _orderService = orderService;
            _bidService = bidService;
            _orderProposalService = orderProposalService;
            expertFavoriteServiceService = expertFavoriteServiceService;
        }
        public async Task<int> Add(OrderDto orderDto, CancellationToken cancellationToken)
        {
            return await _orderService.Add(orderDto, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _orderService.Delete(id, cancellationToken);
        }

        public async Task<OrderDto?> Get(int id, CancellationToken cancellationToken)
        {
            return await _orderService.Get(id, cancellationToken);
        }

        public async Task<List<OrderDto?>> GetAccepted(int customerId, CancellationToken cancellationToken)
        {
            return await _orderService.GetAccepted(customerId, cancellationToken);
        }

        public async Task<List<OrderDto>?> Search(string? keyWord, CancellationToken cancellationToken)
        {
            return await _orderService.Search(keyWord, cancellationToken);
        }

        public async Task<List<OrderDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _orderService.GetAll(cancellationToken);
        }

        public async Task<List<OrderDto?>> GetAllForCustomer(int customerId, CancellationToken cancellationToken)
        {
            return await _orderService.GetAllForCustomer(customerId, cancellationToken);
        }

        public async Task<OrderDto?> GetForCustomer(int customerId, int orderId, CancellationToken cancellationToken)
        {
            return await _orderService.GetForCustomer(customerId, orderId, cancellationToken);
        }

        public async Task SelectBid(int customerId, int bidId, CancellationToken cancellationToken)
        {
            var bid = await _bidService.Get(bidId, cancellationToken);
            var order = await _orderService.GetForCustomer(customerId, bid.OrderId, cancellationToken);
            if (order != null)
            {
                var bids = await _bidService.GetAllByOrderId(bid.OrderId, cancellationToken);
                foreach (var item in bids)
                {
                    if (item.Id == bidId)
                    {
                        bid.ProposalStatus = ProposalStatusEnum.Accepted;
                        await _bidService.Update(bid, cancellationToken);
                        var expertId = bid.ExpertId;
                        var orderProposal = await _orderProposalService.GetByOrderId(bid.OrderId, cancellationToken);
                        orderProposal.ProposalStatus = ProposalStatusEnum.Accepted;
                        order.ExpertId = expertId;
                        order.OrderStatus=OrderStatusEnum.WaitingForSpecialistArrival;
                        bid.SuggestedPrice = orderProposal.SuggestedPrice;
                        await _orderService.Update(order, cancellationToken);
                        await _orderProposalService.Update(orderProposal, cancellationToken);

                    }
                    else
                    {
                        order.OrderStatus=OrderStatusEnum.Cancelled;
                        await _bidService.Update(bid, cancellationToken);
                        var orderProposal = await _orderProposalService.GetByOrderId(bid.OrderId, cancellationToken);
                        orderProposal.ProposalStatus = ProposalStatusEnum.Rejected;
                        await _orderProposalService.Update(orderProposal, cancellationToken);

                    }
                }
            }
        }

        public async Task<int> Set(int customerId, string Discription, DateTime PrefferedDateTime, int ServiceId, CancellationToken cancellationToken)
        {
            return await _orderService.Set(customerId, Discription, PrefferedDateTime, ServiceId, cancellationToken);
        }

        public async Task Update(OrderDto orderDto, CancellationToken cancellationToken)
        {
            await _orderService.Update(orderDto, cancellationToken);
        }

        public async Task<List<OpenOrderOutPutDto>> GetOpenOrders(int expertId, CancellationToken cancellationToken)
        {
            var favoriteServices= await _expertFavoriteServiceService.GetServices(expertId, cancellationToken);
            var orders= new List<OpenOrderOutPutDto>();
            foreach (var service in favoriteServices)
            {
                var order = await _orderService.GetOpenOrders(expertId, service.Id, cancellationToken);
                foreach (var item in order)
                {
                    orders.Add(item);
                }
            }
            return orders;
        }
        public async Task<int> SelectOrder(int expertId,int orderId, int suggestedPrice, CancellationToken cancellationToken)
        {
            await _orderService.SelectOrder(expertId, orderId, cancellationToken);
            await _orderProposalService.AcceptOrder(expertId, orderId, suggestedPrice,cancellationToken);
           var bidId= await _bidService.Add(expertId, orderId,suggestedPrice,cancellationToken);
            return bidId;
        }
    }
}
