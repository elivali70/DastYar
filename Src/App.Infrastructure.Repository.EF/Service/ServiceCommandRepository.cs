using App.Domain.Core.AppFile.Comntracts.Repositories;
using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.Service.Dtos;
using service=App.Domain.Core.Service.Entities;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Core.Service.Contracts.Repositories;

public class ServiceCommandRepository:IServiceCommandRepository
{
    private readonly AppDbContext _appDbContext;

    public ServiceCommandRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<int> Add(ServiceDto serviceDto, CancellationToken cancellationToken)
    {
        var service = new service.Service();

        service.Disctiption = serviceDto.Disctiption;
        service.Name = serviceDto.Name;
        service.BasePrice = serviceDto.BasePrice;
        service.CategoryId = serviceDto.CategoryId;
        service.IsDeleted = false;
        service.CreatedAt =DateTime.Now;

        await _appDbContext.AddAsync(service);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return service.Id;
    }
    public async Task Update(ServiceDto serviceDto, CancellationToken cancellationToken)
    {
        var service = await _appDbContext.Services.SingleOrDefaultAsync(x => x.Id == serviceDto.Id);
        if (service != null)
        {
           service.Disctiption=serviceDto.Disctiption;
            service.Name=serviceDto.Name;
            service.Id=serviceDto.Id;
            service.BasePrice=serviceDto.BasePrice;
            service.CategoryId=serviceDto.CategoryId;
            service.CreatedAt = serviceDto.CreatedAt;

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        else
        {
            throw new Exception($"service with Id={serviceDto.Id} dosnt exist");

        }
    }
    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        var service = await _appDbContext.Services.SingleOrDefaultAsync(x => x.Id == id);
        if (service != null)
        {
            service.IsDeleted = true;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        else
        {
            throw new Exception($"service with Id={id} dosnt exist");
        }
    }
}

  