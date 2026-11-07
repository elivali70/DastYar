using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.User.Dtos;
using App.Domain.Core.User.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

public class AdminService : IAdminService
{
    private readonly IAdminQueryRepository _adminQueryRepository;
    private readonly IAdminCommandRepository _adminCommandRepository;
    private readonly ILogger<AdminService> _logger;

    public AdminService(IAdminQueryRepository adminQueryRepository, 
        IAdminCommandRepository adminCommandRepository,
        ILogger<AdminService> logger)
    {
        _adminQueryRepository = adminQueryRepository;
       _adminCommandRepository = adminCommandRepository;
        _logger = logger;
    }
    public async Task<List<AppUserDto>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogTrace("start motehod {methodename} of AdminService", "GetAll");
        var users= await _adminQueryRepository.GetAll(cancellationToken);
        _logger.LogTrace("finish motehod {methodename} of AdminService", "GetAll");

        return users;
    }
    public async Task<AppUserDto?> Get(int id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("start method {methodName}", nameof(Get));
        var users = await _adminQueryRepository.Get(id, cancellationToken);
        _logger.LogTrace("finish method {methodName}", nameof(Get));
        return users;
    }
    public async Task<IdentityResult> Add(AppUserDto appUserDto, CancellationToken cancellationToken)
    {
        _logger.LogTrace("start method {methodName}", nameof(Get));
        var users = await _adminCommandRepository.Add(appUserDto, cancellationToken);
        _logger.LogTrace("finish method {methodName}", nameof(Get));
        return users;
    }
}