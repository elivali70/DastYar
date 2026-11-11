using App.Domain.Core.Order.Enities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.Order.Contracts.Services
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAll(CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<CommentDto?> GetByOrderId(int orderId, CancellationToken cancellationToken);
        Task Add(int userId, int OrderId, string discription, CancellationToken cancellationToken);
    }
}
