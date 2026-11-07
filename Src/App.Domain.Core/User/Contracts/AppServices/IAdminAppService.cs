using App.Domain.Core.User.Dtos;
using Microsoft.AspNetCore.Identity;

public interface IAdminAppService
{
    Task<List<AppUserDto>> GetAll(CancellationToken cancellationToken);
    Task<AppUserDto?> Get(int id, CancellationToken cancellationToken);
    Task<IdentityResult> Add(AppUserDto appUserDto, CancellationToken cancellationToken);
}