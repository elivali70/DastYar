using App.Domain.Core.Order.Contracts.AppServices;
using App.Domain.Core.Order.Contracts.Services;
using App.Domain.Core.Order.Enities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.AppServices.Order
{
    
    public class CommentAppService : ICommentAppService
    {
        private readonly ICommentService _commentService;

        public CommentAppService(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public async Task<List<CommentDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _commentService.GetAll(cancellationToken);
        }
        public async Task Delete(int id,CancellationToken cancellationToken)
        {
             await _commentService.Delete(id,cancellationToken);
        }

        public async Task<CommentDto?> GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await _commentService.GetByOrderId(orderId,cancellationToken);
        }

        public async Task Add(int customerId, int OrderId, string discription, CancellationToken cancellationToken)
        {
            await _commentService.Add(customerId, OrderId, discription, cancellationToken);
        }
    }
}
