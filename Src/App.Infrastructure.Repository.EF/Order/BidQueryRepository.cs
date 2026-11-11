using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.Order.Dtos;
using App.Domain.Core.Order.Enities;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;

public class BidQueryRepository:IBidQueryRepository
{


    private readonly AppDbContext _appDbContext;

    public BidQueryRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async   Task<List<BidDto>> GetAll(CancellationToken cancellationToken)
    {
        var bids = await _appDbContext.Bids.Where(x => !x.IsDeleted).Select(s => new BidDto()
        {
            Id = s.Id,
           SuggestedPrice=s.SuggestedPrice,
           ExpertId=s.ExpertId,
           CreatedAt=s.CreatedAt,
           OrderId=s.OrderId,
           ProposalStatus=s.ProposalStatus,
        }).ToListAsync(cancellationToken);
        return bids;
    }

    public async Task<BidDto?> Get(int id, CancellationToken cancellationToken)
    {
        var bid = await _appDbContext.Bids.Where(x => x.Id == id && !x.IsDeleted).Select(s => new BidDto()
        {
            Id = s.Id,
            SuggestedPrice = s.SuggestedPrice,
            ExpertId = s.ExpertId,
            CreatedAt = s.CreatedAt,
            OrderId = s.OrderId,
            ProposalStatus = s.ProposalStatus,
        }).SingleOrDefaultAsync(cancellationToken);
        return bid;
    }
    public async Task<List<BidDto>> GetAll(string? keyWord, CancellationToken cancellationToken)
    {
        var bidList = new List<Bid>();
        if (!string.IsNullOrWhiteSpace(keyWord))
        {

            var bids = await _appDbContext.Bids.Where(x => x.ProposalStatus.ToString().Contains(keyWord)).ToListAsync(cancellationToken);
            bidList = bids;

        }
        else
        {
            var bids = await _appDbContext.Bids.Where(x => !x.IsDeleted).ToListAsync(cancellationToken);
            bidList = bids;
        }
        var orderDto = bidList.Select(s => new BidDto()
        {
            Id = s.Id,
           CreatedAt= s.CreatedAt,
           ExpertId= s.ExpertId,
           OrderId= s.OrderId,
           ProposalStatus= s.ProposalStatus,
           SuggestedPrice= s.SuggestedPrice,
        }).ToList();
        return orderDto;

    }

    public async Task<List<BidDto>> GetAllByOrderId(int orderId, CancellationToken cancellationToken)
    {
        var bids = await _appDbContext.Bids.Where(x => !x.IsDeleted && x.OrderId==orderId).Select(s => new BidDto()
        {
            Id = s.Id,
            SuggestedPrice = s.SuggestedPrice,
            ExpertId = s.ExpertId,
            CreatedAt = s.CreatedAt,
            OrderId = s.OrderId,
            ProposalStatus = s.ProposalStatus,
        }).ToListAsync(cancellationToken);
        return bids;
    }
}
