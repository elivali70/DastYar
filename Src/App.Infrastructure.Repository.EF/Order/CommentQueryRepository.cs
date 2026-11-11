using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.Order.Enities;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Core.Order.Contracts.Repositories;

public class CommentQueryRepository: ICommentQueryRepository
{

    private readonly AppDbContext _appDbContext;

    public CommentQueryRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async  Task<List<CommentDto>> GetAll(CancellationToken cancellationToken)
    {
        var comments = await _appDbContext.Comments.Where(x => !x.IsDeleted).Select(s => new CommentDto()
        {
            Id = s.Id,
            Discription=s.Discription,
            CreatedAt=s.CreatedAt,
            OrderId=s.OrderId,
            UserId=s.UserId,
        }).ToListAsync(cancellationToken);
        return comments;
    }
   public async Task<CommentDto?> Get(int orderId, CancellationToken cancellationToken)
    {
        var comment = await _appDbContext.Comments.Where(x => x.UserId ==orderId && !x.IsDeleted).Select(s => new CommentDto()
        {
            Id = s.Id,
            Discription = s.Discription,
            CreatedAt = s.CreatedAt,
            OrderId = s.OrderId,
            UserId = s.UserId,
        }).SingleOrDefaultAsync(cancellationToken);
        return comment;

    }

    public async  Task<CommentDto?> GetByOrderId(int orderId, CancellationToken cancellationToken)
    {
        var comment = await _appDbContext.Comments.Where(x => !x.IsDeleted && x.OrderId==orderId).Select(s => new CommentDto()
        {
            Id = s.Id,
            Discription = s.Discription,
            CreatedAt = s.CreatedAt,
            OrderId = s.OrderId,
            UserId = s.UserId,
        }).SingleOrDefaultAsync(cancellationToken);
        return comment;
    }
}
