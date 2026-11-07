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

}
