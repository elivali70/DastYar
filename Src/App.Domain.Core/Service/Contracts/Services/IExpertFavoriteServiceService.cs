using App.Domain.Core.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.Service.Contracts.Services
{
    public interface IExpertFavoriteServiceService
    {
        Task<List<ServiceDto>> GetServices(int expertId, CancellationToken cancellationToken);
    }
}
