using App.Domain.Core.AppFile.Comntracts.Repositories;
using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.Order.Dtos;
using order=App.Domain.Core.Order.Enities;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.NativeInterop;

public class OrderProposalCommandRepository: IOrderProposalCommandRepository
{
    private readonly AppDbContext _appDbContext;

    public OrderProposalCommandRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<int> Add(OrderProposalDto orderProposalDto,CancellationToken cancellationToken)
    {
        var orderProposal = new order.OrederProposal();
        orderProposal.ProposalStatus = orderProposalDto.ProposalStatus;
        orderProposal.SuggestedPrice = orderProposalDto.SuggestedPrice;
        orderProposal.OrderId = orderProposalDto.OrderId;
        orderProposal.CreatedAt = DateTime.Now;
        orderProposal.Discription = orderProposalDto.Discription;
        orderProposal.DurationTime = orderProposalDto.DurationTime;
        orderProposal.ExpertId = orderProposalDto.ExpertId;
        orderProposal.StatrtedDateTime = orderProposalDto.StatrtedDateTime;
        orderProposal.IsDeleted=false;

        await _appDbContext.AddAsync(orderProposal);
        await _appDbContext.SaveChangesAsync(cancellationToken);
         return orderProposal.Id;
    }
    public async Task Update(OrderProposalDto orderProposalDto, CancellationToken cancellationToken)
    {
        var orderProposal = await _appDbContext.OredeProposals.SingleOrDefaultAsync(x => x.Id == orderProposalDto.Id);
        if (orderProposal != null)
        {
           orderProposal.ProposalStatus=orderProposalDto.ProposalStatus;
            orderProposal.SuggestedPrice=orderProposalDto.SuggestedPrice;
            orderProposal.OrderId=orderProposalDto.OrderId;
            orderProposal.CreatedAt=orderProposalDto.CreatedAt;
            orderProposal.Discription=orderProposalDto.Discription;
            orderProposal.DurationTime=orderProposalDto.DurationTime;
            orderProposal.ExpertId=orderProposalDto.ExpertId;
            orderProposal.StatrtedDateTime=orderProposalDto.StatrtedDateTime;
            
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        else
        {
            throw new Exception($"orderProposal with Id={orderProposalDto.Id} dosnt exist");

        }

    }
    public async Task Delete(int id,CancellationToken cancellationToken)
    {
        var orderProposal = await _appDbContext.OredeProposals.SingleOrDefaultAsync(x => x.Id == id);
        if (orderProposal != null)
        {
            orderProposal.IsDeleted = true;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        else
        {
            throw new Exception($"orderProposal with Id={id} dosnt exist");
        }
    }
}
