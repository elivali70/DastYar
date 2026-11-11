using App.Domain.Core.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.Service.Contracts.Services
{
    public interface IServiceService
    {
        Task<int> GetId(string name, CancellationToken cancellationToken);
        Task<ServiceDto?> Get(int id, CancellationToken cancellationToken);
        Task<List<ServiceDto>> GetAll(CancellationToken cancellationToken);
    }
}
