using App.Domain.Core.User.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

public class AdminAppService : IAdminAppService
{
    private readonly IAdminService _adminService;
    private readonly ILogger<AdminAppService> _logger;

    public AdminAppService(IAdminService adminService, ILogger<AdminAppService> logger)
    {
        _adminService = adminService;
        _logger = logger;
    }
    public async Task<List<AppUserDto>> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogTrace("start method {methodName}", nameof(GetAll));
        var users= await _adminService.GetAll(cancellationToken);
        _logger.LogTrace("finish method {methodName}", nameof(GetAll));
        return users;
    }
    public async Task<AppUserDto?> Get(int id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("start method {methodName}", nameof(Get));
        var users = await _adminService.Get(id,cancellationToken);
        _logger.LogTrace("finish method {methodName}", nameof(Get));
        return users;
    }
    public async Task<IdentityResult> Add(AppUserDto appUserDto, CancellationToken cancellationToken)
    {
        _logger.LogTrace("start method {methodName}", nameof(Get));
        var users = await _adminService.Add(appUserDto, cancellationToken);
        _logger.LogTrace("finish method {methodName}", nameof(Get));
        return users;
    }
}
