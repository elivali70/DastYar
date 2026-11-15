using App.Domain.Core.Service.Contracts.Repositories;
using App.Domain.Core.Service.Dtos;
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
        private readonly IExpertFavoritServiceCommandRepository _expertFavoritServiceCommandRepository;

        public ExpertService(IExpertCommandRepository expertCommandRepository,
            IExpertQueryRepository expertQueryRepository,
            IExpertFavoritServiceCommandRepository expertFavoritServiceCommandRepository)
        {
            _expertCommandRepository = expertCommandRepository;
            _expertQueryRepository = expertQueryRepository;
            _expertFavoritServiceCommandRepository = expertFavoritServiceCommandRepository;
        }

        public async Task AddFavoriteServices(int expertId, int serviceId, CancellationToken cancellationToken)
        {
            var expertFavoritServiceDto = new ExpertFavoritServiceDto()
            {
                ExpertId = expertId,
                ServiceId = serviceId,
                CreatedAt = DateTime.Now,
            };
           var id= await _expertFavoritServiceCommandRepository.Add(expertFavoritServiceDto, cancellationToken);
        }

        public async Task<int> Get(string userName, CancellationToken cancellationToken)
        {
           var expert= await _expertQueryRepository.Get(userName, cancellationToken);
            return expert.Id;
        }
        public async Task<ExpertDto> Get(int userId, CancellationToken cancellationToken)
        {
            var expert = await _expertQueryRepository.Get(userId, cancellationToken);
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
        public async Task Update(int userId, string name, string phoneNumber, string email, string userName, string address, int fileId, CancellationToken cancellationToken)
        {
            var expertDto = new ExpertDto()
            {
                UserId = userId,
                Name = name,
                PhoneNumber = phoneNumber,
                Email = email,
                UserName = userName,
                Address = address,
                PictureFileId = fileId,
            };
            await _expertCommandRepository.Update(expertDto, cancellationToken);
        }
    }
}
