using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.Service.Contracts.Repositories;
using App.Domain.Core.Service.Entities;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Core.Service.Dtos;

public class ServiceQueryRepository : IServiceQueryRepository
{
    private readonly AppDbContext _appDbContext;

    public ServiceQueryRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<ServiceDto>> GetAll(CancellationToken cancellationToken)
    {
        var service = await _appDbContext.Services.Where(x => !x.IsDeleted).Select(s => new ServiceDto()
        {
          Disctiption = s.Disctiption,
            Name = s.Name,
         BasePrice = s.BasePrice,
           CategoryId = s.CategoryId,
           CreatedAt = s.CreatedAt,
        }).ToListAsync(cancellationToken);
        return service;
    }
    public async Task<ServiceDto?> Get(int id, CancellationToken cancellationToken)
    {
        var service = await _appDbContext.Services.Where(x => !x.IsDeleted && x.Id==id).Select(s => new ServiceDto()
        {
            Disctiption = s.Disctiption,
            Name = s.Name,
            BasePrice = s.BasePrice,
            CategoryId = s.CategoryId,
                  CreatedAt = s.CreatedAt,
        }).SingleOrDefaultAsync(cancellationToken);
        return service;
    }

    public async Task<ServiceDto?> Get(string name, CancellationToken cancellationToken)
    {
        var service = await _appDbContext.Services.Where(x => !x.IsDeleted && x.Name == name).Select(s => new ServiceDto()
        {
            Disctiption = s.Disctiption,
            Name = s.Name,
            BasePrice = s.BasePrice,
            CategoryId = s.CategoryId,
            CreatedAt = s.CreatedAt,
        }).SingleOrDefaultAsync(cancellationToken);
        return service;
    }
}