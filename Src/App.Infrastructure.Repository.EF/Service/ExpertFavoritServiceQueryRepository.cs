using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.Service.Dtos;
using App.Domain.Core.Service.Entities;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Core.Service.Contracts.Repositories;

public class ExpertFavoritServiceQueryRepository:IExpertFavoritServiceQueryRepository
{
    private readonly AppDbContext _appDbContext;

    public ExpertFavoritServiceQueryRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<ExpertFavoritServiceDto>> GetAll(CancellationToken cancellationToken)
    {
        var expertFavoriteServices = await _appDbContext.ExpertFavoritServices.Where(x => !x.IsDeleted).Select(s => new ExpertFavoritServiceDto()
        {
           ServiceId = s.ServiceId,
          ExpertId = s.ExpertId,
           CreatedAt =s.CreatedAt,
           Id=s.Id,

        }).ToListAsync(cancellationToken);
        return expertFavoriteServices;
    }
    public async Task<ExpertFavoritServiceDto?> Get(int id, CancellationToken cancellationToken)
    {
        var expertFavoriteService = await _appDbContext.ExpertFavoritServices.Where(x => x.Id == id && !x.IsDeleted).Select(s => new ExpertFavoritServiceDto()
        {
            ServiceId = s.ServiceId,
            ExpertId = s.ExpertId,
            CreatedAt = s.CreatedAt,
            Id = s.Id,
        }).SingleOrDefaultAsync(cancellationToken);
        return expertFavoriteService;
    }
}
