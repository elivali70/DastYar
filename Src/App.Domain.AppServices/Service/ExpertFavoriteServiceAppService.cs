using App.Domain.Core.Service.Contracts.AppServices;
using App.Domain.Core.Service.Contracts.Repositories;
using App.Domain.Core.Service.Contracts.Services;
using App.Domain.Core.Service.Dtos;

namespace App.Domain.AppServices.Service
{
    public class ExpertFavoriteServiceAppService : IExpertFavoriteServiceAppService
    {
        private readonly IExpertFavoriteServiceService _expertFavoriteServiceService;

        public ExpertFavoriteServiceAppService(IExpertFavoriteServiceService expertFavoriteServiceService)
        {
            _expertFavoriteServiceService = expertFavoriteServiceService;
        }
        public async Task<List<ServiceDto>> GetServices(int expertId, CancellationToken cancellationToken)
        {
            return await _expertFavoriteServiceService.GetServices(expertId, cancellationToken);
        }
    }
   
}
