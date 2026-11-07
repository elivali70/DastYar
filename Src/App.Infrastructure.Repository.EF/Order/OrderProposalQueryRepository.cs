using App.Domain.Core.AppFile.Comntracts.Repositories;
using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.Order.Dtos;
using App.Domain.Core.Order.Enities;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;

public class OrderProposalQueryRepository : IOrderProposalQueryRepository
{
    private readonly AppDbContext _appDbContext;

    public OrderProposalQueryRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<OrderProposalDto>> GetAll(CancellationToken cancellationToken)
    {
        var orderProposals = await _appDbContext.OredeProposals.Where(x => !x.IsDeleted).Select(s => new OrderProposalDto()
        {

            ProposalStatus = s.ProposalStatus,
            SuggestedPrice = s.SuggestedPrice,
            OrderId = s.OrderId,
            CreatedAt = s.CreatedAt,
            Discription = s.Discription,
            DurationTime = s.DurationTime,
            ExpertId = s.ExpertId,
            StatrtedDateTime = s.StatrtedDateTime,
        }).ToListAsync(cancellationToken);
        return orderProposals;
    }
    public async Task<OrderProposalDto?> Get(int id, CancellationToken cancellationToken)
    {
        var orderProposal = await _appDbContext.OredeProposals.Where(x => !x.IsDeleted && x.Id==id).Select(s => new OrderProposalDto()
        {

            ProposalStatus = s.ProposalStatus,
            SuggestedPrice = s.SuggestedPrice,
            OrderId = s.OrderId,
            CreatedAt = s.CreatedAt,
            Discription = s.Discription,
            DurationTime = s.DurationTime,
            ExpertId = s.ExpertId,
            StatrtedDateTime = s.StatrtedDateTime,
        }).SingleOrDefaultAsync(cancellationToken);
        return orderProposal;
    }
    public async Task<OrderProposalDto?> GetByOrderId(int orderId, CancellationToken cancellationToken)
    {
        var orderProposal = await _appDbContext.OredeProposals.Where(x => !x.IsDeleted && x.OrderId == orderId).Select(s => new OrderProposalDto()
        {

            ProposalStatus = s.ProposalStatus,
            SuggestedPrice = s.SuggestedPrice,
            OrderId = s.OrderId,
            CreatedAt = s.CreatedAt,
            Discription = s.Discription,
            DurationTime = s.DurationTime,
            ExpertId = s.ExpertId,
            StatrtedDateTime = s.StatrtedDateTime,
        }).SingleOrDefaultAsync(cancellationToken);
        return orderProposal;
    }
}

