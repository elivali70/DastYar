using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.Service.Dtos;
using App.Domain.Core.Service.Entities;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Core.Service.Contracts.Repositories;

public class ExpertFavoritServiceCommandRepository:IExpertFavoritServiceCommandRepository
{
    private readonly AppDbContext _appDbContext;

    public ExpertFavoritServiceCommandRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<int> Add(ExpertFavoritServiceDto expertFavoritServiceDto, CancellationToken cancellationToken)
    {
        var expertFavoriteService = new ExpertFavoritService();

        expertFavoriteService.ServiceId = expertFavoritServiceDto.ServiceId;
        expertFavoriteService.ExpertId = expertFavoritServiceDto.ExpertId;
        expertFavoriteService.CreatedAt=DateTime.Now;

        await _appDbContext.AddAsync(expertFavoriteService);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return expertFavoriteService.Id;
    }
    public async Task Update(ExpertFavoritServiceDto expertFavoritServiceDto, CancellationToken cancellationToken)
    {
        var expertFavoriteService = await _appDbContext.ExpertFavoritServices.SingleOrDefaultAsync(x => x.Id == expertFavoritServiceDto.Id);
        if (expertFavoriteService != null)
        {
            expertFavoriteService.ServiceId = expertFavoritServiceDto.ServiceId;
            expertFavoriteService.ExpertId = expertFavoritServiceDto.ExpertId;
            expertFavoriteService.Id = expertFavoritServiceDto.Id;
            expertFavoriteService.CreatedAt = expertFavoritServiceDto.CreatedAt;

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        else
        {
            throw new Exception($"expertFavoriteService with Id={expertFavoritServiceDto.Id} dosnt exist");

        }
    }
    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        var expertFavoriteService = await _appDbContext.ExpertFavoritServices.SingleOrDefaultAsync(x => x.Id == id);
        if (expertFavoriteService != null)
        {
            expertFavoriteService.IsDeleted = true;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        else
        {
            throw new Exception($"expertFavoriteService with Id={id} dosnt exist");
        }
    }
}

   