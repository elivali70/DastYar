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

        public async Task<int> Get(string userName, CancellationToken cancellationToken)
        {
            return await _expertService.Get(userName, cancellationToken);
        }
        public async Task AddFavoriteServices(int expertId,List<int> serviceId ,CancellationToken cancellationToken)
        {
            foreach (var id in serviceId)
            {
            await _expertService.AddFavoriteServices(expertId, id, cancellationToken);
            }
        }
        public async Task<ExpertDto> Get(int userId, CancellationToken cancellationToken)
        {
            return  await _expertService.Get(userId, cancellationToken);
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
        public async Task Update(int userId, string name, string phoneNumber, string email, string userName, string address, int fileId, CancellationToken cancellationToken)
        {
            await _expertService.Update(userId, name, phoneNumber, email, userName, address, fileId, cancellationToken);
        }
    }
}
