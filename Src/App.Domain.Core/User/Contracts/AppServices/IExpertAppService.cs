using App.Domain.Core.User.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.User.Contracts.AppServices
{
    public interface IExpertAppService
    {
        Task<IdentityResult> Register(ExpertRegisterDto expertRegisterDto, CancellationToken cancellationToken);
       Task<int> Get(string userName, CancellationToken cancellationToken);
        Task<ExpertDto> Get(int userId, CancellationToken cancellationToken);
        Task<List<ExpertDto>> GetAll(CancellationToken cancellationToken);
        Task Update(int userId, string name, string phoneNumber, string email, string userName, string address, int fileId, CancellationToken cancellationToken);
        Task AddFavoriteServices(int expertId, List<int> serviceId, CancellationToken cancellationToken);
    }
}
