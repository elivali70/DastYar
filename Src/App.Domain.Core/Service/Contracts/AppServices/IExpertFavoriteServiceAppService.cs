using App.Domain.Core.Service.Dtos;
using App.Domain.Core.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.Service.Contracts.AppServices
{
    public interface IExpertFavoriteServiceAppService
    {
        Task<List<ServiceDto>> GetServices(int expertId, CancellationToken cancellationToken);
    }
}
