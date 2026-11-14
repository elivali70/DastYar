using App.Domain.Core.User.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Threading;

public interface ICustomerAppService
{
    Task<IdentityResult> Register(CustomerRegisterDto customerRegisterDto, CancellationToken cancellationToken);
    Task<int> GetId(string phoneNumber, CancellationToken cancellationToken);
    Task<CustomerDto> Get(int id, CancellationToken cancellationToken);
   Task Update(int userId,string name, string phoneNumber,string email,string userName,string address,int fileId,CancellationToken cancellationToken);
}
