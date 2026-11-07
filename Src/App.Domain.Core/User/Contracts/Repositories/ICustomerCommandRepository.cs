using App.Domain.Core.User.Dtos;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Core.User.Contracts.Repositories;

public interface ICustomerCommandRepository
{
    Task<IdentityResult> Add(CustomerRegisterDto customerRegisterDto, CancellationToken cancellationToken);
    Task<IdentityResult> Update(CustomerDto customerDto, CancellationToken cancellationToken);
    Task Delete(int userId, CancellationToken cancellationToken);
}
