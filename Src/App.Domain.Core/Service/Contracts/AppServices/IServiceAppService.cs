using App.Domain.Core.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.Service.Contracts.AppServices
{
    public interface IServiceAppService
    {
        Task<int> GetId(string name,CancellationToken cancellationToken);
        Task<ServiceDto?> Get(int id,CancellationToken cancellationToken);
    }
}
