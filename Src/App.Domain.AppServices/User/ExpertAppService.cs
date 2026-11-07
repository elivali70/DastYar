using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.User.Contracts.Services;
using App.Domain.Core.User.Dtos;
using App.Domain.Core.User.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.AppServices.User
{
    public class ExpertAppService : IExpertAppService
    {
        private readonly IExpertService _expertService;

        public ExpertAppService(IExpertService expertService)
        {
            _expertService = expertService;
        }

        public Task<int> Get(string userName, CancellationToken cancellationToken)
        {
            return _expertService.Get(userName, cancellationToken);
        }
        public Task<ExpertDto> Get(int id, CancellationToken cancellationToken)
        {
            return _expertService.Get(id, cancellationToken);
        }
        public async Task<List<ExpertDto>> GetAll(CancellationToken cancellationToken)
        {
            var expert = await _expertService.GetAll(cancellationToken);
            return expert;
        }

        public async Task<IdentityResult> Register(ExpertRegisterDto expertRegisterDto,CancellationToken cancellationToken)
        {
            var result= await _expertService.Register(expertRegisterDto, cancellationToken);
            
            return result;
        }
    }
}
