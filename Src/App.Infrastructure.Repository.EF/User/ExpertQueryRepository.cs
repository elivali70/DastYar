using App.Domain.Core.User.Dtos;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Core.User.Contracts.Repositories;

public class ExpertQueryRepository:IExpertQueryRepository
{
    private readonly AppDbContext _appDbContext;

    public ExpertQueryRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<List<ExpertDto>> GetAll(CancellationToken cancellationToken)
    {
        var expertUsers = await _appDbContext.Experts.Where(x => !x.User.IsDeleted).Select(s => new ExpertDto()
        {
            Id = s.Id,
            UserId = s.User.Id,
            Name = s.User.Name,
            Email = s.User.Email,
            Address = s.User.Address,
            PhoneNumber = s.User.PhoneNumber,
            LastName = s.User.LastName,
            PictureFileId = s.User.PictureFileId,
            SubmitDate = s.User.SubmitDate,
            UserName = s.User.UserName,
            IsActive = s.User.IsActive,
        }).ToListAsync(cancellationToken);
        return expertUsers;
    }
   public async Task<ExpertDto?> Get(int userId, CancellationToken cancellationToken)
    {
        var expertUser = await _appDbContext.Experts.Where(x => !x.User.IsDeleted && x.UserId == userId).Select(s => new ExpertDto()
        {
            Id = s.Id,
            UserId = s.User.Id,
            Name = s.User.Name,
            Email = s.User.Email,
            Address = s.User.Address,
            PhoneNumber = s.User.PhoneNumber,
            LastName = s.User.LastName,
            PictureFileId = s.User.PictureFileId,
            SubmitDate = s.User.SubmitDate,
            UserName = s.User.UserName,
            IsActive = s.User.IsActive,
        }).SingleOrDefaultAsync(cancellationToken);
        return expertUser;
    }
   public async Task<ExpertDto?> Get(string userName, CancellationToken cancellationToken)
    {
        var expertUser = await _appDbContext.Customers.Where(x => !x.User.IsDeleted && x.User.UserName == userName).Select(s => new ExpertDto()
        {
            Id = s.Id,
            UserId = s.User.Id,
            Name = s.User.Name,
            Email = s.User.Email,
            Address = s.User.Address,
            PhoneNumber = s.User.PhoneNumber,
            LastName = s.User.LastName,
            PictureFileId = s.User.PictureFileId,
            SubmitDate = s.User.SubmitDate,
            UserName = s.User.UserName,
            IsActive = s.User.IsActive,
        }).SingleOrDefaultAsync(cancellationToken);
        return expertUser;
    }
}

 
  
  