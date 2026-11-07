using App.Domain.Core.User.Dtos;
using Microsoft.AspNetCore.Identity;

public interface ICustomerAppService
{
    Task<IdentityResult> Register(CustomerRegisterDto customerRegisterDto, CancellationToken cancellationToken);
    Task<int> GetId(string phoneNumber, CancellationToken cancellationToken);
    Task<CustomerDto> Get(int id, CancellationToken cancellationToken);
}
