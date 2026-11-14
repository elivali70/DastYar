using App.Domain.Core.User.Dtos;
using Microsoft.AspNetCore.Identity;

public class CustomerAppService : ICustomerAppService
{
    private readonly ICustomerService _customerService;

    public CustomerAppService(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<int> GetId(string phoneNumber, CancellationToken cancellationToken)
    {
        return await _customerService.GetId(phoneNumber, cancellationToken);
    }

    public async Task<IdentityResult> Register(CustomerRegisterDto customerRegisterDto, CancellationToken cancellationToken)
    {
        var result = await _customerService.Register(customerRegisterDto, cancellationToken);

        return result;
    }
    public async Task<CustomerDto> Get(int id, CancellationToken cancellationToken)
    {
        return await _customerService.Get(id, cancellationToken);
    }

    public async Task Update(int userId, string name, string phoneNumber, string email, string userName, string address, int fileId, CancellationToken cancellationToken)
    {
         await _customerService.Update(userId, name, phoneNumber, email, userName, address, fileId, cancellationToken);
    }
}
