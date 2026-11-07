using App.Domain.Core.User.Dtos;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Core.User.Contracts.Repositories;

public interface IAdminCommandRepository
{
    Task<IdentityResult> Add(AppUserDto appUserDto, CancellationToken cancellationToken);
    Task<IdentityResult> Update(AdminDto adminDto, CancellationToken cancellationToken);
    Task Delete(int userId, CancellationToken cancellationToken);
}
