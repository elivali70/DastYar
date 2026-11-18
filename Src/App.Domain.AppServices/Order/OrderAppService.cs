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
        private readonly IExpertFavoriteServiceService _expertFavoriteServiceService;

        public OrderAppService(IOrderService orderService,
            IBidService bidService,
            IOrderProposalService orderProposalService,
            IExpertFavoriteServiceService expertFavoriteServiceService
           )
        {
            _orderService = orderService;
            _bidService = bidService;
            _expertFavoriteServiceService = expertFavoriteServiceService;
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
            var selectedBid = await _bidService.Get(bidId, cancellationToken);

            if (selectedBid == null)
                throw new Exception("Bid does not exist.");

            var order = await _orderService.GetForCustomer( customerId,selectedBid.OrderId, cancellationToken);

            if (order == null)
                throw new Exception("Order does not belong to this customer.");

            if (order.OrderStatus != OrderStatusEnum.WaitingForProposals ||order.ExpertId != null)
            {
                throw new Exception("This order has already been processed.");
            }

            if (selectedBid.ProposalStatus != ProposalStatusEnum.Pending)
                throw new Exception("This bid cannot be selected.");

            // دریافت تمام پیشنهادهای سفارش
            var bids = await _bidService.GetAllByOrderId(selectedBid.OrderId, cancellationToken);

            foreach (var bid in bids)
            {
                if (bid.ProposalStatus != ProposalStatusEnum.Pending)
                    continue;

                if (bid.Id == selectedBid.Id)
                {
                    bid.ProposalStatus = ProposalStatusEnum.Accepted;
                }
                else
                {
                    bid.ProposalStatus = ProposalStatusEnum.Rejected;
                }

                await _bidService.Update(bid, cancellationToken);
            }

            // اختصاص سفارش به متخصص انتخاب‌شده
            order.ExpertId = selectedBid.ExpertId;

            order.OrderStatus = OrderStatusEnum.WaitingForSpecialistArrival;

            await _orderService.Update(order, cancellationToken);
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
                var serviceOrders = await _orderService.GetOpenOrders(expertId, service.Id, cancellationToken);
                orders.AddRange(serviceOrders);
            }
            return orders.DistinctBy(x => x.OrderId).ToList(); ;
        }
        public async Task<int> SelectOrder(int expertId,int orderId, int suggestedPrice, CancellationToken cancellationToken)
        {
            if (suggestedPrice <= 0)
                throw new Exception("Invalid suggested price.");

            var order = await _orderService.Get(orderId, cancellationToken);

            if (order == null)
                throw new Exception("Order does not exist.");

            if (order.OrderStatus != OrderStatusEnum.WaitingForProposals ||order.ExpertId != null)
            {
                throw new Exception("This order is no longer open.");
            }
            // بررسی خدمات موردعلاقه متخصص

            var favoriteServices = await _expertFavoriteServiceService.GetServices(expertId, cancellationToken);

            if (!favoriteServices.Any(x => x.Id == order.ServiceId))
                throw new Exception("This service is not in your favorites.");
            // بررسی ثبت پیشنهاد قبلی متخصص

            var existingBids = await _bidService.GetAllByOrderId(orderId, cancellationToken);

            if (existingBids.Any(x =>x.ExpertId == expertId && x.ProposalStatus != ProposalStatusEnum.Withdrawn))
            {
                throw new Exception("You have already submitted a bid.");
            }

            // ثبت پیشنهاد
            var bidId = await _bidService.Add( expertId,orderId,suggestedPrice,cancellationToken);

            return bidId;
        }
    }
    }
