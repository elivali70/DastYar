using App.Domain.Core.AppFile.Comntracts.Repositories;
using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.Order.Dtos;
using App.Domain.Core.Order.Enities;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.NativeInterop;

public class OrderCommandRepository : IOrderCommandRepository
{
    private readonly AppDbContext _appDbContext;

    public OrderCommandRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<int> Add(OrderDto orderDto, CancellationToken cancellationToken)
    {
        var order = new Order();
        order.ServiceId = orderDto.ServiceId;
        order.BasePrice = orderDto.BasePrice;
        order.OrderStatus = orderDto.OrderStatus;
        order.CustomerId = orderDto.CustomerId;
        order.CreatedAt = DateTime.Now;
        order.PrefferedDateTime = orderDto.PrefferedDateTime;
        order.ExpertId = orderDto.ExpertId;
        order.Discription = orderDto.Discription;
        order.IsDeleted = false;

        await _appDbContext.AddAsync(order);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return order.Id;

    }
    public async Task Update(OrderDto orderDto, CancellationToken cancellationToken)
    {
        var order = await _appDbContext.Orders.SingleOrDefaultAsync(x => x.Id == orderDto.Id);
        if (order != null)
        {
            order.Id = orderDto.Id;
            order.ServiceId = orderDto.ServiceId;
            order.BasePrice = orderDto.BasePrice;
            order.OrderStatus = orderDto.OrderStatus;
            order.CustomerId = orderDto.CustomerId;
            order.CreatedAt = orderDto.CreatedAt;
            order.PrefferedDateTime = orderDto.PrefferedDateTime;
            order.ExpertId = orderDto.ExpertId;
            order.Discription = orderDto.Discription;

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        else
        {
            throw new Exception($"order with Id={orderDto.Id} dosnt exist");

        }
    }
    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        var order = await _appDbContext.Orders.SingleOrDefaultAsync(x => x.Id == id);
        if (order != null)
        {
            order.IsDeleted = true;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        else
        {
            throw new Exception($"order with Id={id} dosnt exist");
        }
    }
}

