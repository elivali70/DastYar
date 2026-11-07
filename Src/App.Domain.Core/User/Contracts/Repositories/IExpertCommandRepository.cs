using App.Domain.Core.User.Dtos;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Core.User.Contracts.Repositories;

public interface IExpertCommandRepository
{
    Task<IdentityResult> Add(ExpertRegisterDto expertRegisterDto, CancellationToken cancellationToken);
    Task<IdentityResult> Update(ExpertDto expertDto, CancellationToken cancellationToken);
    Task Delete(int userId, CancellationToken cancellationToken);
}
