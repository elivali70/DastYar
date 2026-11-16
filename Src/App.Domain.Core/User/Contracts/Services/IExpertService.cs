using App.Domain.Core.User.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.User.Contracts.Services
{
    public interface IExpertService
    {
        Task<IdentityResult> Register(ExpertRegisterDto expertRegisterDto, CancellationToken cancellationToken);
        Task<int> Get(string userName, CancellationToken cancellationToken);
        Task<ExpertDto> Get(int userId, CancellationToken cancellationToken);
        Task<List<ExpertDto>> GetAll(CancellationToken cancellationToken);
        Task Update(int userId, string name, string phoneNumber, string email, string userName, string address, int fileId, CancellationToken cancellationToken);
        Task AddFavoriteServices(int expertId, int serviceId, CancellationToken cancellationToken);
         Task<ExpertDto> GetByExpertId(int? expertId, CancellationToken cancellationToken);
    }
}
