using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.Order.Enities;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Core.Order.Contracts.Repositories;

public class CommentCommandRepository: ICommentCommandRepository
{
    private readonly AppDbContext _appDbContext;

    public CommentCommandRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Add(CommentDto commentDto, CancellationToken cancellationToken)
    {
        var comment = new Comment();

        comment.OrderId = commentDto.OrderId;
        comment.UserId = commentDto.UserId;
        comment.CreatedAt = commentDto.CreatedAt;
        comment.Discription = commentDto.Discription;
        comment.CreatedAt = DateTime.Now;
        comment.IsDeleted = false;

        await _appDbContext.AddAsync(comment);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return comment.Id;
    }

    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        var comment = await _appDbContext.Comments.SingleOrDefaultAsync(x => x.Id == id);
        if (comment != null)
        {
            comment.IsDeleted = true;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        else
        {
            throw new Exception($"Comment with Id={id} dosnt exist");
        }
    }

    public  async Task Update(CommentDto commentDto, CancellationToken cancellationToken)
    {
        var comment = await _appDbContext.Comments.SingleOrDefaultAsync(x => x.Id == commentDto.Id);
        if (comment != null)
        {
            comment.OrderId = commentDto.OrderId;
            comment.UserId = commentDto.UserId;
            comment.CreatedAt = commentDto.CreatedAt;
            comment.Discription= commentDto.Discription;
            comment.UserId=commentDto.UserId;

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        else
        {
            throw new Exception($"Comment with Id={commentDto.Id} dosnt exist");

        }
    }
}

  