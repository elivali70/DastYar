using App.Domain.Core.User.Dtos;
using App.Domain.Core.User.Entities;
using App.Domain.Core.User.Enums;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Core.User.Contracts.Repositories;

public class AdminCommandRepository : IAdminCommandRepository
{
    public UserManager<AppUser> _userManagere;
    public RoleManager<IdentityRole<int>> _roleManager;
    private readonly AppDbContext _appDbContext;

    public AdminCommandRepository(UserManager<AppUser> userManagere, RoleManager<IdentityRole<int>> roleManager, AppDbContext appDbContext)
    {
        _userManagere = userManagere;
        _roleManager = roleManager;
        _appDbContext = appDbContext;
    }
   
    public async Task<IdentityResult> Add(AppUserDto appUserDto, CancellationToken cancellationToken)
    {

        var user = new AppUser();
        user.Name = appUserDto.Name;
        user.Email = appUserDto.Email;
        user.Address = appUserDto.Address;
        user.PhoneNumber = appUserDto.PhoneNumber;
        user.LastName = appUserDto.LastName;
        user.IsDeleted = false;
        user.IsActive = true;
        user.PictureFileId = appUserDto.PictureFileId;
        user.SubmitDate = DateTime.Now;
        user.UserName = appUserDto.UserName;
        user.Role= appUserDto.Role;
        var userResult = await _userManagere.CreateAsync(user);
        if (!userResult.Succeeded)
        {
            return userResult;
        }
        if (user.Role == RoleEnum.AdminRole)
        {

        var roleResult = await _userManagere.AddToRoleAsync(user, "AdminRole");
        if (!roleResult.Succeeded)
            return roleResult;
        var admin = new Admin();
        admin.UserId = user.Id;
        await _appDbContext.Admins.AddAsync(admin, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        if (user.Role == RoleEnum.CustomerRole)
        {

            var roleResult = await _userManagere.AddToRoleAsync(user, "CustomerRole");
            if (!roleResult.Succeeded)
                return roleResult;
            var customer = new Customer();
            customer.UserId = user.Id;
            await _appDbContext.Customers.AddAsync(customer, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        if (user.Role == RoleEnum.ExpertRole)
        {

            var roleResult = await _userManagere.AddToRoleAsync(user, "ExpertRole");
            if (!roleResult.Succeeded)
                return roleResult;
            var expert = new Expert();
            expert.UserId = user.Id;
            await _appDbContext.Experts.AddAsync(expert, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        return IdentityResult.Success;

    }
   
    public async Task<IdentityResult> Update(AdminDto adminDto, CancellationToken cancellationToken)
    {
        var admin = await _userManagere.FindByIdAsync(adminDto.UserId.ToString());
        if (admin != null && !admin.IsDeleted)
        {
            admin.Name = adminDto.Name;
            admin.Email = adminDto.Email;
            admin.Address = adminDto.Address;
            admin.PhoneNumber = adminDto.PhoneNumber;
            admin.LastName = adminDto.LastName;
            admin.PictureFileId = adminDto.PictureFileId;
            admin.UserName = adminDto.UserName;
            var update = await _userManagere.UpdateAsync(admin);
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
        var admin = await _userManagere.FindByIdAsync(userId.ToString());
        if (admin != null)
        {
            admin.IsDeleted = true;
            await _userManagere.UpdateAsync(admin);
        }
        else
        {
            throw new Exception("کاربر موردنظر پیدا نشد.");
        }

    }
}
