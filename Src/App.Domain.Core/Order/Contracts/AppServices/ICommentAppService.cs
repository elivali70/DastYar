using App.Domain.Core.Order.Enities;
using App.Domain.Core.User.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace App.Domain.Core.Order.Contracts.AppServices
{
    public interface ICommentAppService
    {
        Task<List<CommentDto>> GetAll(CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<CommentDto?> GetByOrderId(int orderId,CancellationToken cancellationToken);
         Task Add(int customerId,int OrderId,string discription, CancellationToken cancellationToken);
    }
}
