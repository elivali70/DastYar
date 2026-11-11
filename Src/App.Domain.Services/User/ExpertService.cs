using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Contracts.Repositories;
using App.Domain.Core.User.Contracts.Services;
using App.Domain.Core.User.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Services.User
{
    public class ExpertService : IExpertService
    {
       
        private readonly IExpertCommandRepository _expertCommandRepository;
        private readonly IExpertQueryRepository _expertQueryRepository;

        public ExpertService(IExpertCommandRepository expertCommandRepository,IExpertQueryRepository expertQueryRepository)
        {
            _expertCommandRepository = expertCommandRepository;
            _expertQueryRepository = expertQueryRepository;
        }

        public async Task<int> Get(string userName, CancellationToken cancellationToken)
        {
           var expert= await _expertQueryRepository.Get(userName, cancellationToken);
            return expert.Id;
        }
        public async Task<ExpertDto> Get(int? id, CancellationToken cancellationToken)
        {
            var expert = await _expertQueryRepository.Get(id, cancellationToken);
            return expert;
        }
        public async Task<List<ExpertDto>> GetAll( CancellationToken cancellationToken)
        {
            var expert = await _expertQueryRepository.GetAll( cancellationToken);
            return expert;
        }

        public async Task<IdentityResult> Register(ExpertRegisterDto expertRegisterDto, CancellationToken cancellationToken)
        {
           return await _expertCommandRepository.Add(expertRegisterDto,cancellationToken);
        }
    }
}
