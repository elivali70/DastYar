using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.User.Dtos;
using Microsoft.AspNetCore.Identity;

public class CustomerService : ICustomerService
{

    private readonly ICustomerCommandRepository _customerCommandRepository;
    private readonly ICustomerQueryRepository _customerQueryRepository;

    public CustomerService(ICustomerCommandRepository customerCommandRepository, ICustomerQueryRepository customerQueryRepository)
    {
        _customerCommandRepository = customerCommandRepository;
        _customerQueryRepository = customerQueryRepository;
    }

    public async Task<int> GetId(string phoneNumber, CancellationToken cancellationToken)
    {
       var customer=await _customerQueryRepository.GetByPhone(phoneNumber,cancellationToken);
        return customer.Id;
    }
    public async Task<CustomerDto> Get(int id, CancellationToken cancellationToken)
    {
        var customer = await _customerQueryRepository.Get(id, cancellationToken);
        return customer;
    }
    public async Task<IdentityResult> Register(CustomerRegisterDto customerRegisterDto, CancellationToken cancellationToken)
    {
        return await _customerCommandRepository.Add(customerRegisterDto, cancellationToken);
    }
}
