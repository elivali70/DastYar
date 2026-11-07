using App.Domain.Core.Category.Dtos;
using App.Domain.Core.Order.Dtos;
using App.Domain.Core.Order.Enities;
using App.Domain.Core.Order.Enums;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.NativeInterop;

public class BidCommandRepository:IBidCommandRepository
{
    private readonly AppDbContext _appDbContext;

    public BidCommandRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Add(BidDto bidDto, CancellationToken cancellationToken)
    {
        var bid= new Bid();
        bid.ProposalStatus = bidDto.ProposalStatus;
        bid.OrderId = bidDto.OrderId;
        bid.SuggestedPrice= bidDto.SuggestedPrice;
        bid.CreatedAt=DateTime.Now;
        bid.IsDeleted = false;

        await _appDbContext.AddAsync(bid);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return bid.Id;
    }

    public async Task Update(BidDto bidDto, CancellationToken cancellationToken)
    {
        var bid = await _appDbContext.Bids.SingleOrDefaultAsync(x => x.Id == bidDto.Id);

        if (bid == null)
        {
            throw new Exception($" There is no bid with Id={bidDto.Id}");
        }
        else
        {
            bid.OrderId = bidDto.OrderId;
            bid.SuggestedPrice= bidDto.SuggestedPrice;
            bid.CreatedAt=bidDto.CreatedAt;
            bid.ExpertId= bidDto.ExpertId;
            bid.ProposalStatus= bidDto.ProposalStatus;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }

  
   public async Task Delete(int id, CancellationToken cancellationToken)
    {
        var bid = await _appDbContext.Bids.SingleOrDefaultAsync(x => x.Id ==id);
        if (bid == null)
        {
            throw new Exception($" There is no bid with Id={id}");
        }
        else
        {
            bid.IsDeleted = true;
            _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }


}



