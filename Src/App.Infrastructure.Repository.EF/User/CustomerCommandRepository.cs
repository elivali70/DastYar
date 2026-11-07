using App.Domain.Core.User.Dtos;
using App.Domain.Core.User.Entities;
using App.Domain.Core.User.Enums;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Core.User.Contracts.Repositories;

public class CustomerCommandRepository:ICustomerCommandRepository
{
    public UserManager<AppUser> _userManagere;
    public RoleManager<IdentityRole<int>> _roleManager;
    private readonly AppDbContext _appDbContext;

    public CustomerCommandRepository(UserManager<AppUser> userManagere, RoleManager<IdentityRole<int>> roleManager, AppDbContext appDbContext)
    {
        _userManagere = userManagere;
        _roleManager = roleManager;
        _appDbContext = appDbContext;
    }
    public async Task<IdentityResult> Add(CustomerRegisterDto customerRegisterDto, CancellationToken cancellationToken)
    {
        var user = new AppUser();
        user.Name = customerRegisterDto.Name;
        user.Email = customerRegisterDto.Email;
        user.Address = customerRegisterDto.Address;
        user.PhoneNumber = customerRegisterDto.PhoneNumber;
        user.LastName = customerRegisterDto.LastName;
        user.IsDeleted = false;
        user.IsActive = true;
        user.SubmitDate = DateTime.Now;
        user.UserName = customerRegisterDto.UserName;
        user.Role = RoleEnum.CustomerRole;
        var userResult = await _userManagere.CreateAsync(user, customerRegisterDto.Password);
        if (!userResult.Succeeded)
        {
            return userResult;
        }

        var roleResult = await _userManagere.AddToRoleAsync(user, "CustomerRole");
        if (!roleResult.Succeeded)
            return roleResult;
        var customer = new Customer();
        customer.UserId = user.Id;
        await _appDbContext.Customers.AddAsync(customer, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return roleResult;
    }
   public async Task<IdentityResult> Update(CustomerDto customerDto, CancellationToken cancellationToken)
    {
        var customer = await _userManagere.FindByIdAsync(customerDto.UserId.ToString());
        if (customer != null && !customer.IsDeleted)
        {
            customer.Name = customerDto.Name;
            customer.Email = customerDto.Email;
            customer.Address = customerDto.Address;
            customer.PhoneNumber = customerDto.PhoneNumber;
            customer.LastName = customerDto.LastName;
            customer.PictureFileId = customerDto.PictureFileId;
            customer.UserName = customerDto.UserName;
            var update = await _userManagere.UpdateAsync(customer);
            return update;
        }
        else
        {
            return IdentityResult.Failed(
            new IdentityError
            {
                Description = "کاربر موردنظر پیدا نشد."
            });
        }
    }
   public async Task Delete(int userId, CancellationToken cancellationToken)
    {
        var customer = await _userManagere.FindByIdAsync(userId.ToString());
        if (customer != null)
        {
            customer.IsDeleted = true;
            await _userManagere.UpdateAsync(customer);
        }
        else
        {
            throw new Exception("کاربر موردنظر پیدا نشد.");
        }
    }
}


   
  