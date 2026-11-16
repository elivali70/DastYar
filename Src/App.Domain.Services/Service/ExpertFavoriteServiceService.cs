using App.Domain.Core.Service.Contracts.AppServices;
using App.Domain.Core.Service.Contracts.Repositories;
using App.Domain.Core.Service.Contracts.Services;
using App.Domain.Core.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Services.Service
{
    public class ExpertFavoriteServiceService : IExpertFavoriteServiceService
    {
        private readonly IExpertFavoritServiceQueryRepository _expertFavoritServiceQueryRepository;

        public ExpertFavoriteServiceService(IExpertFavoritServiceQueryRepository expertFavoritServiceQueryRepository)
        {
            _expertFavoritServiceQueryRepository = expertFavoritServiceQueryRepository;
        }
        public async Task<List<ServiceDto>> GetServices(int expertId, CancellationToken cancellationToken)
        {
            return await _expertFavoritServiceQueryRepository.GetServices(expertId, cancellationToken);
        }
    }
}
