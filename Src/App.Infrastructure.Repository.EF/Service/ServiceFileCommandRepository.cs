using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.AppFile.Entities;
using App.Domain.Core.Service.Dtos;
using App.Domain.Core.Service.Entities;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Core.Service.Contracts.Repositories;

public class ServiceFileCommandRepository:IServiceFileCommandRepository
{
    private readonly AppDbContext _appDbContext;

    public ServiceFileCommandRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Add(ServiceFileDto serviceFileDto, CancellationToken cancellationToken)
    {
        var serviceFile = new ServiceFile();
        serviceFile.ServiceId = serviceFileDto.ServiceId;
        serviceFile.FileId = serviceFileDto.FileId;
        serviceFile.CreatedAt = DateTime.Now;
        serviceFile.IsDeleted = false;

        await _appDbContext.AddAsync(serviceFile);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return serviceFile.Id;
    }
   public async Task Update(ServiceFileDto serviceFileDto, CancellationToken cancellationToken)
    {
        var serviceFile = await _appDbContext.ServiceFiles.SingleOrDefaultAsync(x => x.Id == serviceFileDto.Id);
        if (serviceFile == null)
        {
            throw new Exception($"serviceFile with Id={serviceFileDto.Id} dosnt exist");
        }
        else
        {
            serviceFile.ServiceId = serviceFileDto.ServiceId;
            serviceFile.FileId = serviceFileDto.FileId;
            serviceFile.CreatedAt = serviceFileDto.CreatedAt;
          
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
   public async Task Delete(int id, CancellationToken cancellationToken)
    {
        var serviceFile = await _appDbContext.ServiceFiles.SingleOrDefaultAsync(x => x.Id == id);
        if (serviceFile != null)
        {
            serviceFile.IsDeleted = true;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        else
        {
            throw new Exception($"serviceFile with Id={id} dosnt exist");
        }
    }
}

