using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.User.Dtos;
using App.Domain.Core.User.Enums;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace App.Domain.Core.User.Contracts.Repositories;

public class AdminQueryRepository:IAdminQueryRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger<AdminQueryRepository> _logger;

    public AdminQueryRepository(AppDbContext appDbContext, ILogger<AdminQueryRepository> logger)
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }
    public async Task<List<AppUserDto>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogTrace("start method {name} of AdminqueryRepository", "GetAll");
        var users= await _appDbContext.Users.Where(x=> !x.IsDeleted).Select(s=> new AppUserDto()
        {
            Id = s.Id,
            Name = s.Name,
            Email = s.Email,
            Address = s.Address,
            PhoneNumber = s.PhoneNumber,
            LastName = s.LastName,
            PictureFileId = s.PictureFileId,
            SubmitDate = s.SubmitDate,
            UserName = s.UserName,
            IsActive = s.IsActive,
        }).ToListAsync(cancellationToken);
        _logger.LogInformation("Read Users from DataBase in {name} methode in Adminquery Repository", "GetAll");
        _logger.LogTrace("finish method {name} of AdminqueryRepository", "GetAll");

        return users;
    }
  
    public async Task<AppUserDto?> Get(int id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("start method {name} of AdminqueryRepository", nameof(Get));
        var user = await _appDbContext.Users.Where(x => !x.IsDeleted && x.Id== id).Select(s => new AppUserDto()
        {
            Id=s.Id,
            Name = s.Name,
            Email = s.Email,
            Address = s.Address,
            PhoneNumber = s.PhoneNumber,
            LastName = s.LastName,
            PictureFileId = s.PictureFileId,
            SubmitDate = s.SubmitDate,
            UserName = s.UserName,
            IsActive = s.IsActive,
        }).SingleOrDefaultAsync(cancellationToken);
        _logger.LogInformation("Read Users from DataBase in {name} methode in Adminquery Repository", nameof(Get));
        _logger.LogTrace("finish method {name} of AdminqueryRepository", nameof(Get));
        return user;
    }
  public async  Task<AppUserDto?> Get(string userName, CancellationToken cancellationToken)
    {
        var user = await _appDbContext.Users.Where(x => !x.IsDeleted && x.UserName == userName).Select(s => new AppUserDto()
        {
            Id = s.Id,
            Name = s.Name,
            Email = s.Email,
            Address = s.Address,
            PhoneNumber = s.PhoneNumber,
            LastName = s.LastName,
            PictureFileId = s.PictureFileId,
            SubmitDate = s.SubmitDate,
            UserName = s.UserName,
            IsActive = s.IsActive,
        }).SingleOrDefaultAsync(cancellationToken);
        return user;
    }
}
