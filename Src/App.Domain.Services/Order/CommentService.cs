using App.Domain.Core.Order.Contracts.Repositories;
using App.Domain.Core.Order.Contracts.Services;
using App.Domain.Core.Order.Enities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Services.Order
{
    public class CommentService:ICommentService
    {
        private readonly ICommentQueryRepository _commentQueryRepository;
        private readonly ICommentCommandRepository _commentCommandRepository;

        public CommentService(ICommentQueryRepository commentQueryRepository,ICommentCommandRepository commentCommandRepository)
        {
            _commentQueryRepository = commentQueryRepository;
            _commentCommandRepository = commentCommandRepository;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _commentCommandRepository.Delete(id, cancellationToken);
        }

        public async Task<List<CommentDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _commentQueryRepository.GetAll(cancellationToken);
        }
    }
}
