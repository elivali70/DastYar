using App.Domain.Core.User.Dtos;
using App.Domain.Core.User.Entities;
using App.Domain.Core.User.Enums;
using App.Infrastructure.DataBase.SQL.DbContext;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Core.User.Contracts.Repositories;

public class ExpertCommandRepository:IExpertCommandRepository
{
    public UserManager<AppUser> _userManagere;
    public RoleManager<IdentityRole<int>> _roleManager;
    private readonly AppDbContext _appDbContext;

    public ExpertCommandRepository(UserManager<AppUser> userManagere, RoleManager<IdentityRole<int>> roleManager, AppDbContext appDbContext)
    {
        _userManagere = userManagere;
        _roleManager = roleManager;
        _appDbContext = appDbContext;
    }

    public async Task<IdentityResult> Add(ExpertRegisterDto expertRegisterDto, CancellationToken cancellationToken)
    {
        var user = new AppUser();
        user.Name = expertRegisterDto.Name;
        user.Email = expertRegisterDto.Email;
        user.Address = expertRegisterDto.Address;
        user.PhoneNumber = expertRegisterDto.PhoneNumber;
        user.LastName = expertRegisterDto.LastName;
        user.IsDeleted = false;
        user.IsActive = true;
        user.SubmitDate = DateTime.Now;
        user.UserName = expertRegisterDto.UserName;
        user.Role = RoleEnum.ExpertRole;
        var userResult = await _userManagere.CreateAsync(user, expertRegisterDto.Password);
        if (!userResult.Succeeded)
        {
            return userResult;
        }

        var roleResult = await _userManagere.AddToRoleAsync(user, "ExpertRole");
        if (!roleResult.Succeeded)
            return roleResult;
        var expert = new Expert();
        expert.UserId = user.Id;
        await _appDbContext.Experts.AddAsync(expert, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return roleResult;
    }
   public async Task<IdentityResult> Update(ExpertDto expertDto, CancellationToken cancellationToken)
    {
        var expert = await _userManagere.FindByIdAsync(expertDto.UserId.ToString());
        if (expert != null && !expert.IsDeleted)
        {
            expert.Name = expertDto.Name;
            expert.Email = expertDto.Email;
            expert.Address = expertDto.Address;
            expert.PhoneNumber = expertDto.PhoneNumber;
            expert.LastName = expertDto.LastName;
            expert.PictureFileId = expertDto.PictureFileId;
            expert.UserName = expertDto.UserName;
            var update = await _userManagere.UpdateAsync(expert);
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
        var expert = await _userManagere.FindByIdAsync(userId.ToString());
        if (expert != null)
        {
            expert.IsDeleted = true;
            await _userManagere.UpdateAsync(expert);
        }
        else
        {
            throw new Exception("کاربر موردنظر پیدا نشد.");
        }

    }
}

 
  