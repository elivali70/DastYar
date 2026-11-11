using App.Domain.Core.AppFile.Comntracts.Repositories;
using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.Order.Dtos;
using App.Domain.Core.Order.Enities;
using App.Domain.Core.Order.Enums;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;

public class OrderQueryRepository : IOrderQueryRepository
{
    private readonly AppDbContext _appDbContext;

    public OrderQueryRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<OrderDto>> GetAll(CancellationToken cancellationToken)
    {
        var oreders = await _appDbContext.Orders.Where(x => !x.IsDeleted).Select(s => new OrderDto()
        {
            Id = s.Id,
            ServiceId = s.ServiceId,
            BasePrice = s.BasePrice,
            OrderStatus = s.OrderStatus,
            CustomerId = s.CustomerId,
            CreatedAt = s.CreatedAt,
            PrefferedDateTime = s.PrefferedDateTime,
            ExpertId = s.ExpertId,
            Discription = s.Discription,
        }).ToListAsync(cancellationToken);
        return oreders;

    }
    public async Task<OrderDto?> Get(int id, CancellationToken cancellationToken)
    {
        var oreder = await _appDbContext.Orders.Where(x => !x.IsDeleted&& x.Id==id).Select(s => new OrderDto()
        {
            Id = s.Id,
            ServiceId = s.ServiceId,
            BasePrice = s.BasePrice,
            OrderStatus = s.OrderStatus,
            CustomerId = s.CustomerId,
            CreatedAt = s.CreatedAt,
            PrefferedDateTime = s.PrefferedDateTime,
            ExpertId = s.ExpertId,
            Discription = s.Discription,
        }).SingleOrDefaultAsync(cancellationToken);
        return oreder;

    }

    public async Task<List<OrderDto>?> GetAll(string? keyWord, CancellationToken cancellationToken)
    {
        var orderList = new List<Order>();
        if (!string.IsNullOrWhiteSpace(keyWord))
        {

            var order = await _appDbContext.Orders.Where(x => x.Discription.Contains(keyWord)).ToListAsync(cancellationToken);
            orderList = order;
       
        }
      else
        {
            var order= await _appDbContext.Orders.Where(x => !x.IsDeleted) .ToListAsync(cancellationToken);
            orderList = order;
        }
       var orderDto= orderList.Select(s => new OrderDto()
        {
            Id = s.Id,
            ServiceId = s.ServiceId,
            BasePrice = s.BasePrice,
            OrderStatus = s.OrderStatus,
            CustomerId = s.CustomerId,
            CreatedAt = s.CreatedAt,
            PrefferedDateTime = s.PrefferedDateTime,
            ExpertId = s.ExpertId,
            Discription = s.Discription,
        }).ToList();
        return orderDto;

    }

    public async Task<List<OrderDto?>> GetAllForCustomer(int customerId, CancellationToken cancellationToken)
    {
        var oreders = await _appDbContext.Orders.Where(x => !x.IsDeleted && x.CustomerId == customerId).Select(s => new OrderDto()
        {
            Id = s.Id,
            ServiceId = s.ServiceId,
            BasePrice = s.BasePrice,
            OrderStatus = s.OrderStatus,
            CustomerId = s.CustomerId,
            CreatedAt = s.CreatedAt,
            PrefferedDateTime = s.PrefferedDateTime,
            ExpertId = s.ExpertId,
            Discription = s.Discription,
        }).ToListAsync(cancellationToken);
        return oreders;
    }
    public async Task<OrderDto?> GetForCustomer(int customerId,int orderId ,CancellationToken cancellationToken)
    {
        var oreder = await _appDbContext.Orders.Where(x => !x.IsDeleted && x.CustomerId == customerId && x.Id==orderId).Select(s => new OrderDto()
        {
            Id = s.Id,
            ServiceId = s.ServiceId,
            BasePrice = s.BasePrice,
            OrderStatus = s.OrderStatus,
            CustomerId = s.CustomerId,
            CreatedAt = s.CreatedAt,
            PrefferedDateTime = s.PrefferedDateTime,
            ExpertId = s.ExpertId,
            Discription = s.Discription,
        }).SingleOrDefaultAsync(cancellationToken);
        return oreder;
    }

    public async Task<List<OrderDto?>> GetAccepted(int customerId, CancellationToken cancellationToken)
    {
        var oreders = await _appDbContext.Orders.Where(x => !x.IsDeleted && x.CustomerId == customerId &&
        x.OrderStatus==OrderStatusEnum.Completed).Select(s => new OrderDto()
        {
            Id = s.Id,
            ServiceId = s.ServiceId,
            BasePrice = s.BasePrice,
            OrderStatus = s.OrderStatus,
            CustomerId = s.CustomerId,
            CreatedAt = s.CreatedAt,
            PrefferedDateTime = s.PrefferedDateTime,
            ExpertId = s.ExpertId,
            Discription = s.Discription,
        }).ToListAsync(cancellationToken);
        return oreders;
    }
}
  