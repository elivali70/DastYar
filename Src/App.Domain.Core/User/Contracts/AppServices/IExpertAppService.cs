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
        Task<ExpertDto> Get(int? id, CancellationToken cancellationToken);
        Task<List<ExpertDto>> GetAll(CancellationToken cancellationToken);
    }
}
