using App.Domain.Core.AppFile.Comntracts.Repositories;
using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.AppFile.Entities;
using App.Domain.Core.Order.Dtos;
using App.Domain.Core.Order.Enities;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.NativeInterop;

public class OrderFileCommandRepository : IOrderFileCommandRepository
{
    private readonly AppDbContext _appDbContext;

    public OrderFileCommandRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<int> Add(OrderFileDto orderFileDto, CancellationToken cancellationToken)
    {
        var orderFile = new OrderFile();
        orderFile.OrderId = orderFileDto.OrderId;
        orderFile.FileId = orderFileDto.FileId;
        orderFile.IsDeleted = false; 
        await _appDbContext.AddAsync(orderFile);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return orderFile.Id;
    }
    public async Task Update(OrderFileDto orderFileDto, CancellationToken cancellationToken)
    {
        var orderFile = await _appDbContext.OrderFiles.SingleOrDefaultAsync(x => x.Id == orderFileDto.Id);
        if (orderFile != null)
        {
            orderFile.Id = orderFileDto.Id;
            orderFile.OrderId = orderFileDto.OrderId;
            orderFile.FileId = orderFileDto.FileId;

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        else
        {
            throw new Exception($"orderFile with Id={orderFileDto.Id} dosnt exist");

        }

    }
    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        var orderFile = await _appDbContext.OrderFiles.SingleOrDefaultAsync(x => x.Id == id);
        if (orderFile != null)
        {
            orderFile.IsDeleted = true;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        else
        {
            throw new Exception($"orderFile with Id={id} dosnt exist");
        }
    }
}

