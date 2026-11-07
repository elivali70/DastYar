using App.Domain.Core.User.Contracts.Repositories;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.EntityFrameworkCore;

namespace App.Domain.Core.User.Dtos;

public class CustomerQueryRepository:ICustomerQueryRepository
{
    private readonly AppDbContext _appDbContext;

    public CustomerQueryRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<List<CustomerDto>> GetAll(CancellationToken cancellationToken)
    {
        var customerUsers = await _appDbContext.Customers.Where(x => !x.User.IsDeleted).Select(s => new CustomerDto()
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
        return customerUsers;
    }
   public async Task<CustomerDto?> Get(int userId, CancellationToken cancellationToken)
    {
        var customerUser = await _appDbContext.Customers.Where(x => !x.User.IsDeleted && x.UserId==userId).Select(s => new CustomerDto()
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
        return customerUser;
    }
   public async Task<CustomerDto?> Get(string userName, CancellationToken cancellationToken)
    {
        var customerUser = await _appDbContext.Customers.Where(x => !x.User.IsDeleted && x.User.UserName == userName).Select(s => new CustomerDto()
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
        return customerUser;
    }

    public async Task<CustomerDto?> GetByPhone(string phoneNumber, CancellationToken cancellationToken)
    {
        var customerUser = await _appDbContext.Users.Where(x => !x.IsDeleted && x.PhoneNumber == phoneNumber).Select(s => new CustomerDto()
        {
            UserId = s.Id,
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
        var customer= await _appDbContext.Customers.Where(x=>x.UserId==customerUser.UserId).SingleOrDefaultAsync(cancellationToken);
        customerUser.Id = customer.Id;
        return customerUser;
    }
}
   