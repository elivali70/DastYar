using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.Service.Dtos;
using App.Domain.Core.Service.Entities;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Core.Service.Contracts.Repositories;

public class ServiceFileQueryRepository:IServiceFileQueryRepository
{
    private readonly AppDbContext _appDbContext;

    public ServiceFileQueryRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<ServiceFileDto>> GetAll(CancellationToken cancellationToken)
    {
        var serviceFiles = await _appDbContext.ServiceFiles.Where(x => !x.IsDeleted).Select(s => new ServiceFileDto()
        {
            ServiceId = s.ServiceId,
          FileId = s.FileId,
           CreatedAt = s.CreatedAt

        }).ToListAsync(cancellationToken);
        return serviceFiles;
    }
   public async Task<ServiceFileDto?> Get(int id, CancellationToken cancellationToken)
    {
        var serviceFile = await _appDbContext.ServiceFiles.Where(x => !x.IsDeleted && x.Id==id).Select(s => new ServiceFileDto()
        {
            ServiceId = s.ServiceId,
            FileId = s.FileId,
            CreatedAt = s.CreatedAt

        }).FirstOrDefaultAsync(cancellationToken);
        return serviceFile;

    }
}
