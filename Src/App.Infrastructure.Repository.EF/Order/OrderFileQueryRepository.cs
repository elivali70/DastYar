using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.Order.Dtos;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;

public class OrderFileQueryRepository: IOrderFileQueryRepository
{
    private readonly AppDbContext _appDbContext;

    public OrderFileQueryRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<OrderFileDto>> GetAll(CancellationToken cancellationToken)
    {
        var orderFiles = await _appDbContext.OrderFiles.Where(x => !x.IsDeleted).Select(s => new OrderFileDto()
        {
           Id = s.Id,
           FileId= s.FileId,
           OrderId = s.OrderId,
        }).ToListAsync(cancellationToken);
        return orderFiles;
    }
    public async Task<OrderFileDto?> Get(int id,CancellationToken cancellationToken)
    {
        var orderFile = await _appDbContext.OrderFiles.Where(x => !x.IsDeleted && x.Id==id).Select(s => new OrderFileDto()
        {
            Id = s.Id,
            FileId = s.FileId,
            OrderId = s.OrderId,
        }).SingleOrDefaultAsync(cancellationToken);
        return orderFile;
    }
}
